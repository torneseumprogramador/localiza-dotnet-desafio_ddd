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
    public class BrandsController : ControllerBase
    {
        private readonly EntityService _entityService;
        private readonly ILogger<BrandsController> _logger;

        public BrandsController(ILogger<BrandsController> logger)
        {
            _logger = logger;
            _entityService = new EntityService(new EntityRepository());
        }

        [HttpGet]
        [Route("/brands")]
        [Route("/marcas")]
        [Authorize(Roles = "User, Operator")]
        public async Task<ICollection<Brand>> Index()
        {
            return await _entityService.All<Brand>();
        }

        [HttpPost]
        [Route("/brands")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] Brand brand)
        {
            try
            {
                await _entityService.Save(brand);
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
        [Route("/brands/{id}")]
        [Route("/marcas/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update(int id, [FromBody] Brand brand)
        {
            brand.Id = id;
            try
            {
                await _entityService.Save(brand);
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
        [Route("/brands/{id}")]
        [Route("/marcas/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _entityService.Delete<Brand>(id);
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
