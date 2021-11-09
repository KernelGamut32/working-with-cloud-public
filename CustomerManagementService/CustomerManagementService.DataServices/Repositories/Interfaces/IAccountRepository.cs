using CustomerManagementService.DataServices.Models;
using System.Threading.Tasks;

namespace CustomerManagementService.DataServices.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountById(int id);
        Task<Account> GetAccountByAccountNumber(string accountNumber);
        Task<Account> UpsertAccount(Account account);
    }
}
