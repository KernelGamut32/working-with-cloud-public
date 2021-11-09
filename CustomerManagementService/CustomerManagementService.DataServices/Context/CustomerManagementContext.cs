using CustomerManagementService.DataServices.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService.DataServices.Context
{
    public class CustomerManagementContext : DbContext
    {
        public CustomerManagementContext(DbContextOptions<CustomerManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Account);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account);

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.AccountNumber)
                .IsUnique();
        }
    }
}
