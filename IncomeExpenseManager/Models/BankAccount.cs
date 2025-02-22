using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeExpenseManager.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal CurrentBalance { get; set; }
        public string? UserId { get; set; }
    }
}
