using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Infrastructure.Database;

namespace Infrastructure.RepositoryServices
{
    public class EntityRepository : IEntityRepository
    {
        public EntityRepository()
        {
            this.sqlDriver = new SqlDriver();
        }

        public SqlDriver sqlDriver;

        public async Task<ICollection<T>> All<T>()
        {
            return await this.sqlDriver.All<T>();
        }

        public async Task Delete<T>(int id)
        {
            var instance = MapTable.CreateInstanceAndSetId<T>(id);
            await this.sqlDriver.Delete<T>(instance);
        }

        public async Task Delete<T>(T entity)
        {
            await this.sqlDriver.Delete(entity);
        }

        public async Task<T> FindById<T>(int id)
        {
            return await this.sqlDriver.FindById<T>(id);
        }

        public async Task Save<T>(T entity)
        {
            await this.sqlDriver.Save(entity);
        }

        public async Task Update<T>(T entity)
        {
            await this.sqlDriver.Update(entity);
        }


        public async Task<ICollection<T>> All<T>(string key, ICollection<dynamic> parameters = null)
        {
            return await this.sqlDriver.AllByPrecedure<T>(key, PrepareParams(parameters));
        }

        public async Task<T> Get<T>(string key, ICollection<dynamic> parameters = null)
        {
            return await this.sqlDriver.GetByPrecedure<T>(key, PrepareParams(parameters));
        }

        public static List<DbParameter> PrepareParams(ICollection<dynamic> parameters = null)
        {
            List<DbParameter> dbParams = null;
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    dbParams = new List<DbParameter>();
                    string key = param.GetType().GetProperty("Key").GetValue(param, null);
                    object val = param.GetType().GetProperty("Value").GetValue(param, null);

                    var parameterType = new SqlParameter($"@{key}", System.Data.SqlDbType.VarChar)
                    {
                        Value = val
                    };
                    dbParams.Add(parameterType);
                }
            }

            return dbParams;
        }
    }
}
