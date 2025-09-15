using System;

namespace BankingSystem
{
    // Main program class
    class Program
    {
        static void Main(string[] args)
        {
            // Use interface instead of concrete class
            IBank bank = new Bank();
            IBankService bankService = new BankService(bank);

            Console.WriteLine("Welcome to Simple Bank!");
            Console.WriteLine("Let's create some accounts first.");

            // Get user input to create accounts
            CreateAccountsFromUser(bankService);

            bool exit = false;
            while (!exit)
            {
                PrintMenu();
                Console.Write("Enter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HandleDeposit(bankService);
                        break;
                    case "2":
                        HandleWithdrawal(bankService);
                        break;
                    case "3":
                        HandleCheckBalance(bankService);
                        break;
                    case "4":
                        exit = true;
                        Console.WriteLine("Thank you for using our bank. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
            }
        }

        // Get user input to create accounts
        private static void CreateAccountsFromUser(IBankService bankService)
        {
            Console.Write("How many accounts do you want to create? ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int accountCount) && accountCount > 0)
            {
                for (int i = 1; i <= accountCount; i++)
                {
                    Console.Write($"Enter account number for account {i}: ");
                    string? accountNumber = Console.ReadLine();
                    
                    Console.Write($"Enter starting balance for account {i}: ");
                    string? balanceInput = Console.ReadLine();
                    if (decimal.TryParse(balanceInput, out decimal balance))
                    {
                        bool success = bankService.CreateAccountWithValidation(accountNumber ?? $"ACC{i}", balance);
                        if (!success)
                        {
                            i--; // Retry this account
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid balance. Using $0 as default.");
                        bankService.CreateAccountWithValidation(accountNumber ?? $"ACC{i}", 0);
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid number. Creating 3 default accounts.");
                bankService.CreateAccountWithValidation("ACC101", 5000);
                bankService.CreateAccountWithValidation("ACC102", 1200);
                bankService.CreateAccountWithValidation("ACC103", 300);
            }
            Console.WriteLine();
        }

        private static void PrintMenu()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Check Balance");
            Console.WriteLine("4. Exit");
            Console.WriteLine("-----------------------------------");
        }

        private static void HandleDeposit(IBankService bankService)
        {
            Console.Write("Enter Account Number: ");
            string? accNum = Console.ReadLine();

            Console.Write("Enter Deposit Amount: ");
            string? amountInput = Console.ReadLine();
            if (decimal.TryParse(amountInput, out decimal amount))
            {
                bankService.ProcessDeposit(accNum ?? "", amount);
            }
            else
            {
                Console.WriteLine("Invalid amount entered.");
            }
        }

        private static void HandleWithdrawal(IBankService bankService)
        {
            Console.Write("Enter Account Number: ");
            string? accNum = Console.ReadLine();

            Console.Write("Enter Withdrawal Amount: ");
            string? amountInput = Console.ReadLine();
            if (decimal.TryParse(amountInput, out decimal amount))
            {
                bankService.ProcessWithdrawal(accNum ?? "", amount);
            }
            else
            {
                Console.WriteLine("Invalid amount entered.");
            }
        }

        private static void HandleCheckBalance(IBankService bankService)
        {
            Console.Write("Enter Account Number: ");
            string? accNum = Console.ReadLine();
            bankService.DisplayAccountInfo(accNum ?? "");
        }
    }
}
