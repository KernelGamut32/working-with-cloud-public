using CustomerManagementService.DataServices.Models;
using System.Threading.Tasks;

namespace CustomerManagementService.DataServices.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetTransactionById(int id);
        Task<Transaction> GetTransactionByAccountId(int accountId);
        Task<Transaction> UpsertTransaction(Transaction transaction);
    }
}
