using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Services;
using Infrastructure.Services.Exceptions;
using Domain.UseCase.UserServices;

namespace api.Controllers
{
    [ApiController]
    public class ChecklistController : ControllerBase
    {
        private readonly EntityService _entityService;
        private readonly ILogger<ChecklistController> _logger;

        public ChecklistController(ILogger<ChecklistController> logger)
        {
            _logger = logger;
            _entityService = new EntityService(new EntityRepository());
        }

        [HttpGet]
        [Route("/categories")]
        [Authorize(Roles = "Operator, User")]
        public async Task<ICollection<Checklist>> Index()
        {
            return await _entityService.All<Checklist>();
        }

        [HttpPost]
        [Route("/categories")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] Checklist checklist)
        {
            try
            {
                await _entityService.Save(checklist);
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
        [Route("/categories/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update(int id, [FromBody] Checklist checklist)
        {
            checklist.Id = id;
            try
            {
                await _entityService.Save(checklist);
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
        [Route("/categories/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _entityService.Delete<Checklist>(id);
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
