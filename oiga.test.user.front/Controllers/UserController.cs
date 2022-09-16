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
            var queryData = string.IsNullOrEmpty(query) ? " " : query;
            var methodName = $"user/search?page={page}&query={queryData}";

            Console.WriteLine("-----------------------------------------2. Front Search------------");
            Console.WriteLine(methodName);

            return await daprClient.InvokeMethodAsync<List<UserSearch>>(HttpMethod.Get, "usersearch", methodName);
        }

        [HttpGet("getbyid")]
        public async Task<User> GetById(int id, [FromServices] DaprClient daprClient)
        {
            var methodName = $"user/getbyid?id={id}";
            return await daprClient.InvokeMethodAsync<User>(HttpMethod.Get, "userregister", methodName);
        }

        [HttpPost("register")]
        public async Task<string> Register(User user, [FromServices] DaprClient daprClient)
        {
            await daprClient.PublishEventAsync("messagebus", "userregister", user);

            return string.Empty;
        }
    }
}
