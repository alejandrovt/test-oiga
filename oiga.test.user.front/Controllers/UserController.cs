using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using oiga.test.user.common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace oiga.test.user.front.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("search")]
        public async Task<IEnumerable<UserSearch>> Search(string query, [FromServices] DaprClient daprClient)
        {
            var methodName = $"user/search?query={query}";
            //return await daprClient.InvokeMethodAsync<List<UserSearch>>(HttpMethod.Get, "usersearch", "user/search");
            return await daprClient.InvokeMethodAsync<List<UserSearch>>(HttpMethod.Get, "usersearch", methodName);
        }
    }
}
