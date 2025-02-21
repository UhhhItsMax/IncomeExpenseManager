using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IncomeExpenseManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public decimal CurrentBalance { get; set; }
        public string Currency { get; set; }
    }
}
