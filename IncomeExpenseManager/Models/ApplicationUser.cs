using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeExpenseManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public decimal CurrentBalance { get; set; }
        public string Currency { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
    }
}
