﻿using BL.Metadata;
using BL.Models;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface ICategoryService : IModifiableCrudService<Category, CategoryFilter, CategoryModifier>
    {
        Task<IList<TransactionCategoryList>> GroupTransactions(IList<Transaction> transactions, string defaultCategoryName);
        IList<TransactionCategoryList> GroupTransactionsForWallet(IList<Category> categories, IList<Transaction> transactions, string defaultCategoryName);
    }
}
