using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IEntityRepository
    {
        Task<T> FindById<T>(int id);
        Task Update<T>(T entity);
        Task Save<T>(T entity);
        Task Delete<T>(int id);
        Task Delete<T>(T entity);
        Task<ICollection<T>> All<T>();
        Task<ICollection<T>> All<T>(string key, ICollection<dynamic> parameters = null);
        Task<T> Get<T>(string key, ICollection<dynamic> parameters = null);
    }
}
