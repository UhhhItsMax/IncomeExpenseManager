using IncomeExpenseManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace IncomeExpenseManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Income>()
                .ToTable("Incomes")
                .HasBaseType((Type)null);

            modelBuilder.Entity<Expense>()
                .ToTable("Expenses")
                .HasBaseType((Type)null);

            modelBuilder.Entity<Category>()
                .ToTable("Categories")
                .HasBaseType((Type)null);

        }
    }
}
