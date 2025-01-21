using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IncomeExpenseManager.Data;
using IncomeExpenseManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace IncomeExpenseManager.Controllers
{
    [Authorize]

    public class IncomeController : Controller
    {
        private readonly ApplicationDbContext _domainContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ExpenseController> _logger;

        public IncomeController(ApplicationDbContext domainContext, UserManager<IdentityUser> userManager, ILogger<ExpenseController> logger)
        {
            _domainContext = domainContext;
            _userManager = userManager;
            _logger = logger;
        }

        private async Task PopulateCategoriesAsync()
        {
            var userId = _userManager.GetUserId(User);
            var categories = await _domainContext.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }

        // GET: Income
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var incomes = await _domainContext.Incomes
                .Where(i => i.UserId == userId)
                .ToListAsync();
            await PopulateCategoriesAsync();
            return View(incomes);
        }

        // GET: Income/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var income = await _domainContext.Incomes
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (income == null)
            {
                return NotFound();
            }
            await PopulateCategoriesAsync();
            return View(income);
        }

        // GET: Income/Create
        public async Task<IActionResult> Create()
        {
            await PopulateCategoriesAsync();
            return View();
        }

        // POST: Income/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Source,Id,Name,Amount,Date,Description,IsRecurring,CategoryId")] Income income)
        {
            if (ModelState.IsValid)
            {
                income.UserId = _userManager.GetUserId(User);
                _domainContext.Add(income);
                await _domainContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Income created successfully!";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError($"ModelState Error: {error.ErrorMessage}");
                }
            }
            await PopulateCategoriesAsync();
            return View(income);
        }

        // GET: Income/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var income = await _domainContext.Incomes
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (income == null)
            {
                return NotFound();
            }
            await PopulateCategoriesAsync();
            return View(income);
        }

        // POST: Income/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Source,Id,Name,Amount,Date,Description,IsRecurring,CategoryId")] Income income)
        {
            if (id != income.Id)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            var existing = await _domainContext.Incomes
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (existing == null) {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    existing.Name = income.Name;
                    existing.Amount = income.Amount;
                    existing.Date = income.Date;
                    existing.Description = income.Description;
                    existing.IsRecurring = income.IsRecurring;
                    existing.Source = income.Source;
                    _domainContext.Update(existing);
                    await _domainContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeExists(income.Id))
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
            await PopulateCategoriesAsync();
            return View(income);
        }

        // GET: Income/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var income = await _domainContext.Incomes
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (income == null)
            {
                return NotFound();
            }
            await PopulateCategoriesAsync();
            return View(income);
        }

        // POST: Income/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var income = await _domainContext.Incomes
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (income != null)
            {
                _domainContext.Incomes.Remove(income);
                await _domainContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool IncomeExists(int id)
        {
            return _domainContext.Incomes.Any(e => e.Id == id);
        }
    }
}
