using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Transfer> Transfers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transfer>().Property(x => x.Amount).HasPrecision(19, 4);

            base.OnModelCreating(modelBuilder);
        }
    }
}
