using CustomerManagementService.BusinessServices.Interfaces;
using CustomerManagementService.DataServices.Models;
using CustomerManagementService.DataServices.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace CustomerManagementService.BusinessServices
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IAccountRepository accountRepository;
        private readonly ITransactionRepository transactionRepository;

        public CustomerAccountService(ICustomerRepository customerRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            this.customerRepository = customerRepository;
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
        }

        public async Task<Customer> GetCustomerAccountByAccountNumber(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ArgumentException("Invalid account number provided to service", "accountNumber");
            }

            var account = await accountRepository.GetAccountByAccountNumber(accountNumber);
            if (account == null)
            {
                return null;
            } 
            else
            {
                var customer = await customerRepository.GetCustomerByAccountId(account.Id);
                if (customer == null)
                {
                    return null;
                }
                else
                {
                    customer.Account = account;
                    return customer;
                }
            }
        }

        public async Task<Customer> OpenCustomerAccount(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Invalid customer detail provided to service", "customer");
            }

            var customer = new Customer();
            customer.FirstName = firstName;
            customer.LastName = lastName;

            customer.Account = new Account();

            var accountNumber = DateTime.Now.ToString("yyyyMMddHHss");

            customer.Account.AccountNumber = accountNumber;
            customer.Account.Balance = 0.0M;
            customer.Account.Status = AccountStatus.Open;

            await accountRepository.UpsertAccount(customer.Account);
            await customerRepository.UpsertCustomer(customer);
            return await GetCustomerAccountByAccountNumber(accountNumber);
        }

        public async Task<Customer> CloseCustomerAccount(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ArgumentException("Invalid account number provided to service", "accountNumber");
            }

            var account = await accountRepository.GetAccountByAccountNumber(accountNumber);

            account.Balance = 0.0M;
            account.Status = AccountStatus.Closed;

            await accountRepository.UpsertAccount(account);
            return await GetCustomerAccountByAccountNumber(accountNumber);
        }

        public async Task<Customer> ApplyTransactionToCustomerAccount(string accountNumber, decimal amount, TransactionType transactionType)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ArgumentException("Invalid account number provided to service", "accountNumber");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Invalid transaction amount provided to service", "amount");
            }

            var account = await accountRepository.GetAccountByAccountNumber(accountNumber);

            account.Balance = transactionType == TransactionType.Debit ? account.Balance - amount :
                account.Balance + amount;

            await accountRepository.UpsertAccount(account);
            var transaction = new Transaction
            {
                Amount = amount,
                Type = transactionType,
                AccountId = account.Id
            };
            await transactionRepository.UpsertTransaction(transaction);
            return await customerRepository.GetCustomerByAccountId(account.Id);
        }
    }
}
