using CustomerManagementService.DataServices.Context;
using CustomerManagementService.DataServices.Models;
using CustomerManagementService.DataServices.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CustomerManagementService.DataServices.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CustomerManagementContext customerManagementContext;

        public AccountRepository(CustomerManagementContext customerManagementContext)
        {
            this.customerManagementContext = customerManagementContext;
        }

        public async Task<Account> GetAccountById(int id)
        {
            return await customerManagementContext.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account> GetAccountByAccountNumber(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ArgumentException("Invalid account number provided to get operation", "accountNumber");
            }

            return await customerManagementContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber.ToLower() == accountNumber.ToLower());
        }

        public async Task<Account> UpsertAccount(Account account)
        {
            if (account == null)
            {
                throw new ArgumentException("Invalid account object provided to upsert operation", "account");
            }

            var accountId = account.Id;
            var existingAccount = await GetAccountById(accountId);
            if (existingAccount == null)
            {
                accountId = await InsertAccount(account);
            }
            else
            {
                accountId = await UpdateAccount(existingAccount, account);
            }
            return await GetAccountById(accountId);
        }

        private async Task<int> InsertAccount(Account account)
        {
            customerManagementContext.Accounts.Add(account);
            await customerManagementContext.SaveChangesAsync();
            return account.Id;
        }

        private async Task<int> UpdateAccount(Account existingAccount, Account account)
        {
            existingAccount.AccountNumber = account.AccountNumber;
            existingAccount.Balance = account.Balance;
            existingAccount.Status = account.Status;
            await customerManagementContext.SaveChangesAsync();
            return existingAccount.Id;
        }
    }
}
