using JfService.Balance.Application.DbContexts;
using JfService.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JfService.Balance.Infrastructure
{
    public class JfServiceDbContext : DbContext, IJfServiceDbContext
    {
        public DbSet<Domain.Entities.Balance> Balances { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public JfServiceDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=jfservicedb;Trusted_Connection=True;");
        }
    }
}