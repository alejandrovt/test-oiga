using Dapper;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using oiga.test.user.common;
using oiga.test.user.search.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace oiga.test.user.search.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [Topic("messagebus", "userregistered")]
        [HttpPost("register")]
        public void Register(UserSearch userSearch, [FromServices] ApplicationDbContext context)
        {
            Console.WriteLine("-----------------------------------------Search: Registered User 5------------");
            Console.WriteLine(userSearch.FullName);

            context.EntityUsersSearch.Add(userSearch);
            context.SaveChanges();
        }

        [HttpGet("search")]
        public async Task<IEnumerable<UserSearch>> Search(string query, [FromServices] ApplicationDbContext context, [FromServices]  IConfiguration configuration)
        {
            var sql = @$"select id, full_name FullName, user_name UserName
                         from usersearch";
            if (!string.IsNullOrEmpty(query))
            {
                query = Regex.Replace(query, @"[^\w\s.!@$%^&*()\-\/]+", "");
                var queryARR = query.Split(' ');
                var where = "upper(t.name) like upper('%" + string.Join("%') or upper(t.name) like upper('%", queryARR) + "%')";
                

                sql = @$"select s2.id, s2.full_name FullName, s2.user_name UserName
                        from (
	                        select t.id
	                        from (
		                        SELECT s.id, value name
		                        FROM usersearch s
		                        CROSS APPLY STRING_SPLIT(full_name, ' ')
	                        ) t
	                        where {where}
	                        group by t.id
                        ) t2
                        inner join usersearch s2 on s2.id=t2.id
                        order by s2.full_name, s2.user_name";
            }

            var users = context.ExecuteQuery<UserSearch>(sql, configuration);

            return users;
        }
    }
}
