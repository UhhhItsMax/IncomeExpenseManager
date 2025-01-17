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

namespace IncomeExpenseManager.Controllers
{
    public class IncomeController : Controller
    {
        private readonly ApplicationDbContext _domainContext;
        private readonly UserManager<IdentityUser> _userManager;

        public IncomeController(ApplicationDbContext domainContext, UserManager<IdentityUser> userManager)
        {
            _domainContext = domainContext;
            _userManager = userManager;
        }

        // GET: Income
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var incomes = await _domainContext.Incomes
                .Where(i => i.UserId == userId)
                .ToListAsync();
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

            return View(income);
        }

        // GET: Income/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Income/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Source,Id,Name,Amount,Date,Description,IsRecurring")] Income income)
        {
            if (ModelState.IsValid)
            {
                income.UserId = _userManager.GetUserId(User);
                _domainContext.Add(income);
                await _domainContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(income);
        }

        // POST: Income/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Source,Id,Name,Amount,Date,Description,IsRecurring")] Income income)
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
