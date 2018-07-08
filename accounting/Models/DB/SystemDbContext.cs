using System;
using Microsoft.EntityFrameworkCore;

namespace Accounting.WS.Models.DB
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options)
            : base(options)
        {
        }

        // site stuff
        public DbSet<Site> Site { get; set; }

        // expenditure stuff
        public DbSet<Expenditure> Expenditure { get; set; }
        public DbSet<ExpenditureReFund> ExpenditureReFund { get; set; }

        // income stuff
        public DbSet<Income> Income { get; set; }
    }
}