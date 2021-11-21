using Microsoft.EntityFrameworkCore;

namespace OWASP10_2021.Data
{
    public class AccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseInMemoryDatabase("Accounts");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity => { entity.Property(e => e.AccountId).IsRequired(); });
            modelBuilder.Entity<Account>(entity => { entity.Property(e => e.SSN).IsRequired(); });

            modelBuilder.Entity<Account>().HasData(
                Account.SeedData()
            );
        }
    }
}
