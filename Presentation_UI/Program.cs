using Data_PLL;
using Data_PLL.Entities;
using Domain_BLL;
using Domain_BLL.Implementations;
using Presentation_UI.Utility;
using System.Net.NetworkInformation;

public class Program
{
    public static async Task Main(string[] args)
    {
        CustomerOperation customerOperation = new CustomerOperation();
        await customerOperation.Customeroperation();

        string accountNumber;
        string pin;
        Customers loggedUser;
        string userName;
        while (true)
        {
            LineAndColorModes.Welcome();
            Console.Write("Enter your Account Number: ");
            accountNumber = Console.ReadLine();
            Console.Write("Enter your Login Pin: ");
            pin = Console.ReadLine();

            if (string.IsNullOrEmpty(accountNumber) || string.IsNullOrEmpty(pin))
            {
                Console.WriteLine("Invalid account number or PIN. Please try again. Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                continue;
            }

            loggedUser = await customerOperation.Login(accountNumber, pin);
            if (loggedUser == null)
            {
                Console.WriteLine("Invalid account number or PIN. Please try again. Press any key to continue");
                Console.ReadKey();
                continue;
            }
            Console.Clear();
            userName = $"Welcome {loggedUser.AccountName}";
            break;
        }

        while (true)
        {
            LineAndColorModes.Welcome();
            Console.WriteLine(userName);
            Console.WriteLine("Enter an option:");
            Console.WriteLine("1. Check balance");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Exit");

            int option;
            while (!int.TryParse(Console.ReadLine(), out option))
            {
                LineAndColorModes.Red("Invalid option. Please enter a number.");
            }

            switch (option)
            {
                case 1:
                    LineAndColorModes.AnimationSlide();
                    LineAndColorModes.Services("Check Balance");
                    await customerOperation.CheckBalanceAsync(loggedUser.AccountNumber, loggedUser.Pin);
                    LineAndColorModes.Yellow("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 2:
                    LineAndColorModes.AnimationSlide();
                    a:
                    LineAndColorModes.Services("Withdraw");
                    Console.Write("Enter the amount to withdraw: ");
                    decimal withdrawAmount;
                    if (!decimal.TryParse(Console.ReadLine(), out withdrawAmount))
                    {
                        LineAndColorModes.Red("Invalid amount. Please enter a number.");
                        goto a;
                    }
                    await customerOperation.WithdrawAsync(loggedUser.AccountNumber, loggedUser.Pin, withdrawAmount);
                    LineAndColorModes.Yellow("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 3:
                    LineAndColorModes.AnimationSlide();
                    b:
                    LineAndColorModes.Services("Deposit");
                    Console.WriteLine("Enter the amount to deposit: ");
                    decimal depositAmount;
                    if (!Decimal.TryParse(Console.ReadLine(), out depositAmount))
                    {
                        LineAndColorModes.Red("Invalid amount. Please enter a number.");
                        goto b;
                    }
                    await customerOperation.DepositAsync(loggedUser.AccountNumber, loggedUser.Pin, depositAmount);
                    LineAndColorModes.Yellow("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 4:
                    LineAndColorModes.AnimationSlide();
                    c:
                    LineAndColorModes.Services("Transfer");
                    Console.Write("Enter the amount to transfer: ");
                    decimal transferAmount;
                    if (!Decimal.TryParse(Console.ReadLine(), out transferAmount))
                    {
                        LineAndColorModes.Red("Invalid amount. Please enter a number.");
                        goto c;
                    }
                    Console.Write("Enter recipient's account number: ");
                    string recipientAccountNumber = Console.ReadLine();
                    await customerOperation.TransferAsync(loggedUser.AccountNumber, loggedUser.Pin, recipientAccountNumber, transferAmount);
                    LineAndColorModes.Yellow("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 5:
                    Console.WriteLine("Exiting...");
                    LineAndColorModes.AnimationSlide();
                    Environment.Exit(0);
                    return;
                default:
                    LineAndColorModes.Red("Invalid option. Please enter a number between 1 and 4.");
                    break;
            }
        }
    }
}
