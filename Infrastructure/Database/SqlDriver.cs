using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class SqlDriver
    {
        public SqlDriver()
        {
            string cnn = Environment.GetEnvironmentVariable("CONNECTION_STRING", EnvironmentVariableTarget.Process);
            this.connectionString = cnn;
        }

        private string connectionString;

        public async Task Save<T>(T obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var queryString = MapTable.BuilderInsert(obj);
                SqlCommand command = new SqlCommand(queryString, connection);
                var parameters = MapTable.BuilderParameters(obj);
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
                command.Connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update<T>(T obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var queryString = MapTable.BuilderUpdate(obj);
                SqlCommand command = new SqlCommand(queryString, connection);
                var parameters = MapTable.BuilderParameters(obj, true);
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
                command.Connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public void SqlCommand(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public async Task<ICollection<T>> All<T>(string SqlWhere = null)
        {
            var list = new List<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var queryString = MapTable.BuilderSelect<T>(SqlWhere);
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection.Open();

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (await dr.ReadAsync())
                    {
                        var instance = Activator.CreateInstance(typeof(T));
                        this.fill(instance, dr);
                        list.Add((T)instance);
                    }
                }
            }

            return list;
        }

        public List<T> AllByPrecedure<T>(string queryString, List<DbParameter> parameters = null)
        {
            var list = new List<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection.Open();
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                        command.Parameters.Add(parameter);
                }

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var instance = Activator.CreateInstance(typeof(T));
                        this.fill(instance, dr);
                        list.Add((T)instance);
                    }
                }
            }

            return list;
        }

        private void fill(object modelo, SqlDataReader dr)
        {
            foreach (var p in modelo.GetType().GetProperties())
            {
                try
                {
                    if (dr[p.Name] == DBNull.Value) continue;
                    p.SetValue(modelo, dr[p.Name]);
                }
                catch { }
            }
        }

        public async Task<T> FindById<T>(int id)
        {
            var instance = Activator.CreateInstance(typeof(T));
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var queryString = MapTable.BuildFindById(instance);
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (await dr.ReadAsync())
                    {
                        this.fill(instance, dr);
                        break;
                    }
                }
            }
            return (T)instance;
        }

        public async Task Delete<T>(T obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var queryString = MapTable.BuilderDelete(obj);
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}