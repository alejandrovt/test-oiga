using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using oiga.test.user.common;
using oiga.test.user.register.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace oiga.test.user.register.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [Topic("messagebus", "userregister")]
        [HttpPost("register")]
        public async Task Register(User user, 
            [FromServices] DaprClient daprClient, 
            [FromServices] ApplicationDbContext context)
        {
            context.EntityUsers.Add(user);
            context.SaveChanges();

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
