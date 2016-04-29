﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BL.Models;
using DAL.DataAccess;
using Shared.Filters;
using BL.Mapping;

namespace BL.Service
{
    public class TransactionService : BaseCrudService<Transaction, DAL.Data.Transaction, Transactions, BaseFilter>, ITransactionService
    {
        public async Task<IList<Transaction>> GetByBankIdAsync(int? bankId)
        {
            return MapperInstance.Mapper.Map<IList<Transaction>>(await _instance.GetByBankIdAsync(bankId));
        }
    }
}
