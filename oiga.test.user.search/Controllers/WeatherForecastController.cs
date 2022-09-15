using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using oiga.test.user.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr;

namespace oiga.test.user.search.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Test", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
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

        [Topic("messagebus", "userregistered")]
        [HttpPost("userregistered")]
        public void UserRegistered(UserSearch userSearch)
        {
            Console.WriteLine("-----------------------------------------Search Registes User 1------------");
            Console.WriteLine(userSearch.FullName);
        }
    }
}
