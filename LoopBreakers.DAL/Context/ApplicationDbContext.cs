using LoopBreakers.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace LoopBreakers.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Recipient> Recipients { get; set; }

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
