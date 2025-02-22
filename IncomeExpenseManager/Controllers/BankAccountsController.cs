using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncomeExpenseManager.Data;
using IncomeExpenseManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace IncomeExpenseManager.Controllers
{
    [Authorize]
    public class BankAccountsController : Controller
    {
        private readonly ApplicationDbContext _domainContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<BankAccountsController> _logger;

        public BankAccountsController(ApplicationDbContext domainContext, UserManager<ApplicationUser> userManager, ILogger<BankAccountsController> logger)
        {
            _domainContext = domainContext;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: BankAccounts
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var bankAccounts = await _domainContext.BankAccounts
                .Where(i => i.UserId == userId)
                .ToListAsync();
            return View(bankAccounts);
        }

        // GET: BankAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var bankAccount = await _domainContext.BankAccounts
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CurrentBalance")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                bankAccount.UserId = _userManager.GetUserId(User);
                _domainContext.Add(bankAccount);
                await _domainContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Bank account created successfully!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError($"ModelState Error: {error.ErrorMessage}");
                }
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var bankAccount = await _domainContext.BankAccounts
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CurrentBalance")] BankAccount bankAccount)
        {
            if (id != bankAccount.Id)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var existingAccount = await _domainContext.BankAccounts
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (existingAccount == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    existingAccount.Name = bankAccount.Name;
                    existingAccount.CurrentBalance = bankAccount.CurrentBalance;

                    _domainContext.Update(existingAccount);
                    await _domainContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccountExists(bankAccount.Id))
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

            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var bankAccount = await _domainContext.BankAccounts
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var bankAccount = await _domainContext.BankAccounts
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (bankAccount != null)
            {
                _domainContext.BankAccounts.Remove(bankAccount);
                await _domainContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BankAccountExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _domainContext.BankAccounts.Any(e => e.Id == id && e.UserId == userId);
        }
    }
}
