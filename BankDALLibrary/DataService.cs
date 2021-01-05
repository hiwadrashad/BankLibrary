using System;
using System.Collections.Generic;
using BankModelsLibrary;
using BankStructuresLibrary;
using BankDataStorageLibrary;
using System.Linq;

namespace BankDALLibrary
{
    public class DataService :BankStructuresLibrary.Interfaces.IDataService
    {
        private ApplicationDbContext _dbcontext = ApplicationDbContext.GetDbContext();

        private static DataService _dataService;

        private DataService()
        { 
        
        }

        public static DataService GetDataService()
        {
            if (_dataService == null)
            {
                _dataService = new DataService();
                //_dataService.InitData();
            }
            return _dataService;
        }

        //private void InitData()
        //{
        //    _users = new List<User>()
        //    {
        //        new User()
        //        { }
        //    };
        //    _superUsers = new List<SuperUser>()
        //    {
        //        new SuperUser()
        //        { }
        //    };
        //    _banks = new List<Bank>()
        //    {
        //        new Bank()
        //        { }
        //    };
        //    _transactions = new List<Transaction>()
        //    {
        //        new Transaction()
        //        { }
        //    };
        //}

        public bool AddUser(User user)
        {
            _dbcontext.UserModels.Add(user);
            _dbcontext.SaveChanges();
            return true;
        }
        public bool AddsuperUser(SuperUser SuperUser)
        {
            _dbcontext.SuperUserModels.Add(SuperUser);
            _dbcontext.SaveChanges();
            return true;
        }
        public bool AddBank(Bank bank)
        {
            _dbcontext.BankModels.Add(bank);
            _dbcontext.SaveChanges();
            return true;
        }
        public bool AddTransaction(Transaction transaction)
        {
            _dbcontext.TransactionModels.Add(transaction);
            _dbcontext.SaveChanges();
            return true;
        }

        public bool RemoveUser(User user)
        {
            _dbcontext.UserModels.Remove(user);
            _dbcontext.SaveChanges();
            return true;
        }

        public bool RemoveSuperUser(SuperUser Superuser)
        {
            _dbcontext.SuperUserModels.Remove(Superuser);
            _dbcontext.SaveChanges();
            return true;
        }

        public bool RemoveBank(Bank bank)
        {
            _dbcontext.BankModels.Remove(bank);
            _dbcontext.SaveChanges();
            return true;
        }

        public bool RemoveTransaction(Transaction transaction)
        {
            _dbcontext.TransactionModels.Remove(transaction);
            _dbcontext.SaveChanges();
            return true;
        }

        public IEnumerable<User> ReturnAllUsers()
        {
            return _dbcontext.UserModels;
        }
        public IEnumerable<SuperUser> ReturnAllSuperUsers()
        {
            return _dbcontext.SuperUserModels;
        }

        public IEnumerable<Bank> ReturnAllBanks()
        {
            return _dbcontext.BankModels;
        }

        public IEnumerable<Transaction> ReturnAllTransactions()
        {
            return _dbcontext.TransactionModels;
        }

        public bool UpdateUser(User user)
        {
            var item = _dbcontext.UserModels.Find(user.id);
            item = user;
            _dbcontext.SaveChanges();
            return true;
        }

        public bool UpdateSuperUser(SuperUser SuperUser)
        {
            var item = _dbcontext.SuperUserModels.Where(a => a.id == SuperUser.id).FirstOrDefault();
            item = SuperUser;
            _dbcontext.SaveChanges();
            return true;
        }

        public bool UpdateBank(Bank bank)
        {
            var item = _dbcontext.BankModels.Where(a => a.id == bank.id).FirstOrDefault();
            item = bank;
            _dbcontext.SaveChanges();
            return true;
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            var item = _dbcontext.TransactionModels.Where(a => a.id == transaction.id).FirstOrDefault();
            item = transaction;
            _dbcontext.SaveChanges();
            return true;
        }
    }
}
