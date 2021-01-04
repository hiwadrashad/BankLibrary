using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankModelsLibrary
{
    public class User
    {
        [Key]
        public string id { get; set; }
        public string username { get; set; }
        public string Password { get; set; }
        public int PIN { get; set; }
        public double AccountCredit { get; set; }
        public PersonalData data { get; set; }
        public DateTime DateOfGeneration { get; set; }
        public string AdditionalInformation { get; set; }
        public List<User> Contacts { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
