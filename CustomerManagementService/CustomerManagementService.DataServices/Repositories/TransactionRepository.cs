using CustomerManagementService.DataServices.Context;
using CustomerManagementService.DataServices.Models;
using CustomerManagementService.DataServices.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CustomerManagementService.DataServices.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly CustomerManagementContext customerManagementContext;

        public TransactionRepository(CustomerManagementContext customerManagementContext)
        {
            this.customerManagementContext = customerManagementContext;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await customerManagementContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Transaction> GetTransactionByAccountId(int accountId)
        {
            return await customerManagementContext.Transactions.FirstOrDefaultAsync(t => t.AccountId == accountId);
        }

        public async Task<Transaction> UpsertTransaction(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentException("Invalid transaction object provided to upsert operation", "transaction");
            }

            var transactionId = transaction.Id;
            var existingTransaction = await GetTransactionById(transactionId);
            if (existingTransaction == null)
            {
                transactionId = await InsertTransaction(transaction);
            }
            else
            {
                transactionId = await UpdateTransaction(existingTransaction, transaction);
            }
            return await GetTransactionById(transactionId);
        }

        private async Task<int> InsertTransaction(Transaction transaction)
        {
            transaction.Account = null;
            customerManagementContext.Transactions.Add(transaction);
            await customerManagementContext.SaveChangesAsync();
            return transaction.Id;
        }

        private async Task<int> UpdateTransaction(Transaction existingTransaction, Transaction transaction)
        {
            existingTransaction.Account = null;
            existingTransaction.Amount = transaction.Amount;
            existingTransaction.Type = transaction.Type;
            if (transaction.Account?.Id > 0)
            {
                existingTransaction.AccountId = transaction.Account.Id;
            }
            await customerManagementContext.SaveChangesAsync();
            return existingTransaction.Id;
        }
    }
}
