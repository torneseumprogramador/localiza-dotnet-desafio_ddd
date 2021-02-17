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
    public class VehiclesController : ControllerBase
    {
        private readonly EntityService _entityService;
        private readonly ILogger<VehiclesController> _logger;

        public VehiclesController(ILogger<VehiclesController> logger)
        {
            _logger = logger;
            _entityService = new EntityService(new EntityRepository());
        }

        [HttpGet]
        [Route("/veiculos")]
        [Authorize(Roles = "User, Operator")]
        public async Task<ICollection<Vehicle>> Index()
        {
            return await _entityService.All<Vehicle>();
        }

        [HttpGet]
        [Route("/veiculos/disponiveis")]
        [Authorize(Roles = "User, Operator")]
        public async Task<ICollection<Vehicle>> Available()
        {
            return await _entityService.All<Vehicle>("sp_getVeiclesAvailable");
        }

        [HttpPost]
        [Route("/veiculos")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] Vehicle vehicle)
        {
            try
            {
                await _entityService.Save(vehicle);
                return StatusCode(201);
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
        [Route("/veiculos/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update(int id, [FromBody] Vehicle vehicle)
        {
            vehicle.Id = id;
            try
            {
                await _entityService.Update(vehicle);
                return StatusCode(204);
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
        [Route("/veiculos/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _entityService.Delete<Vehicle>(id);
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
