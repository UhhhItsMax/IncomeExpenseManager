using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IncomeExpenseManager.Data;
using IncomeExpenseManager.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IncomeExpenseManager.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _domainContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ExpenseController> _logger;


        public ExpenseController(ApplicationDbContext domainContext, UserManager<IdentityUser> userManager, ILogger<ExpenseController> logger)
        {

            _domainContext = domainContext;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Expense
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var expenses = await _domainContext.Expenses
                .Where(i => i.UserId == userId)
                .ToListAsync();
            return View(expenses);
        }

        // GET: Expense/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var expense = await _domainContext.Expenses
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expense/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Vendor,Id,Name,Amount,Date,Description,IsRecurring")] Expense expense)
        {


            if (ModelState.IsValid)
            {
                expense.UserId = _userManager.GetUserId(User);
                _domainContext.Add(expense);
                await _domainContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Expense created successfully!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError($"ModelState Error: {error.ErrorMessage}");
                }
            }
            return View(expense);
        }

        // GET: Expense/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var expense = await _domainContext.Expenses
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Vendor,Id,Name,Amount,Date,Description,IsRecurring")] Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            var existing = await _domainContext.Expenses
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if(existing == null) {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    existing.Name = expense.Name;
                    existing.Amount = expense.Amount;
                    existing.Date = expense.Date;
                    existing.Vendor = expense.Vendor;
                    existing.Description = expense.Description;
                    existing.IsRecurring = expense.IsRecurring;
                    _domainContext.Update(existing);
                    await _domainContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        // GET: Expense/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var userId = _userManager.GetUserId(User);
            var expense = await _domainContext.Expenses
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var expense = await _domainContext.Expenses
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (expense != null)
            {
                _domainContext.Expenses.Remove(expense);
                await _domainContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _domainContext.Expenses.Any(e => e.Id == id);
        }
    }
}
