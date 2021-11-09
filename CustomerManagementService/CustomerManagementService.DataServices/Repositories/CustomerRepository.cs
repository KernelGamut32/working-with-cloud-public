using CustomerManagementService.DataServices.Context;
using CustomerManagementService.DataServices.Models;
using CustomerManagementService.DataServices.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CustomerManagementService.DataServices.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerManagementContext customerManagementContext;

        public CustomerRepository(CustomerManagementContext customerManagementContext)
        {
            this.customerManagementContext = customerManagementContext;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await customerManagementContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> GetCustomerByAccountId(int accountId)
        {
            return await customerManagementContext.Customers.FirstOrDefaultAsync(c => c.AccountId == accountId);
        }

        public async Task<Customer> UpsertCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentException("Invalid customer object provided to upsert operation", "customer");
            }

            var customerId = customer.Id;
            var existingCustomer = await GetCustomerById(customerId);
            if (existingCustomer == null)
            {
                customerId = await InsertCustomer(customer);
            }
            else
            {
                customerId = await UpdateCustomer(existingCustomer, customer);
            }
            return await GetCustomerById(customerId);
        }

        private async Task<int> InsertCustomer(Customer customer)
        {
            customerManagementContext.Customers.Add(customer);
            await customerManagementContext.SaveChangesAsync();
            return customer.Id;
        }

        private async Task<int> UpdateCustomer(Customer existingCustomer, Customer customer)
        {
            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            if (customer.Account?.Id > 0)
            {
                existingCustomer.AccountId = customer.Account.Id;
            }
            await customerManagementContext.SaveChangesAsync();
            return existingCustomer.Id;
        }
    }
}
