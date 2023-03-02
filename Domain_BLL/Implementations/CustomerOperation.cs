using Data_PLL;
using Data_PLL.Entities;
using Domain_BLL.Interfaces;
using Domain_BLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Domain_BLL.Implementations
{
    public class CustomerOperation : ICustomerOperations
    {
        TransactionHistory history = new TransactionHistory();

        private readonly AtmDbContextFactory _atmDb;

        public CustomerOperation()
        {
            _atmDb = new AtmDbContextFactory();
        }

      
        public async Task  Customeroperation() {
            var customerViewModels = CustomerList.GetCustomers();


            using (var context = _atmDb.CreateDbContext(null))
            {
                foreach (var customer in customerViewModels)
                {
                    var existingCustomer = context.Customers.FirstOrDefault(x => x.AccountNumber == customer.AccountNumber);


                    if (existingCustomer != null)
                    {
                        continue;
                    }

                    var newCustomer = new Customers
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        AccountName = customer.AccountName,
                        AccountNumber = customer.AccountNumber,
                        Pin = customer.Pin,
                        Balance = customer.Balance,
                        DateCreated = customer.DateCreated
                    };
                    await context.Customers.AddRangeAsync(newCustomer);

                    
                }
                
              
                await context.SaveChangesAsync();
            }
       
        }

        public async Task<CustomerViewModel> DepositAsync(Customers user, decimal amount)
        {
            using var context = _atmDb.CreateDbContext(null);

            var customer = await context.Customers.FirstOrDefaultAsync(x => x.AccountNumber == user.AccountNumber && x.Pin == user.Pin);

            if (customer == null)
            {
                Console.WriteLine("Invalid account number or PIN.");
                return null;
            }
            if (amount == 0)
            {
                Console.WriteLine("Invalid amount.");
                return null;
            }


            customer.Balance += amount;

            var customerViewModel = new CustomerViewModel
            {

                AccountNumber = customer.AccountNumber,
                Balance = customer.Balance
            };

            history.CustomersId = customer.Id;
            history.TransactionType = "Deposit";
            history.TransactionDate = DateTime.UtcNow;
            history.Balance = customer.Balance;

            customer.TransactionHistories.Append(history);
            await context.SaveChangesAsync();

            Console.WriteLine($"You have successfully made a deposit of {amount}. New balance is {customer.Balance:C}.");
            return customerViewModel;
        }

        public async Task<Customers> Login(string accountNumber, string pin)
        {
            Customers LoggedCustomer;
            using (var context = _atmDb.CreateDbContext(null))
            {
               
                var customers = await context.Customers.Where(c => c.AccountNumber.Contains(accountNumber) && c.Pin.Contains(pin)).FirstOrDefaultAsync();

                
                LoggedCustomer = customers;
            }
            return LoggedCustomer;
        }

  

        public async Task<CustomerViewModel> WithdrawAsync(Customers user, decimal amount)
        {
            using (var context = _atmDb.CreateDbContext(null))
            {
                var customer = await context.Customers.FirstOrDefaultAsync(x => x.AccountNumber == user.AccountNumber && x.Pin == user.Pin);

                if (customer == null)
                {
                    Console.WriteLine("Invalid account number or PIN.");
                    return null;
                }

                if (amount <= 0)
                {
                    Console.WriteLine("Invalid amount to withdraw.");
                    return null;
                }

                if (amount > customer.Balance)
                {
                    Console.WriteLine("Insufficient funds.");
                    return null;
                }
                if(amount != (int)amount)
                {
                    Console.WriteLine("You can't withdraw decimals");
                    return null;
                }
                bool ab = true;
                while (ab == true)
                {
                    Console.Write("Processing");
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write(".");
                        await Task.Delay(600);
                        Console.Write(".");
                    }
                    ab = false;
                }
                customer.Balance -= amount;

                var customerViewModel = new CustomerViewModel
                {
                    
                    AccountNumber = customer.AccountNumber,
                    Balance = customer.Balance
                };
                history.CustomersId = customer.Id;
                history.TransactionType = "Withdraw";
                history.TransactionDate = DateTime.UtcNow;
                history.Balance = customer.Balance;

                customer.TransactionHistories.Append(history);
                await context.SaveChangesAsync();

                Console.WriteLine($"Withdrawal of {amount} successful. New balance is {customer.Balance:C}.");
                return customerViewModel;
            }
        }


        public async Task CheckBalanceAsync(Customers user)
        {
            using (var context = _atmDb.CreateDbContext(null))
            {
                var customer = await context.Customers.FirstOrDefaultAsync(x => x.AccountNumber == user.AccountNumber && x.Pin == user.Pin);
                try
                {

                    if (customer == null)
                    {
                        Console.WriteLine("Invalid account number or PIN.");
                        return;
                    }

                    Console.WriteLine($"Account balance for {customer.AccountName}: {customer.Balance:C}");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Error: Multiple customers with the same account number and PIN.");
                   
                }
            }
        }
        public async Task<bool> TransferAsync(Customers user, string recipientAccountNumber, decimal amount)
        {

            using (var context = _atmDb.CreateDbContext(null))
            {
                if (user == null)
                {
                    Console.WriteLine("Invalid account number or PIN.");
                    return false;
                }

                

                var recipient = await context.Customers.FirstOrDefaultAsync(x => x.AccountNumber == recipientAccountNumber);

                if (recipient == null)
                {
                    Console.WriteLine("Recipient account not found.");
                    return false;
                }

                if (amount <= 0)
                {
                    Console.WriteLine("Invalid transfer amount.");
                    return false;
                }

                if (amount > user.Balance)
                {
                    Console.WriteLine("Insufficient funds.");
                    return false;
                }

                if (user.AccountNumber == recipientAccountNumber)
                {
                    Console.WriteLine("You cannot transfer funds to your own account.");
                    return false;
                }
                var customer = await context.Customers.FirstOrDefaultAsync(x => x.AccountNumber == user.AccountNumber && x.Pin == user.Pin);
                customer.Balance -= amount;
                recipient.Balance += amount;

                await context.SaveChangesAsync();
                history.CustomersId = customer.Id;
                history.TransactionType = "Transfer";
                history.TransactionDate = DateTime.UtcNow;
                history.Balance = customer.Balance;
                history.CustomersNavigation = recipient;

                customer.TransactionHistories.Append(history);
                await context.SaveChangesAsync();

                Console.WriteLine($"Transfer successful. {customer.AccountName} transferred {amount:C} to {recipient.AccountName}.");

                return true;
            }
        }

        public async Task<IEnumerable<TransactionHistory>> TransactionHistoryAsync(Customers user)
        {
            using(var context = _atmDb.CreateDbContext(null))
            {
                var customer = await context.Customers.FirstOrDefaultAsync(x => x.AccountNumber == user.AccountNumber && x.Pin == user.Pin);

                return customer.TransactionHistories;
            }
        }

    }
}

