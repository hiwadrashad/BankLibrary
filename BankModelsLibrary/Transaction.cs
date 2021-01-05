using BankStructuresLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankModelsLibrary
{
    public class Transaction
    {
       [Key]
        public string id { get; set; }
        public double AmountMutation { get; set; }
        public AddedOrSubstractedEnum addedorsubtracted { get; set; }
        public DateTime DateOfTransaction { get; set; }
#nullable enable
        public User? SendMoneyFrom { get; set; }
        public User? SendMoneyTo { get; set; }
#nullable disable
    }
}
