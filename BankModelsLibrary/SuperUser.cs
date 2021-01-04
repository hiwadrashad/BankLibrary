using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankModelsLibrary
{
    class SuperUser
    {
        [Key]
        public string id { get; set; }
        public string username { get; set; }
        public string Password { get; set; }
        public PersonalData data { get; set; }
        public List<User> users { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
