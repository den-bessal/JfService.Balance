using JfService.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace JfService.Balance.Application.DbContexts
{
    public interface IJfServiceDbContext
    {
        /// <summary>
        /// Сохраняет все изменения в данном контексте.
        /// </summary>
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DatabaseFacade Database { get; }

        /// <summary>
        /// Произведенные начисления.
        /// </summary>
        public DbSet<Domain.Entities.Balance> Balances { get; set; }

        /// <summary>
        /// Произведенные платежи.
        /// </summary>
        public DbSet<Payment> Payments { get; set; }
    }
}