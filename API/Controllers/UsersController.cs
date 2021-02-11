using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.ViewModel;
using Domain.Entities;
using Domain.UseCase.UserServices;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Services.Person;
using Infrastructure.Services;
using API.Authentication;

namespace api.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
            _userService = new UserService(new PersonRepository(), new EntityRepository());
        }

        [HttpPost]
        [Route("/users/login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {  
            try
            {
                IPerson person;
                if(userLogin.Document.Length >= 11)
                {
                    person = new User()
                    {
                        Document = userLogin.Document,
                        Password = userLogin.Password,
                    };
                }
                else
                {
                    person = new Operator()
                    {
                        Document = userLogin.Document,
                        Password = userLogin.Password,
                    };
                }

                return StatusCode(200, await _userService.Login(person, new Token()));

            }
            catch (EntityNotFound err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpGet]
        [Route("/users")]
        [Authorize(Roles = "User, Operator")]
        public async Task<ICollection<UserView>> Index()
        {
            return await _userService.All();
        }

        [HttpPost]
        [Route("/users")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            try
            {
                await _userService.Save(user);
                return StatusCode(201);
            }
            catch(EntityUniq err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpPut]
        [Route("/users/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            user.Id = id;
            try
            {
                await _userService.Save(user);
                return StatusCode(204);
            }
            catch(EntityUniq err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpDelete]
        [Route("/users/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.Delete(id);
                return StatusCode(204);
            }
            catch(EntityEmptyId err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
            catch(EntityNotFound err)
            {
                return StatusCode(404, new {
                    Message = err.Message
                });
            }
        }
    }
}
