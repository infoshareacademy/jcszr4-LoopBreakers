using LoopBreakers.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LoopBreakers.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRoles, int>
    {
        public DbSet<Transfer> Transfers { get; set; }
        public override DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<ActivityReport> ActivityReport { get; set; }
        public DbSet<TransferReport> TransferReport { get; set; }
        public DbSet<AppError> ExceptionsStorage { get; set; }

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
