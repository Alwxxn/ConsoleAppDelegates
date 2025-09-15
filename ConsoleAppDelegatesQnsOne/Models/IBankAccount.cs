using System;

namespace BankingSystem
{
    // Interface for bank account operations
    public interface IBankAccount
    {
        string AccountNumber { get; }
        decimal Balance { get; }
        
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        
        // Events
        event Action<string, decimal>? MoneyDeposited;
        event Action<string, decimal>? MoneyWithdrawn;
        event Action<string>? InsufficientFunds;
    }
}
