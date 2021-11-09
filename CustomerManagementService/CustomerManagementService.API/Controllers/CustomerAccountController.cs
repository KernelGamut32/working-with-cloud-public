using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CustomerManagementService.API.Models;
using CustomerManagementService.BusinessServices.Interfaces;
using CustomerManagementService.DataServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CustomerAccountController : ControllerBase
    {
        private readonly ICustomerAccountService customerAccountService;

        public CustomerAccountController(ICustomerAccountService customerAccountService)
        {
            this.customerAccountService = customerAccountService;
        }

        [HttpGet("GetCustomerAccountByAccountNumber")]
        public async Task<IActionResult> GetCustomerAccountByAccountNumberAsync(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ValidationException("Invalid account number provided to operation.");
            }

            var response = await customerAccountService.GetCustomerAccountByAccountNumber(accountNumber);
            return Ok(new OkObjectResult(response));
        }

        [HttpPost("OpenCustomerAccount")]
        public async Task<IActionResult> OpenCustomerAccountAsync(OpenAccountRequest openAccountRequest)
        {
            if (string.IsNullOrEmpty(openAccountRequest.FirstName) || string.IsNullOrEmpty(openAccountRequest.LastName))
            {
                throw new ValidationException("Invalid customer detail provided to operation.");
            }

            var response = await customerAccountService.OpenCustomerAccount(openAccountRequest.FirstName, openAccountRequest.LastName);
            return Ok(new OkObjectResult(response));
        }

        [HttpPost("CloseCustomerAccount")]
        public async Task<IActionResult> CloseCustomerAccountAsync(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ValidationException("Invalid account number provided to operation.");
            }

            var response = await customerAccountService.CloseCustomerAccount(accountNumber);
            return Ok(new OkObjectResult(response));
        }

        [HttpPost("ApplyTransactionToCustomerAccountAsync")]
        public async Task<IActionResult> ApplyTransactionToCustomerAccountAsync(TransactionRequest transactionRequest)
        {
            if (string.IsNullOrEmpty(transactionRequest.AccountNumber) || transactionRequest.Amount <= 0)
            {
                throw new ValidationException("Invalid transaction detail provided to operation.");
            }

            var response = await customerAccountService.ApplyTransactionToCustomerAccount(transactionRequest.AccountNumber, transactionRequest.Amount, transactionRequest.TransactionType);
            return Ok(new OkObjectResult(response));
        }
    }
}
