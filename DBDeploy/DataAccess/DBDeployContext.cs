using DBHosting.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DBHosting.DataAccess
{
    public class DBDeployContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DBDeployContext(DbContextOptions<DBDeployContext> options) : base(options)
        {

        }

    }
}
