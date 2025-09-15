using System;
using System.Collections.Generic;

namespace BankingSystem
{
    // Main bank class implementing interface
    public class Bank : IBank
    {
        // Private field for encapsulation
        private readonly Dictionary<string, IBankAccount> _accounts = new Dictionary<string, IBankAccount>();

        // Create new account
        public void CreateAccount(string accountNumber, decimal initialBalance)
        {
            if (!ValidationUtilities.IsValidAccountNumber(accountNumber))
                throw new ArgumentException("Account number cannot be empty and must be at least 3 characters");

            if (_accounts.ContainsKey(accountNumber))
            {
                throw new InvalidOperationException($"Account {accountNumber} already exists");
            }

            var account = new BankAccount(accountNumber, initialBalance);
            
            // Connect events to handlers
            account.MoneyDeposited += OnMoneyDeposited;
            account.MoneyWithdrawn += OnMoneyWithdrawn;
            account.InsufficientFunds += OnInsufficientFunds;
            
            _accounts[accountNumber] = account;
            Console.WriteLine($"Account {accountNumber} created with {ValidationUtilities.FormatCurrency(initialBalance)}");
        }

        // Get account by number
        public IBankAccount? GetAccount(string accountNumber)
        {
            if (!ValidationUtilities.IsValidAccountNumber(accountNumber))
                return null;

            _accounts.TryGetValue(accountNumber, out var account);
            return account;
        }

        // Do transaction using delegate
        public void PerformTransaction(string accountNumber, decimal amount, TransactionDelegate transaction)
        {
            var account = GetAccount(accountNumber);
            if (account == null)
            {
                throw new InvalidOperationException($"Account {accountNumber} not found");
            }

            if (!ValidationUtilities.IsValidAmount(amount))
            {
                throw new ArgumentException("Amount must be between $0.01 and $1,000,000");
            }

            Console.WriteLine($"Processing transaction for account {accountNumber}...");
            
            try
            {
                // Use delegate to do the transaction
                transaction(amount);
                Console.WriteLine("Transaction done.");
                CheckBalance(accountNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Transaction failed: {ex.Message}");
            }
        }

        // Show account balance
        public void CheckBalance(string accountNumber)
        {
            var account = GetAccount(accountNumber);
            if (account != null)
            {
                Console.WriteLine($"Account {account.AccountNumber} has {ValidationUtilities.FormatCurrency(account.Balance)}");
            }
            else
            {
                Console.WriteLine($"Error: Account {accountNumber} not found.");
            }
        }

        // Private methods for event handling
        private void OnMoneyDeposited(string accountNumber, decimal amount)
        {
            Console.WriteLine($"Event: {ValidationUtilities.FormatCurrency(amount)} deposited to account {accountNumber}");
        }

        private void OnMoneyWithdrawn(string accountNumber, decimal amount)
        {
            Console.WriteLine($"Event: {ValidationUtilities.FormatCurrency(amount)} withdrawn from account {accountNumber}");
        }

        private void OnInsufficientFunds(string accountNumber)
        {
            Console.WriteLine($"Event: Not enough money in account {accountNumber}");
        }

        // Private method to get total accounts count
        private int GetTotalAccounts()
        {
            return _accounts.Count;
        }
    }
}
