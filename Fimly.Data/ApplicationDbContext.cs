﻿using Fimly.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fimly.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency { Id = 1, Symbol = "£", Name = "GBP" },
                new Currency { Id = 2, Symbol = "€", Name = "EUR" },
                new Currency { Id = 3, Symbol = "$", Name = "USD" },
                new Currency { Id = 4, Symbol = "¥", Name = "JPY" },
                new Currency { Id = 5, Symbol = "₩", Name = "KRW" },
                new Currency { Id = 6, Symbol = "﷼", Name = "SAR" },
                new Currency { Id = 7, Symbol = "₽", Name = "RUB" },
                new Currency { Id = 8, Symbol = "R", Name = "ZAR" }
            );

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }
        public virtual DbSet<Person> People { get; set; }
    }
}
