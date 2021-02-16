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
    public class CategoriesController : ControllerBase
    {
        private readonly EntityService _entityService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger;
            _entityService = new EntityService(new EntityRepository());
        }

        [HttpGet]
        [Route("/categorias")]
        [Authorize(Roles = "User, Operator")]
        public async Task<ICollection<Category>> Index()
        {
            return await _entityService.All<Category>();
        }

        [HttpPost]
        [Route("/categorias")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            try
            {
                await _entityService.Save(category);
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
        [Route("/categorias/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {
            category.Id = id;
            try
            {
                await _entityService.Update(category);
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
        [Route("/categorias/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _entityService.Delete<Category>(id);
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
