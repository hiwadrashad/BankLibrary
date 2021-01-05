using BankModelsLibrary;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

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


        public async void SendMoney(User SendFrom, User Sendto, double price)
        {
            var lockingobject = new object();

            Monitor.Enter(lockingobject);
            using (var transaction = new TransactionScope())
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
                                addedorsubtracted = BankStructuresLibrary.Enums.AddedOrSubstractedEnum.Added,
                                AmountMutation = price,
                                DateOfTransaction = DateTime.Now,
                                SendMoneyTo = Sendto,
                                SendMoneyFrom = SendFrom
                            };
                         SendFrom.AccountCredit = SendFrom.AccountCredit - price;
                         BankDALLibrary.DataService.GetDataService().UpdateUser(SendFrom);
                        
                        }
                    }
                    );
                    
                    transaction.Complete();
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {

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
            using (var transaction = new TransactionScope())
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
                                addedorsubtracted = BankStructuresLibrary.Enums.AddedOrSubstractedEnum.Substracted,
                                AmountMutation = price,
                                DateOfTransaction = DateTime.Now,
                                SendMoneyTo = Sendto,
                                SendMoneyFrom = SendFrom
                            };
                            SendFrom.AccountCredit = SendFrom.AccountCredit - price;
                            BankDALLibrary.DataService.GetDataService().UpdateUser(SendFrom);

                        }
                    }
                    );

                    transaction.Complete();
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {

                }
                finally
                {
                    Monitor.Exit(lockingobject);
                }
            }
        }

    }
}
