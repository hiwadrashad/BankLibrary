using BankGeneralFunctionsLibrary;
using BankModelsLibrary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankEventsAndDelegates
{
    public class EventsAndDelegates
    {
        public EventsAndDelegates item = new EventsAndDelegates();
        GeneralMethods methods = new GeneralMethods();

        public delegate void AutomatedPaymentDelegate(double price, int amountoftimespayment);
        public event AutomatedPaymentDelegate AutomatedpaymentEvent;

        public void AutomatedPaymentTroughDelegates(double price, int amountoftimespayment)
        {
            var item2 = new AutomatedPaymentDelegate(methods.AutomatedPayment);
            item.AutomatedpaymentEvent += item2;
            item.AutomatedpaymentEvent(price, amountoftimespayment);
            item.AutomatedpaymentEvent -= item2;
        }

        public delegate void SendMoneyDelegate(User user, User sendto, double price);
        public event SendMoneyDelegate SendMoneyEvent;

        public void SendMoneyTroughDelegates(User user, User sendto, double price)
        {
            var item2 = new SendMoneyDelegate(methods.SendMoney);
            item.SendMoneyEvent += item2;
            item.SendMoneyEvent(user,sendto, price);
            item.SendMoneyEvent -= item2;
        }

        public delegate void AskMoneyDelegate(User user, User sendto, double price);
        public event AskMoneyDelegate askMoneyEvent;

        public void askMoneyTroughDelegates(User user, User sendto, double price)
        {
            var item2 = new AskMoneyDelegate(methods.AskMoney);
            item.askMoneyEvent += item2;
            item.askMoneyEvent(user, sendto, price);
            item.askMoneyEvent -= item2;
        }

#nullable enable
        public delegate Task<List<BankModelsLibrary.User>>? ReturnAllusersSortedOnNameDelegate();
        public event ReturnAllusersSortedOnNameDelegate? ReturnAllusersSortedOnNameEvent;

        public void ReturnAllusersSortedOnNameTroughDelegate()
        {
            var item2 = new ReturnAllusersSortedOnNameDelegate(methods.ReturnAllUsersSortedOnName);
            item.ReturnAllusersSortedOnNameEvent += item2;
            item.ReturnAllusersSortedOnNameEvent();
            item.ReturnAllusersSortedOnNameEvent -= item2;
        }
#nullable disable
    }
}
