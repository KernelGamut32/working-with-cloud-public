using CustomerManagementService.DataServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagementService.DataServices.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(int id);
        Task<Customer> GetCustomerByAccountId(int accountId);
        Task<Customer> UpsertCustomer(Customer customer);
        Task<IList<Customer>> GetAllCustomers();
    }
}
