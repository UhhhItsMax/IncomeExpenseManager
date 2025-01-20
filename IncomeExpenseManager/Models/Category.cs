using System.ComponentModel.DataAnnotations;

namespace IncomeExpenseManager.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string? UserId { get; set; }
    }
}
