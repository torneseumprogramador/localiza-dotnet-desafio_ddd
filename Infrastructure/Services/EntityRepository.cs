using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EntityRepository : IEntityRepository
    {
        public Task<ICollection<T>> All<T>()
        {
            throw new System.NotImplementedException();
        }

        public Task Delete<T>(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> FindById<T>(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Save<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Update<T>(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
