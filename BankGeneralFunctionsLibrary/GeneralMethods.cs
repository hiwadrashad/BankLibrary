using BankDALLibrary;
using BankModelsLibrary;
using BankStructuresLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Linq;

namespace BankGeneralFunctionsLibrary
{
    public static class Cloning
    {
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }

    public class GeneralMethods
    {
        IDataService _dataService = DataService.GetDataService();
        public static bool EnoughSaldo(User usercheck, double pricecheck)
        {
            if (usercheck.AccountCredit >= pricecheck)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<BankModelsLibrary.Transaction>> ReturnTransactionsWithinSecondsTimeFrame(User user, int timeinseconds)
        {
           return await Task.Run(() =>
            {
                var date = DateTime.Now.AddSeconds(-timeinseconds);
                var item = user.Transactions.Where(a => a.DateOfTransaction > date).ToList();
                return item;
            }
            );            
        }

        public async Task<List<BankModelsLibrary.Transaction>> ReturnTransactionsWithinSecondsTimeFramePositiveTransactions(User user, int timeinseconds)
        {
            return await Task.Run(() =>
            {
                var date = DateTime.Now.AddSeconds(-timeinseconds);
                var item = user.Transactions.Where(a => a.DateOfTransaction > date).ToList();
                //item2 = item.
                return item;
            }
             );
        }

        public async Task<List<BankModelsLibrary.Transaction>> ReturnTransactionsWithinSecondsTimeFrameNegativeTransactions(User user, int timeinseconds)
        {
            return await Task.Run(() =>
            {
                var date = DateTime.Now.AddSeconds(-timeinseconds);
                var item = user.Transactions.Where(a => a.DateOfTransaction > date).ToList();
                return item;
            }
             );
        }


        public async void AutomatedPayment(double price, int amountoftimespayment)
        {
            var lockingobject = new object();

            Monitor.Enter(lockingobject);
            using (var transactionrollback = new TransactionScope())
            {
                User DummyFrom = new User()
                {
                     id = Guid.NewGuid().ToString(),
                     AccountCredit = 1000000,
                     AdditionalInformation = "",
                     Contacts = new System.Collections.Generic.List<User>()
                     { 
                     new User()
                     { }
                     },
                     data = new PersonalData()
                     { },
                     DateOfGeneration = DateTime.Now,
                     Password = "test",
                     PIN = 1234,
                     Transactions = new System.Collections.Generic.List<BankModelsLibrary.Transaction>()
                     { },
                     username = "test"

                     
                };
                User DummyTo = new User()
                {
                    id = Guid.NewGuid().ToString(),
                    AccountCredit = 1000000,
                    AdditionalInformation = "",
                    Contacts = new System.Collections.Generic.List<User>()
                     {
                     new User()
                     { }
                     },
                    data = new PersonalData()
                    { },
                    DateOfGeneration = DateTime.Now,
                    Password = "test",
                    PIN = 1234,
                    Transactions = new System.Collections.Generic.List<BankModelsLibrary.Transaction>()
                    { },
                    username = "test"


                };
                try
                {
                    for (int a = 0; a < amountoftimespayment; a++)
                    {
                        await Task.Run(() =>
                        {
                            if (EnoughSaldo(DummyFrom, price))
                            {
                                var transaction = new BankModelsLibrary.Transaction()
                                {
                                    id = Guid.NewGuid().ToString(),
                                    addedorsubtracted = BankModelsLibrary.Enums.AddedOrSubstractedEnum.Added,
                                    AmountMutation = price,
                                    DateOfTransaction = DateTime.Now,
                                    SendMoneyTo = DummyTo,
                                    SendMoneyFrom = DummyFrom
                                };
                                Thread.Sleep(30000);
                                _dataService.AddTransaction(transaction);
                                DummyFrom.Transactions.Add(transaction);
                                DummyFrom.Contacts.Add(DummyTo);
                                DummyFrom.AccountCredit = DummyFrom.AccountCredit - price;
                                _dataService.UpdateUser(DummyFrom);
                                DummyTo.Transactions.Add(transaction);
                                DummyTo.Contacts.Add(DummyFrom);
                                DummyTo.AccountCredit = DummyTo.AccountCredit + price;
                                _dataService.UpdateUser(DummyTo);


                                MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("The transaction went succesfull", "Succesfull", MessageBox.MsgBoxStyle.OkOnly);
                            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only alert Message');", true);
                        }
                            else
                            {

                                MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("You don't have enough saldo to make the transaction", "Failed", MessageBox.MsgBoxStyle.OkOnly);
                            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only alert Message');", true);

                        }
                            transactionrollback.Complete();
                        }
                        );
                    }

                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("Please enter a valid value", "Failed", MessageBox.MsgBoxStyle.OkOnly);
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("Please enter a valid value", "Failed", MessageBox.MsgBoxStyle.OkOnly);
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("Something went wrong", "Failed", MessageBox.MsgBoxStyle.OkOnly);
                }
                finally
                {
                    Monitor.Exit(lockingobject);
                }
            }
        }



        public async void SendMoney(User SendFrom, User Sendto, double price)
        {
            var lockingobject = new object();

            Monitor.Enter(lockingobject);
            using (var transactionrollback = new TransactionScope())
            {
                try
                {
                    await Task.Run(() =>
                    {
                        if (EnoughSaldo(SendFrom, price))
                        {
                            var transaction = new BankModelsLibrary.Transaction()
                            {
                                id = Guid.NewGuid().ToString(),
                                addedorsubtracted = BankModelsLibrary.Enums.AddedOrSubstractedEnum.Added,
                                AmountMutation = price,
                                DateOfTransaction = DateTime.Now,
                                SendMoneyTo = Sendto,
                                SendMoneyFrom = SendFrom
                            };
                            Thread.Sleep(5000);
                            _dataService.AddTransaction(transaction);
                            SendFrom.AccountCredit = SendFrom.AccountCredit - price;
                            SendFrom.Contacts.Add(Sendto);
                            SendFrom.Transactions.Add(transaction);
                            _dataService.UpdateUser(SendFrom);
                            Sendto.Contacts.Add(SendFrom);
                            Sendto.Transactions.Add(transaction);
                            Sendto.AccountCredit = Sendto.AccountCredit + price;
                            _dataService.UpdateUser(Sendto);


                            MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("The transaction went succesfull", "Succesfull", MessageBox.MsgBoxStyle.OkOnly);
                            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only alert Message');", true);
                        }
                        else
                        {

                            MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("You don't have enough saldo to make the transaction", "Failed", MessageBox.MsgBoxStyle.OkOnly);
                            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only alert Message');", true);

                        }
                        transactionrollback.Complete();
                    }
                    );

                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("Please enter a valid value", "Failed", MessageBox.MsgBoxStyle.OkOnly);
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("Please enter a valid value", "Failed", MessageBox.MsgBoxStyle.OkOnly);
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("Something went wrong", "Failed", MessageBox.MsgBoxStyle.OkOnly);
                }
                finally
                {
                    Monitor.Exit(lockingobject);
                }
            }
        }
        public async void AskMoney(User SendFrom, User Sendto, double price)
        {
            var lockingobject = new object();

            Monitor.Enter(lockingobject);
            using (var transactionrollback = new TransactionScope())
            {
                try
                {
                    await Task.Run(() =>
                    {
                        if (EnoughSaldo(SendFrom, price))
                        {
                            var transaction = new BankModelsLibrary.Transaction()
                            {
                                id = Guid.NewGuid().ToString(),
                                addedorsubtracted = BankModelsLibrary.Enums.AddedOrSubstractedEnum.Substracted,
                                AmountMutation = price,
                                DateOfTransaction = DateTime.Now,
                                SendMoneyTo = Sendto,
                                SendMoneyFrom = SendFrom
                            };
                            Thread.Sleep(5000);
                            _dataService.AddTransaction(transaction);
                            SendFrom.AccountCredit = SendFrom.AccountCredit - price;
                            SendFrom.Contacts.Add(Sendto);
                            SendFrom.Transactions.Add(transaction);
                            _dataService.UpdateUser(SendFrom);
                            Sendto.Contacts.Add(SendFrom);
                            Sendto.Transactions.Add(transaction);
                            Sendto.AccountCredit = Sendto.AccountCredit + price;
                            _dataService.UpdateUser(Sendto);



                            MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("The transaction went succesfull", "Succesfull", MessageBox.MsgBoxStyle.OkOnly);
                            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only alert Message');", true);
                        }
                        else
                        {

                            MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("You don't have enough saldo to make the transaction", "Failed", MessageBox.MsgBoxStyle.OkOnly);
                            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only alert Message');", true);

                        }
                        transactionrollback.Complete();
                    }
                    );

                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("Please enter a valid value", "Failed", MessageBox.MsgBoxStyle.OkOnly);
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("Please enter a valid value", "Failed", MessageBox.MsgBoxStyle.OkOnly);
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    MessageBox.MsgBoxResult result = MessageBox.Interaction.MsgBox("Something went wrong", "Failed", MessageBox.MsgBoxStyle.OkOnly);
                }
                finally
                {
                    Monitor.Exit(lockingobject);
                }
            }
        }

    }
}
