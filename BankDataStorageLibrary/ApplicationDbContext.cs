using Microsoft.EntityFrameworkCore;
using BankModelsLibrary;
using System;

namespace BankDataStorageLibrary
{
    public class ApplicationDbContext : DbContext
    {
        private static ApplicationDbContext _dbContext;

        public ApplicationDbContext()
        { }

        public static ApplicationDbContext GetDbContext()
        {
            if (_dbContext != null)
            {
                _dbContext = new ApplicationDbContext();
            }
            return _dbContext;
        }

        public DbSet<Bank> BankModels { get; set; }
        public DbSet<SuperUser> SuperUserModels { get; set; }
        public DbSet<Transaction> TransactionModels { get; set; }
        public DbSet<User> UserModels { get; set; }

    }
}
