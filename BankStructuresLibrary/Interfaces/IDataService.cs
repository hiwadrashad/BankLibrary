using BankModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankStructuresLibrary.Interfaces
{
    public interface IDataService
    {
        bool AddUser(User user);
        bool AddsuperUser(SuperUser SuperUser);
        bool AddBank(Bank bank);
        bool AddTransaction(Transaction transaction);
        bool RemoveUser(User user);
        bool RemoveSuperUser(SuperUser Superuser);
        bool RemoveBank(Bank bank);
        bool RemoveTransaction(Transaction transaction);
        IEnumerable<User> ReturnAllUsers();
        IEnumerable<SuperUser> ReturnAllSuperUsers();
        IEnumerable<Bank> ReturnAllBanks();
        IEnumerable<Transaction> ReturnAllTransactions();
        bool UpdateUser(User user);
        bool UpdateSuperUser(SuperUser SuperUser);
        bool UpdateBank(Bank bank);
        bool UpdateTransaction(Transaction transaction);
    }
}
