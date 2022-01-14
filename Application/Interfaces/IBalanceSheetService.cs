using JfService.Balance.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace JfService.Balance.Application.Interfaces
{
    public interface IBalanceSheetService
    {
        /// <summary>
        /// Возвращает оборотную ведомость.
        /// </summary>
        /// <param name="accountId">Идентификатор ЛС.</param>
        /// <param name="ct">Токен отмены асинхронной операции.</param>
        /// <returns>Оборотная ведомость по периодам.</returns>
        public Task<BalanceSheet> GetBalanceSheetAsync(long accountId, CancellationToken ct = default);

        /// <summary>
        /// Возвращает текущую задолженность.
        /// </summary>
        /// <param name="accountId">Идентификатор ЛС.</param>
        /// <param name="ct">Токен отмены асинхронной операции.</param>
        public Task<decimal> GetCurrentDebtAsync(long accountId, CancellationToken ct = default);
    }
}