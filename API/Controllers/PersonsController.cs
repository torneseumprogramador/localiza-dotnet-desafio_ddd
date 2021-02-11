using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Domain.UseCase.UserServices;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Services.Person;
using Infrastructure.Services;

namespace api.Controllers
{
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<PersonsController> _logger;

        public PersonsController(ILogger<PersonsController> logger)
        {
            _logger = logger;
            _userService = new UserService(new PersonRepository(), new EntityRepository());
        }

        [HttpGet]
        [Route("/persons")]
        [Authorize(Roles = "User, Operator")]
        public async Task<ICollection<Person>> Index()
        {
            return await _userService.All<Person>(PersonRole.User);
        }
    }
}