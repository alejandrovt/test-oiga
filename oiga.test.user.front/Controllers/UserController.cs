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
        public async Task<IEnumerable<UserSearch>> Search(int page, string query, [FromServices] DaprClient daprClient)
        {
            var methodName = $"user/search?page={page}{(!string.IsNullOrEmpty(query) ? $"&query={query}" : "")}";
            return await daprClient.InvokeMethodAsync<List<UserSearch>>(HttpMethod.Get, "usersearch", methodName);
        }

        [HttpPost("register")]
        public async Task<bool> Register(User user, [FromServices] DaprClient daprClient)
        {
            await daprClient.PublishEventAsync("messagebus", "userregister", user);

            return true;
        }
    }
}
