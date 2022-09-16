using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using oiga.test.user.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace oiga.test.user.register.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get([FromServices] DaprClient daprClient)
        {
            var userSearch = new UserSearch{ 
                FullName= "Alejandro Villada Trejos",
                Id = 1,
                UserName = "alejandrovt"
            };

            Console.WriteLine("-----------------------------------------Register 4------------");
            Console.WriteLine(userSearch.FullName);

            await daprClient.PublishEventAsync("messagebus", "userregistered", userSearch);

            return await daprClient.InvokeMethodAsync<List<WeatherForecast>>(HttpMethod.Get, "usersearch", "weatherforecast");
        }
    }
}
