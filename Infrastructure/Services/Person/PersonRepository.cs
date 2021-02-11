using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services.Person
{
    public class PersonRepository : IPersonRepository
    {
        public Task<ICollection<T>> All<T>()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CountByDocument<T>(string document)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CountByIdAndDocument<T>(int id, string document)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> FindByDocumentAndPassword<T>(string document, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
