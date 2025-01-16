using IncomeExpenseManager.Data;
using IncomeExpenseManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncomeExpenseManager.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            // Fetch incomes and expenses
            var incomes = await _context.Incomes.ToListAsync();
            var expenses = await _context.Expenses.ToListAsync();

            // Combine them into a single list of TransactionBase
            var allTransactions = new List<TransactionBase>();
            allTransactions.AddRange(incomes);
            allTransactions.AddRange(expenses);

            // Sort them by date descending, for instance
            allTransactions = allTransactions
                .OrderByDescending(t => t.Date)
                .ToList();

            return View(allTransactions);
        }
    }
}
