using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using oiga.test.user.common;
using System;
using System.Threading.Tasks;

namespace oiga.test.user.register.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [Topic("messagebus", "userregister")]
        [HttpPost("register")]
        public async Task Register(User user, [FromServices] DaprClient daprClient)
        {
            Console.WriteLine("-----------------------------------------User Register 1------------");
            Console.WriteLine(user.FullName);

            var userSearch = new UserSearch
            {
                FullName = user.FullName,
                Id = user.Id,
                UserName = user.UserName
            };

            await daprClient.PublishEventAsync("messagebus", "userregistered", userSearch);
        }
    }
}
