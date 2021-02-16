using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.RepositoryServices.Person;
using Infrastructure.RepositoryServices;
using Domain.UseCase;
using Domain.ViewModel;
using Infrastructure.RepositoryServices.Exceptions;
using Domain.Entities.Exceptions;

namespace api.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PersonService _personService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
            _personService = new PersonService(new PersonRepository(), new EntityRepository());
        }

        [HttpGet]
        [Route("/usuarios")]
        [Authorize(Roles = "User, Operator")]
        public async Task<ICollection<User>> Index()
        {
            return await _personService.AllUsers();
        }

        [HttpPost]
        [Route("/usuarios")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] UserSave userSave)
        {
            try
            {
                await _personService.SaveUser(userSave);
                return StatusCode(201);
            }
            catch (UserInvalidCPF err)
            {
                return StatusCode(401, new
                {
                    Message = err.Message
                });
            }
            catch (EntityUniq err)
            {
                return StatusCode(401, new
                {
                    Message = err.Message
                });
            }
        }

        [HttpPut]
        [Route("/usuarios/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update(int id, [FromBody] UserSave userSave)
        {
            try
            {
                await _personService.SaveUser(userSave, id);
                return StatusCode(204);
            }
            catch (UserInvalidCPF err)
            {
                return StatusCode(401, new
                {
                    Message = err.Message
                });
            }
            catch (EntityUniq err)
            {
                return StatusCode(401, new
                {
                    Message = err.Message
                });
            }
        }

        [HttpDelete]
        [Route("/usuarios/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _personService.Delete(id);
                return StatusCode(204);
            }
            catch (EntityEmptyId err)
            {
                return StatusCode(401, new
                {
                    Message = err.Message
                });
            }
            catch (EntityNotFound err)
            {
                return StatusCode(404, new
                {
                    Message = err.Message
                });
            }
        }
    }
}
