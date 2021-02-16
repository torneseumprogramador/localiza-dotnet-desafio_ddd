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
using Domain.ViewModel;
using Domain.UseCase;
using Infrastructure.PdfServices;

namespace api.Controllers
{
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly EntityService _entityService;
        private readonly ScheduleService _scheduleService;
        private readonly ILogger<SchedulesController> _logger;

        public SchedulesController(ILogger<SchedulesController> logger)
        {
            _logger = logger;
            _entityService = new EntityService(new EntityRepository());
            _scheduleService = new ScheduleService(new EntityRepository());
        }

        [HttpGet]
        [Route("/agendamentos")]
        [Authorize(Roles = "User, Operator")]
        public async Task<ICollection<Schedule>> Index()
        {
            return await _entityService.All<Schedule>();
        }

        [HttpPost]
        [Route("/agendamento/simulacao")]
        [Authorize(Roles = "User, Operator")]
        public async Task<IActionResult> Simulation([FromBody] VehicleScheduleSimulationInput schedule)
        {
            try
            {
                return StatusCode(200, await _scheduleService.Simulation(schedule));
            }
            catch (EntityNotFound err)
            {
                return StatusCode(403, new
                {
                    Message = err.Message
                });
            }
            catch (EntityUniq err)
            {
                return StatusCode(401, new
                {
                    Message = err.Message
                });
            }
        }

        [HttpPost]
        [Route("/agendamento/alugar")]
        [Authorize(Roles = "Operator, User")]
        public async Task<IActionResult> BookCar([FromBody] ScheduleInput schedule)
        {
            try
            {
                var scheduleOut = await _scheduleService.BookCar(schedule, new PdfWriter());
                return StatusCode(201, scheduleOut);
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
        [Route("/agendamento/devolucao")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> ReturnPayment([FromBody] Checklist checklist)
        {
            try
            {
                var schedulePaymentOut = await _scheduleService.ReturnPayment(checklist, new PdfWriter());
                return StatusCode(201, schedulePaymentOut);
            }
            catch (EntityNotFound err)
            {
                return StatusCode(401, new
                {
                    Message = err.Message
                });
            }
        }


        [HttpDelete]
        [Route("/agendamentos/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _entityService.Delete<Schedule>(id);
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