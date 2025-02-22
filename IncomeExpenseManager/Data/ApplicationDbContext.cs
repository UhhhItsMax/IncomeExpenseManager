using IncomeExpenseManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using IncomeExpenseManager.Models;


namespace IncomeExpenseManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TransactionBase> Transactions { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryVariable> CategoryVariables { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransactionBase>()
                .ToTable("Transactions")
                .HasDiscriminator<string>("TransactionType")
                .HasValue<Income>("Income")
                .HasValue<Expense>("Expense");

            modelBuilder.Entity<TransactionBase>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CategoryVariable>()
                .HasOne(cv => cv.Category)
                .WithMany(c => c.Variables)
                .HasForeignKey(cv => cv.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BankAccount>()
                .HasIndex(b => b.UserId);
        }
    }
}
