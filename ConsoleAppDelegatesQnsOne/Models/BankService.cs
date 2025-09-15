using System;

namespace BankingSystem
{
    // Service class for bank operations implementing interface
    public class BankService : IBankService
    {
        private readonly IBank _bank;

        // Constructor injection
        public BankService(IBank bank)
        {
            _bank = bank ?? throw new ArgumentNullException(nameof(bank));
        }

        // Business logic for creating account with validation
        public bool CreateAccountWithValidation(string accountNumber, decimal initialBalance)
        {
            try
            {
                // Use validation utilities
                string validationMessage = ValidationUtilities.GetValidationMessage(accountNumber, initialBalance, true);
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    Console.WriteLine($"Validation Error: {validationMessage}");
                    return false;
                }

                // Check account format
                if (!ValidationUtilities.IsValidAccountFormat(accountNumber))
                {
                    Console.WriteLine("Validation Error: Account number can only contain letters and numbers.");
                    return false;
                }

                // Additional business rules
                if (initialBalance > 1000000)
                {
                    Console.WriteLine("Warning: Large initial deposit detected.");
                }

                _bank.CreateAccount(accountNumber, initialBalance);
                Console.WriteLine($"Account {accountNumber} created successfully with {ValidationUtilities.FormatCurrency(initialBalance)}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create account: {ex.Message}");
                return false;
            }
        }

        // Business logic for processing deposit
        public bool ProcessDeposit(string accountNumber, decimal amount)
        {
            try
            {
                // Validate transaction
                if (!ValidateTransaction(accountNumber, amount, "deposit"))
                {
                    return false;
                }

                var account = _bank.GetAccount(accountNumber);
                if (account == null)
                {
                    Console.WriteLine($"Account {accountNumber} not found.");
                    return false;
                }

                // Business rule: Check for suspicious activity
                if (ValidationUtilities.IsSuspiciousDeposit(amount))
                {
                    Console.WriteLine("Large deposit detected - may require additional verification.");
                }

                TransactionDelegate depositHandler = account.Deposit;
                _bank.PerformTransaction(accountNumber, amount, depositHandler);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deposit failed: {ex.Message}");
                return false;
            }
        }

        // Business logic for processing withdrawal
        public bool ProcessWithdrawal(string accountNumber, decimal amount)
        {
            try
            {
                // Validate transaction
                if (!ValidateTransaction(accountNumber, amount, "withdrawal"))
                {
                    return false;
                }

                var account = _bank.GetAccount(accountNumber);
                if (account == null)
                {
                    Console.WriteLine($"Account {accountNumber} not found.");
                    return false;
                }

                // Business rule: Check for suspicious activity
                if (ValidationUtilities.IsSuspiciousWithdrawal(amount))
                {
                    Console.WriteLine("Large withdrawal detected - may require additional verification.");
                }

                TransactionDelegate withdrawHandler = account.Withdraw;
                _bank.PerformTransaction(accountNumber, amount, withdrawHandler);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Withdrawal failed: {ex.Message}");
                return false;
            }
        }

        // Get account information
        public void DisplayAccountInfo(string accountNumber)
        {
            try
            {
                if (!ValidationUtilities.IsValidAccountNumber(accountNumber))
                {
                    Console.WriteLine("Invalid account number format.");
                    return;
                }

                _bank.CheckBalance(accountNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get account info: {ex.Message}");
            }
        }

        // Validate transaction parameters
        public bool ValidateTransaction(string accountNumber, decimal amount, string transactionType)
        {
            // Use validation utilities
            string validationMessage = ValidationUtilities.GetValidationMessage(accountNumber, amount);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                Console.WriteLine($"Validation Error: {validationMessage}");
                return false;
            }

            // Check account format
            if (!ValidationUtilities.IsValidAccountFormat(accountNumber))
            {
                Console.WriteLine("Validation Error: Account number can only contain letters and numbers.");
                return false;
            }

            // Additional transaction-specific validations
            if (transactionType.ToLower() == "withdrawal")
            {
                var account = _bank.GetAccount(accountNumber);
                if (account != null && amount > account.Balance)
                {
                    Console.WriteLine("Validation Error: Insufficient funds for withdrawal.");
                    return false;
                }
            }

            return true;
        }

        // Private helper method for logging
        private void LogTransaction(string accountNumber, decimal amount, string transactionType)
        {
            Console.WriteLine($"Transaction Log: {transactionType} of {ValidationUtilities.FormatCurrency(amount)} for account {accountNumber}");
        }
    }
}
