using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Database;

namespace Infrastructure.Services
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
    }
}
