using JfService.Balance.Application.Models;
using JfService.Balance.Application.Usecases.Balances.ViewModels;
using System;
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
        /// <exception cref="ArgumentException"/>
        public Task<BalanceSheet> GetAsync(long accountId, CancellationToken ct = default);
    }
}