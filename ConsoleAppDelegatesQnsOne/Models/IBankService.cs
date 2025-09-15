using System;

namespace BankingSystem
{
    // Interface for bank service operations
    public interface IBankService
    {
        bool CreateAccountWithValidation(string accountNumber, decimal initialBalance);
        bool ProcessDeposit(string accountNumber, decimal amount);
        bool ProcessWithdrawal(string accountNumber, decimal amount);
        void DisplayAccountInfo(string accountNumber);
        bool ValidateTransaction(string accountNumber, decimal amount, string transactionType);
    }
}
