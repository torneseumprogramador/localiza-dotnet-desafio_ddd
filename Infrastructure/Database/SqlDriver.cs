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

        private readonly string connectionString;

        public async Task Save<T>(T obj)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            var queryString = MapTable.BuilderInsert(obj);
            SqlCommand command = new SqlCommand(queryString, connection);
            var parameters = MapTable.BuilderParameters(obj);
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            command.Connection.Open();

            MapTable.SetIdOfEntity(obj, await command.ExecuteScalarAsync());
            await connection.CloseAsync();
        }

        public async Task Update<T>(T obj)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            var queryString = MapTable.BuilderUpdate(obj);
            SqlCommand command = new SqlCommand(queryString, connection);
            var parameters = MapTable.BuilderParameters(obj, true);
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            command.Connection.Open();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        public async Task SqlCommand(string queryString)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        public async Task<ICollection<T>> All<T>(string SqlWhere = null)
        {
            var list = new List<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var queryString = MapTable.BuilderSelect<T>(SqlWhere);
                SqlCommand command = new SqlCommand(queryString, connection)
                {
                    CommandType = System.Data.CommandType.Text
                };
                command.Connection.Open();

                using SqlDataReader dr = command.ExecuteReader();
                while (await dr.ReadAsync())
                {
                    var instance = Activator.CreateInstance(typeof(T));
                    this.fill(instance, dr);
                    list.Add((T)instance);
                }
                await dr.CloseAsync();
                await dr.DisposeAsync();
            }

            return list;
        }

        public async Task<List<T>> AllByPrecedure<T>(string queryString, List<DbParameter> parameters = null)
        {
            var list = new List<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Connection.Open();
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                        command.Parameters.Add(parameter);
                }

                using SqlDataReader dr = command.ExecuteReader();
                while (await dr.ReadAsync())
                {
                    var instance = Activator.CreateInstance(typeof(T));
                    this.fill(instance, dr);
                    list.Add((T)instance);
                }
                await dr.CloseAsync();
                await dr.DisposeAsync();
            }

            return list;
        }

        public async Task<T> GetByPrecedure<T>(string queryString, List<DbParameter> parameters = null)
        {
            var instance = Activator.CreateInstance(typeof(T));
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Connection.Open();
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                        command.Parameters.Add(parameter);
                }

                using SqlDataReader dr = command.ExecuteReader();
                if (await dr.ReadAsync())
                    this.fill(instance, dr);
                else instance = null;

                await dr.CloseAsync();
                await dr.DisposeAsync();
            }

            return (T)instance;
        }

        public async Task<T> FindById<T>(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            var instance = MapTable.CreateInstanceAndSetId<T>(id);
            var queryString = MapTable.BuildFindById<T>(id);
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            using SqlDataReader dr = command.ExecuteReader();
            if (await dr.ReadAsync())
                this.fill(instance, dr);

            await dr.CloseAsync();
            await dr.DisposeAsync();
            return instance;
        }

        public async Task<int> CountByPrecedure<T>(string queryString, List<DbParameter> parameters = null)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Connection.Open();
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                        command.Parameters.Add(parameter);
                }

                count = Convert.ToInt32(await command.ExecuteScalarAsync());
            }

            return count;
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

        public async Task Delete<T>(T obj)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            var queryString = MapTable.BuilderDelete(obj);
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        public async Task CreateTable<T>()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            var queryString = MapTable.CreateTable<T>();
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }
    }
}