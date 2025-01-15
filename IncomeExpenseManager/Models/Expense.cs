using System.ComponentModel.DataAnnotations;

namespace IncomeExpenseManager.Models
{
    public class Expense : TransactionBase
    {
        public string Vendor { get; set; }
    }
}
