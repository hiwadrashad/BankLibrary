using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankModelsLibrary
{
    public class Bank
    {
        [Key]
        public string id { get; set; }
        public string username { get; set; }
        public string Password { get; set; }
        public PersonalData data { get; set; }
        public DateTime DateOfGeneration { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
