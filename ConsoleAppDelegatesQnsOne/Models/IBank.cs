using System;

namespace BankingSystem
{
    // Interface for bank operations
    public interface IBank
    {
        void CreateAccount(string accountNumber, decimal initialBalance);
        IBankAccount? GetAccount(string accountNumber);
        void PerformTransaction(string accountNumber, decimal amount, TransactionDelegate transaction);
        void CheckBalance(string accountNumber);
    }
}
