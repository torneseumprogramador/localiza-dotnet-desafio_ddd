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
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
            _userService = new UserService(new PersonRepository(), new EntityRepository());
        }

        [HttpPost]
        [Route("/users/login")]
        [Route("/operators/login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {  
            try
            {
                return StatusCode(200, await _userService.Login(userLogin, new Token()));
            }
            catch (EntityNotFound err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }
    }
}
