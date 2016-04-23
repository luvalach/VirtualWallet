﻿using BL.Mapping;
using FioSdkCsharp;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Filter = BL.Filters.TransactionFilter;
using FioFilter = FioSdkCsharp.TransactionFilter;

namespace BL.Models
{
    public class Fio : Bank
    {
        private const string fioResource = "FioBankToken";
        private const string fioUser = "FioUser";
        private const uint syncTimeOutinSec = 30;
        private BankAccountInfo bankAccountInfo;
        private static DateTime nextPossibleSyncTime;

        public string Token { get; set; }

        public override bool HasCredentials { get { return !string.IsNullOrEmpty(Token); } }

        public override CredentialType CredentialType
        {
            get { return CredentialType.Token; }
        }

        public override BankAccountInfo BankAccountInfo { get { return bankAccountInfo; } }

        public override DateTime NextPossibleSyncTime { get { return nextPossibleSyncTime; } }

        public Fio()
        {
            var credentials = GetCredentials();
            if (credentials != null)
            {
                credentials.RetrievePassword();
                Token = credentials.Password;
            }
        }

        public override async Task<IList<Transaction>> GetNewTransactionsAsync()
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidOperationException("Fio bank token has not been set");

            ApiExplorer explorer = new ApiExplorer(Token);
            var statement = await explorer.LastAsync();
            nextPossibleSyncTime = DateTime.Now.AddSeconds(syncTimeOutinSec);

            return GetTransactions(statement);
        }

        public override async Task<IList<Transaction>> GetTransactionsAsync(Filter filter)
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidOperationException("Fio bank token has not been set");

            ApiExplorer explorer = new ApiExplorer(Token);
            var statement = await explorer.PeriodsAsync(FioFilter.LastDays(filter.Days));
            nextPossibleSyncTime = DateTime.Now.AddSeconds(syncTimeOutinSec);

            return GetTransactions(statement);
        }

        public override async Task SetLastDownloadDateAsync(DateTime date)
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidOperationException("Fio bank token has not been set");

            ApiExplorer explorer = new ApiExplorer(Token);
            await explorer.SetLastDownloadDateAsync(date);
        }

        public override void SaveCredentials()
        {
            if (string.IsNullOrEmpty(Token))
                return;

            var password = new PasswordCredential(fioResource, fioUser, Token);
            PasswordVault.Add(password);
        }

        public override void SetCredentials(string token = null, string login = null, string password = null)
        {
            Token = token;
        }

        public override void RemoveCredentials()
        {
            var credentials = GetCredentials();
            if (credentials != null)
                PasswordVault.Remove(credentials);
        }

        private PasswordCredential GetCredentials()
        {
            return PasswordVault.RetrieveAll().FirstOrDefault(x => x.Resource == fioResource && x.UserName == fioUser);
        }

        private IList<Transaction> GetTransactions(FioSdkCsharp.Models.AccountStatement accountStatement)
        {
            bankAccountInfo = MapperInstance.Mapper.Map<BankAccountInfo>(accountStatement.Info);

            var transactions = new List<Transaction>();
            foreach (var transaction in accountStatement.TransactionList.Transactions)
            {
                transactions.Add(new Transaction()
                {
                    Id = (int)transaction.Id.Value,
                    Amount = transaction.Amount?.Value ?? 0,
                    Source = this,
                    Description = transaction.Comment?.Value,
                    Date = transaction.Date?.Value ?? DateTime.Now,
                    Currency = transaction.Currency?.Value
                });
            }

            return transactions;
        }
    }
}
