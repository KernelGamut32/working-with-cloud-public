using CustomerManagementService.DataServices.Models;

namespace CustomerManagementService.API.Models
{
    public class TransactionRequest
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
