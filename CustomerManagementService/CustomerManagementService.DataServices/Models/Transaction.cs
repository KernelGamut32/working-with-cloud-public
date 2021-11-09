using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagementService.DataServices.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public int AccountId { get; set; }
    }
}
