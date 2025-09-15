using System;

namespace BankingSystem
{
    // Utility class for validation
    public static class ValidationUtilities
    {
        // Validate account number
        public static bool IsValidAccountNumber(string? accountNumber)
        {
            return !string.IsNullOrWhiteSpace(accountNumber) && accountNumber.Length >= 3;
        }

        // Validate amount
        public static bool IsValidAmount(decimal amount)
        {
            return amount > 0 && amount <= 1000000; // Max 1 million
        }

        // Validate initial balance
        public static bool IsValidInitialBalance(decimal balance)
        {
            return balance >= 0 && balance <= 1000000; // Max 1 million
        }

        // Get validation error message
        public static string GetValidationMessage(string? accountNumber, decimal amount, bool isInitialBalance = false)
        {
            if (!IsValidAccountNumber(accountNumber))
            {
                return "Account number must be at least 3 characters long and not empty.";
            }

            if (isInitialBalance)
            {
                if (!IsValidInitialBalance(amount))
                {
                    return "Initial balance must be between $0 and $1,000,000.";
                }
            }
            else
            {
                if (!IsValidAmount(amount))
                {
                    return "Amount must be between $0.01 and $1,000,000.";
                }
            }

            return string.Empty; // No error
        }

        // Check for suspicious activity
        public static bool IsSuspiciousDeposit(decimal amount)
        {
            return amount > 10000; // Over $10,000
        }

        public static bool IsSuspiciousWithdrawal(decimal amount)
        {
            return amount > 5000; // Over $5,000
        }

        // Format currency
        public static string FormatCurrency(decimal amount)
        {
            return $"${amount:N2}";
        }

        // Validate account number format
        public static bool IsValidAccountFormat(string? accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                return false;

            // Check if it contains only letters and numbers
            foreach (char c in accountNumber)
            {
                if (!char.IsLetterOrDigit(c))
                    return false;
            }

            return true;
        }
    }
}
