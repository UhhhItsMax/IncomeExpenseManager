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
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _domainContext;
        private readonly UserManager<IdentityUser> _userManager;


        public CategoriesController(ApplicationDbContext domainContext, UserManager<IdentityUser> userManager)
        {
            _domainContext = domainContext;
            _userManager = userManager;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var categories = await _domainContext.Categories
                .Where(i => i.UserId == userId)
                .ToListAsync();
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var category = await _domainContext.Categories
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UserId")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.UserId = _userManager.GetUserId(User);
                _domainContext.Add(category);
                await _domainContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Category created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var category = await _domainContext.Categories
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            var existing = await _domainContext.Categories
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (ModelState.IsValid)
            {
                try
                {
                    existing.Name = category.Name;
                    _domainContext.Update(category);
                    await _domainContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var category = await _domainContext.Categories
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var category = await _domainContext.Categories.FindAsync(id);
            if (category != null)
            {
                _domainContext.Categories.Remove(category);
            }

            await _domainContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _domainContext.Categories.Any(i => i.Id == id);
        }
    }
}
