﻿using System.Collections.Generic;
using DAL.Data;
using Shared.Filters;
using DAL.Helpers;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace DAL.DataAccess
{
    public class Transactions : BaseCrudDataAccess<Transaction, TransactionFilter>, ITransactions
    {
        public async Task<IList<Transaction>> GetByBankIdAsync(int? bankId, TransactionFilter filter = null)
        {
            if (filter == null)
                filter = new TransactionFilter();

            var connection = ConnectionHelper.GetDbAsyncConnection();
            var query = connection.Table<Transaction>().Where(x => x.BankId == bankId);

            return await ApplyFilters(query, filter).ToListAsync();
        }

        protected override AsyncTableQuery<Transaction> ApplyFilters(AsyncTableQuery<Transaction> query, TransactionFilter filter)
        {
            if (filter.DateSince.HasValue)
                query = query.Where(x => x.Date >= filter.DateSince.Value);

            if (filter.IsCashPayment)
                query = query.Where(x => x.BankId == null);

            return base.ApplyFilters(query, filter);
        }
    }
}
