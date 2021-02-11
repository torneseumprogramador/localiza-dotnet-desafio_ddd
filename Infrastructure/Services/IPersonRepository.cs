using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IPersonRepository
    {
        Task<int> CountByIdAndDocument<T>(int id, string document);
        Task<int> CountByDocument<T>(string document);
        Task<T> FindByDocumentAndPassword<T>(string document, string password);
        Task<ICollection<T>> All<T>();
    }
}
