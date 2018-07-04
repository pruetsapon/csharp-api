using System;
using Microsoft.EntityFrameworkCore;

namespace Webapi.DB.Model
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
    }
}