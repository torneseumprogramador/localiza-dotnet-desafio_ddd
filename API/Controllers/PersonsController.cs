using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Domain.UseCase.UserServices;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Services;

namespace api.Controllers
{
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly EntityService _entityService;
        private readonly ILogger<PersonsController> _logger;

        public PersonsController(ILogger<PersonsController> logger)
        {
            _logger = logger;
            _entityService = new EntityService(new EntityRepository());
        }

        [HttpGet]
        [Route("/persons")]
        [Authorize(Roles = "Operator")]
        public async Task<ICollection<Person>> Index()
        {
            return await _entityService.All<Person>();
        }
    }
}