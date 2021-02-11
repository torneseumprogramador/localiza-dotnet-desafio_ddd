using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Authentication;
using Domain.Entities;
using Domain.ViewModel;
using Infrastructure.Services;
using Infrastructure.Services.Person;

namespace Domain.UseCase.UserServices
{
    public class UserService
    {
        public UserService(IPersonRepository personRepository, IEntityRepository entityRepository)
        {
            this.personRepository = personRepository;
            this.entityRepository = entityRepository;
        }

        private IPersonRepository personRepository;
        private IEntityRepository entityRepository;

        public async Task Save(IPerson person)
        {
            if (person.Id > 0)
            {
                var size = await personRepository.CountByIdAndDocument<IPerson>(person.Id, person.Document);
                if (size > 0) throw new EntityUniq("Documento já cadastrado");
            }
            else
            {
                var size = await personRepository.CountByDocument<IPerson>(person.Document);
                if (size > 0) throw new EntityUniq("Documento já cadastrado");
            }
            
            await entityRepository.Save(person);
        }

        public async Task Delete(int id)
        {
            await entityRepository.Delete<IPerson>(id);
        }

        public async Task<UserJwt> Login(IPerson person, IToken token)
        {
            var loggedPerson = await personRepository.FindByDocumentAndPassword<IPerson>(person.Document, person.Password);
            if (loggedPerson == null) throw new EntityNotFound("Documento e senha inválidos");
            return new UserJwt()
            {
                Id = loggedPerson.Id,
                Name = loggedPerson.Name,
                Document = loggedPerson.Document,
                Role = loggedPerson.Role.ToString(),
                Token = token.GerarToken(loggedPerson)
            };
        }

        public async Task<ICollection<UserView>> All()
        {
           return await personRepository.All<UserView>();
        }
    }
}
