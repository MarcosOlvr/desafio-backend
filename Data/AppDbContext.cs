using desafio_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_backend.Data
{
    public class AppDbContext : DbContext
    {   
        public AppDbContext(DbContextOptions options) : base(options) 
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.Payee)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.Payer)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
