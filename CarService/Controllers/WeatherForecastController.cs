using CarService.Models;
using CarService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly CoworkerService service;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, CoworkerService service)
        {
            _logger = logger;
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("coworkercounter")]
        public int GetCarCount()
        {
            return service.GetCoworkersNumber();
        }

        [HttpGet]
        [Route("getcoworker")]
        public Coworker GetCoworker(string email)
        {
            return service.GetCoworkerByEmail(email);
        }

        [HttpPost]
        [Route("phone/{coworkerId}")]
        public bool AddPhoneToCoworker([FromBody] Phone phone, [FromRoute] int coworkerId)
        {
            service.AddPhoneToCoworker(coworkerId, phone);
            return true;
        }
    }
}
