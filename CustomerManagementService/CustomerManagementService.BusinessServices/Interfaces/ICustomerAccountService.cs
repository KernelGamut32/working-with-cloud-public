using CustomerManagementService.DataServices.Models;
using System.Threading.Tasks;

namespace CustomerManagementService.BusinessServices.Interfaces
{
    public interface ICustomerAccountService
    {
        Task<Customer> GetCustomerAccountByAccountNumber(string accountNumber);
        Task<Customer> OpenCustomerAccount(string firstName, string lastName);
        Task<Customer> CloseCustomerAccount(string accountNumber);
        Task<Customer> ApplyTransactionToCustomerAccount(string accountNumber, decimal amount, TransactionType transactionType);
    }
}
