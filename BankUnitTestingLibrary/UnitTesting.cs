using BankDALLibrary;
using BankEventsAndDelegates;
using BankModelsLibrary;
using BankStructuresLibrary.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BankUnitTestingLibrary
{
    [TestClass]
    public class UnitTesting
    {
        [TestMethod]
        [TestCategory("DALTesting")]
        public void checkIfDALAddingWorks()
        { 
        User testuser = new User()
        { 
         id = Guid.NewGuid().ToString(),
         AccountCredit = 1000000,
         AdditionalInformation = "",
         DateOfGeneration = DateTime.Now,
         Password = "test",
         PIN= 0000,
         username = "test"
        };
            object[] value = new object[0];
            IDataService _dataService = DataService.GetDataService();
            Action action = () => _dataService.AddUser(testuser);
            NUnit.Framework.Assert.DoesNotThrow(() => action(), "Code'sfunctionality not impended", value);
        }


    }
}
