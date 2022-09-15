using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using oiga.test.user.common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace oiga.test.user.search.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [Topic("messagebus", "userregistered")]
        [HttpPost("register")]
        public void Register(UserSearch userSearch)
        {
            Console.WriteLine("-----------------------------------------Search: Registered User 1------------");
            Console.WriteLine(userSearch.FullName);
        }

        [Topic("messagebus", "usersearch")]
        [HttpGet("search")]
        public async Task<IEnumerable<UserSearch>> Search(string query, [FromServices] DaprClient daprClient)
        {
            var user1 = new UserSearch
            {
                FullName = "Alejandro Villada Trejos",
                Id = "1",
                UserName = "alejandrovt"
            };
            var user2 = new UserSearch
            {
                FullName = "Yenny Betancur Perez",
                Id = "2",
                UserName = "yennybet"
            };

            Console.WriteLine("-----------------------------------------Search: Search Users 1------------");
            Console.WriteLine(query);

            List<UserSearch> users = new();
            users.Add(user1);
            users.Add(user2);

            return users;
        }
    }
}
