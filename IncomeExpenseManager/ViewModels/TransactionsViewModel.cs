using IncomeExpenseManager.Models;
using System.ComponentModel.DataAnnotations;

namespace IncomeExpenseManager.ViewModels
{
    public class TransactionsViewModel
    {
        [Required]
        public IEnumerable<TransactionBase> Transactions { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
