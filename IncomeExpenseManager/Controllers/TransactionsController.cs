using IncomeExpenseManager.Data;
using IncomeExpenseManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



namespace IncomeExpenseManager.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TransactionsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Transactions
        public async Task<IActionResult> Index(
            string typeFilter,
            string sortOrder,
            string searchString)
        {
            // Getr the current user's ID
            var userId = _userManager.GetUserId(User);

            // Fetch incomes and expenses
            var incomes = await _context
                .Incomes
                .ToListAsync();
            var expenses = await _context
                .Expenses
                .ToListAsync();

            // Combine them into a single list of TransactionBase
            var allTransactions = new List<TransactionBase>();
            allTransactions.AddRange(incomes);
            allTransactions.AddRange(expenses);

            if (!string.IsNullOrEmpty(typeFilter))
            {
                if (typeFilter == "Income")
                {
                    allTransactions = allTransactions
                        .Where(t => t is Income)
                        .ToList();
                }
                else if (typeFilter == "Expense")
                {
                    allTransactions = allTransactions
                        .Where(t => t is Expense)
                        .ToList();
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                allTransactions = allTransactions
                    .Where(t => t.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            switch (sortOrder)
            {
                case "date_asc":
                    allTransactions = allTransactions
                        .OrderBy(t => t.Date)
                        .ToList();
                    break;

                case "date_desc":
                    allTransactions = allTransactions
                        .OrderByDescending(t => t.Date)
                        .ToList();
                    break;

                case "type_asc":    // Income First
                    allTransactions = allTransactions
                        .OrderBy(t => t is Expense)
                        .ThenByDescending(t => t.Date)
                        .ToList();
                    break;

                case "type_desc":   // Expense First
                    allTransactions = allTransactions
                        .OrderBy(t => t is Income)
                        .ThenByDescending(t => t.Date)
                        .ToList();
                    break;

                default:
                    allTransactions = allTransactions
                        .OrderByDescending(t => t.Date)
                        .ToList();
                    break;
            }

            decimal totalBalance = 0;


            return View(allTransactions);
        }
    }
}
