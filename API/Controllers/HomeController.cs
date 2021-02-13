using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ViewModel;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        [AllowAnonymous]
        public Welcome Index()
        {
            return new Welcome();
        }

        [HttpGet]
        [Route("/config/start")]
        [AllowAnonymous]
        public async Task<string> InicialConfig()
        {
            var sql = new SqlDriver();
            //await sql.CreateTable<Person>();
            //await sql.CreateTable<Address>();
            //await sql.CreateTable<Model>();
            //await sql.CreateTable<Brand>();

            return "Sistema configurado";
        }
    }
}
