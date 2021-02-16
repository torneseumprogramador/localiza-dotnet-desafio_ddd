using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.RepositoryServices;
using Infrastructure.RepositoryServices.Exceptions;
using Domain.UseCase.UserServices;

namespace api.Controllers
{
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly EntityService _entityService;
        private readonly ILogger<ModelsController> _logger;

        public ModelsController(ILogger<ModelsController> logger)
        {
            _logger = logger;
            _entityService = new EntityService(new EntityRepository());
        }

        [HttpGet]
        [Route("/modelos")]
        [Authorize(Roles = "User, Operator")]
        public async Task<ICollection<Model>> Index()
        {
            return await _entityService.All<Model>();
        }

        [HttpPost]
        [Route("/modelos")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] Model model)
        {
            try
            {
                await _entityService.Save(model);
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
        [Route("/modelos/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update(int id, [FromBody] Model model)
        {
            model.Id = id;
            try
            {
                await _entityService.Update(model);
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
        [Route("/modelos/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _entityService.Delete<Model>(id);
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
