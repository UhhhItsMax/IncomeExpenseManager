using System.ComponentModel.DataAnnotations;

namespace IncomeExpenseManager.Models
{
    public class Income : TransactionBase
    {
        public string Source { get; set; }
    }
}
