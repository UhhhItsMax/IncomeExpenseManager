using IncomeExpenseManager.Data;
using IncomeExpenseManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IncomeExpenseManager.ViewModels;



namespace IncomeExpenseManager.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _domainContext;
        private readonly UserManager<IdentityUser> _userManager;

        public TransactionsController(ApplicationDbContext domainContext, UserManager<IdentityUser> userManager)
        {
            _domainContext = domainContext;
            _userManager = userManager;
        }

        // GET: Transactions
        public async Task<IActionResult> Index(
            string yearSearch,
            string monthSearch,
            string typeFilter,
            string sortOrder,
            string searchString)
        {
            // Getr the current user's ID
            var userId = _userManager.GetUserId(User);

            var transactionsQuery = _domainContext.Transactions
                .Include(t => t.Category)
                .Where(t => t.UserId == userId)
                .Where(t => t.Date.Year.ToString() == yearSearch || string.IsNullOrEmpty(yearSearch))
                .Where(t => t.Date.Month.ToString() == monthSearch || string.IsNullOrEmpty(monthSearch))
                .AsQueryable();

            if (!string.IsNullOrEmpty(typeFilter))
            {
                if (typeFilter == "Income")
                {
                    transactionsQuery = transactionsQuery.OfType<Income>();
                }
                else if (typeFilter == "Expense")
                {
                    transactionsQuery = transactionsQuery.OfType<Expense>();
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                transactionsQuery = transactionsQuery
                    .Where(t => t.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase));
            }

            transactionsQuery = sortOrder switch
            {
                "date_asc" => transactionsQuery
                    .OrderBy(t => t.Date),
                "date_desc" => transactionsQuery
                    .OrderByDescending(t => t.Date),
                "type_asc" => transactionsQuery
                    .OrderBy(t => t is Expense)
                    .ThenByDescending(t => t.Date),
                "type_desc" => transactionsQuery
                    .OrderBy(t => t is Income)
                    .ThenByDescending(t => t.Date),
                _ => transactionsQuery
                    .OrderByDescending(t => t.Date)
            };

            var transactions = await transactionsQuery.ToListAsync();

            decimal totalIncome = transactions.OfType<Income>().Sum(i => i.Amount);
            decimal totalExpense = transactions.OfType<Expense>().Sum(e => e.Amount);
            decimal totalBalance = totalIncome - totalExpense;

            var viewModel = new TransactionsViewModel
            {
                Transactions = transactions,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                TotalBalance = totalBalance,
                YearSearch = yearSearch,
                MonthSearch = monthSearch
            };


            return View(viewModel);
        }
    }
}
