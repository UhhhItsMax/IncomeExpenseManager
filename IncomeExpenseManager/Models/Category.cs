using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeExpenseManager.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string? UserId { get; set; }

        public ICollection<CategoryVariable> Variables { get; set; } = new List<CategoryVariable>();
        public ICollection<TransactionBase> Transactions { get; set; } = new List<TransactionBase>();


    }
}
