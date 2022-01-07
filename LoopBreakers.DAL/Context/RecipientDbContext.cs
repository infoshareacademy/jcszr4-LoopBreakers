using System;
using System.Collections.Generic;
using System.Text;
using LoopBreakers.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoopBreakers.DAL.Context
{
    public class RecipientDbContext:DbContext
    {
        public DbSet<Recipient> Recipient { get; set; }

        public RecipientDbContext(DbContextOptions<RecipientDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipient>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
