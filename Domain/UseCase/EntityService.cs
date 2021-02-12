using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Services;
using Infrastructure.Services.Exceptions;

namespace Domain.UseCase.UserServices
{
    public class EntityService
    {
        public EntityService(IEntityRepository repository)
        {
            this.repository = repository;
        }

        private IEntityRepository repository;

        public virtual async Task Save<T>(T entity)
        {
            await repository.Save(entity);
        }

        public async Task Delete<T>(int id)
        {
            if (id == 0) throw new EntityEmptyId("Id não pode ser vazio");
            var user = await repository.FindById<T>(id);
            if (user == null) throw new EntityNotFound("Registro não encontrado");
            await repository.Delete(user);
        }

        public async Task<ICollection<T>> All<T>()
        {
           return await repository.All<T>();
        }
    }
}
