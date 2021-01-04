using BankModelsLibrary.Enums;
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
        public User SendMoneyTooOther { get; set; }
        public User SendMoneyFromSelf { get; set; }
        public User RecieveMoneyFromOther { get; set; }
        public User RecieveMoneyToSelf { get; set; }
    }
}
