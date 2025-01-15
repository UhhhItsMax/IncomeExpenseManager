using System;
using System.ComponentModel.DataAnnotations;

namespace IncomeExpenseManager.Models
{
    public abstract class TransactionBase
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        public string Description { get; set; }
        public bool IsRecurring { get; set; }

    }
}
