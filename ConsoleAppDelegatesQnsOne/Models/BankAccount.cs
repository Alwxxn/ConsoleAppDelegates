using System;

namespace BankingSystem
{
    // Bank account class implementing interface
    public class BankAccount : IBankAccount
    {
        // Private fields for encapsulation
        private readonly string _accountNumber;
        private decimal _balance;

        // Public properties
        public string AccountNumber => _accountNumber;
        public decimal Balance => _balance;

        // Events for account activities
        public event Action<string, decimal>? MoneyDeposited;
        public event Action<string, decimal>? MoneyWithdrawn;
        public event Action<string>? InsufficientFunds;

        // Constructor
        public BankAccount(string accountNumber, decimal initialBalance = 0)
        {
            if (!ValidationUtilities.IsValidAccountNumber(accountNumber))
                throw new ArgumentException("Account number cannot be empty and must be at least 3 characters");
            
            if (!ValidationUtilities.IsValidInitialBalance(initialBalance))
                throw new ArgumentException("Initial balance must be between $0 and $1,000,000");

            _accountNumber = accountNumber;
            _balance = initialBalance;
        }

        // Add money to account
        public void Deposit(decimal amount)
        {
            if (!ValidationUtilities.IsValidAmount(amount))
                throw new ArgumentException("Deposit amount must be between $0.01 and $1,000,000");

            _balance += amount;
            // Trigger event when money is deposited
            MoneyDeposited?.Invoke(_accountNumber, amount);
        }

        // Take money from account
        public void Withdraw(decimal amount)
        {
            if (!ValidationUtilities.IsValidAmount(amount))
                throw new ArgumentException("Withdrawal amount must be between $0.01 and $1,000,000");

            if (amount > _balance)
            {
                // Trigger event when not enough money
                InsufficientFunds?.Invoke(_accountNumber);
                throw new InvalidOperationException("Not enough money in account");
            }

            _balance -= amount;
            // Trigger event when money is withdrawn
            MoneyWithdrawn?.Invoke(_accountNumber, amount);
        }

        // Private method for internal validation
        private bool IsValidAmount(decimal amount)
        {
            return ValidationUtilities.IsValidAmount(amount);
        }
    }
}
