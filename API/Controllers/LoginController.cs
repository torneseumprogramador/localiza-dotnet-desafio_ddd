using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.RepositoryServices.Person;
using Infrastructure.RepositoryServices;
using API.Authentication;
using Domain.UseCase;
using Infrastructure.RepositoryServices.Exceptions;

namespace api.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly PersonService _personService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
            _personService = new PersonService(new PersonRepository(), new EntityRepository());
        }

        [HttpPost]
        [Route("/pessoa/login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(PersonLogin userLogin)
        {  
            try
            {
                return StatusCode(200, await _personService.Login(userLogin, new Token()));
            }
            catch (EntityNotFound err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpPost]
        [Route("/usuario/login")]
        [AllowAnonymous]
        public async Task<ActionResult> UserLogin(UserLogin userLogin)
        {
            try
            {
                return StatusCode(200, await _personService.Login(userLogin, new Token()));
            }
            catch (EntityNotFound err)
            {
                return StatusCode(401, new
                {
                    Message = err.Message
                });
            }
        }

        [HttpPost]
        [Route("/operador/login")]
        [AllowAnonymous]
        public async Task<ActionResult> OperatorLogin(OperatorLogin userLogin)
        {
            try
            {
                return StatusCode(200, await _personService.Login(userLogin, new Token()));
            }
            catch (EntityNotFound err)
            {
                return StatusCode(401, new
                {
                    Message = err.Message
                });
            }
        }
    }
}
