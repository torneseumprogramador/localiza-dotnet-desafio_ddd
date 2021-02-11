using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Infrastructure.Database;

namespace Infrastructure.Services.Person
{
    public class PersonRepository : IPersonRepository
    {
        public PersonRepository()
        {
            this.sqlDriver = new SqlDriver();
        }

        public SqlDriver sqlDriver;

        public async Task<ICollection<T>> All<T>()
        {
            return await this.sqlDriver.All<T>();
        }

        public async Task<int> CountByDocument<T>(string document, int type)
        {
            List<DbParameter> parameters = new List<DbParameter>();

            var parameterDocument = new SqlParameter("@document", System.Data.SqlDbType.VarChar)
            {
                Value = document
            };
            parameters.Add(parameterDocument);

            var parameterType = new SqlParameter("@type", System.Data.SqlDbType.TinyInt)
            {
                Value = type
            };
            parameters.Add(parameterType);

            return await this.sqlDriver.CountByPrecedure<T>("sp_CountUserByDocument", parameters);
        }

        public async Task<int> CountByIdAndDocument<T>(int id, string document, int type)
        {
            List<DbParameter> parameters = new List<DbParameter>();

            var parameterId = new SqlParameter("@id", System.Data.SqlDbType.Int)
            {
                Value = id
            };
            parameters.Add(parameterId);

            var parameterType = new SqlParameter("@type", System.Data.SqlDbType.TinyInt)
            {
                Value = type
            };
            parameters.Add(parameterType);

            var parameterDocument = new SqlParameter("@document", System.Data.SqlDbType.VarChar)
            {
                Value = document
            };
            parameters.Add(parameterDocument);

            return await this.sqlDriver.CountByPrecedure<T>("sp_CountUserByDifferentIdAndDocument", parameters);
        }

        public async Task<T> FindByDocumentAndPassword<T>(string document, string password, int type)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            var parameterDocument = new SqlParameter("@document", System.Data.SqlDbType.VarChar)
            {
                Value = document
            };
            parameters.Add(parameterDocument);

            var parameterType = new SqlParameter("@type", System.Data.SqlDbType.TinyInt)
            {
                Value = type
            };
            parameters.Add(parameterType);

            var parameterPass = new SqlParameter("@password", System.Data.SqlDbType.VarChar)
            {
                Value = password
            };
            parameters.Add(parameterPass);

            var entity = await this.sqlDriver.OneByPrecedure<T>("sp_findUserByDocumentAndPassword", parameters);
            return entity;
        }

        public async Task<ICollection<T>> All<T>(int type)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            var parameterType = new SqlParameter("@type", System.Data.SqlDbType.TinyInt)
            {
                Value = type
            };
            parameters.Add(parameterType);

            return await this.sqlDriver.AllByPrecedure<T>("sp_findUsersByType", parameters);
        }
    }
}
