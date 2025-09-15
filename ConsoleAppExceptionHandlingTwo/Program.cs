using System;

public class HandleArithmeticException
{
    public static void Main(string[] args)
    {
        try
        {
            int numerator = 10;
            int denominator = 0;

            // This line will attempt to divide by zero, throwing an ArithmeticException
            int result = numerator / denominator;

            Console.WriteLine($"Result: {result}"); // This line will not be reached
        }
        catch (ArithmeticException ex)
        {
            // Catch the specific exception for division by zero
            Console.WriteLine("❌ Error: Cannot divide by zero.");
            Console.WriteLine($"Exception details: {ex.Message}");
        }
    }
}