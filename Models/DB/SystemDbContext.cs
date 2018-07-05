using System;
using Microsoft.EntityFrameworkCore;

namespace Webapi.Models.DB
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Site> Site {get;set;}
    }
}