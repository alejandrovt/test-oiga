using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using oiga.test.user.common;
using System.Collections.Generic;
using System.Linq;

namespace oiga.test.user.search.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<UserSearch> EntityUsersSearch { get; set; }

        public List<T> ExecuteQuery<T>(string sql, IConfiguration configuration)
        {
            using var con = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            con.Open();
            var data = con.Query<T>(sql).ToList();

            return data;
        }
    }
}
