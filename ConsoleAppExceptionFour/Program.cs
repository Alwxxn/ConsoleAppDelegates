using System;

public class HandlePrimeNumberException
{
    // A helper method to check if a number is prime
    public static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        // Check for factors from 3 up to the square root of the number
        for (int i = 3; i <= Math.Sqrt(number); i += 2)
        {
            if (number % i == 0) return false;
        }
        return true;
    }

    // Method that gets a random number and throws an exception if it's prime
    public static int GetRandomNumber()
    {
        Random rand = new Random();
        int randomNumber = rand.Next(1, 101); // Generates a number between 1 and 100

        Console.WriteLine($"Generated random number: {randomNumber}");

        if (IsPrime(randomNumber))
        {
            // If the number is prime, throw a new exception
            throw new Exception($"💥 Prime Number Exception: {randomNumber} is a prime number!");
        }

        // If not prime, return the number
        return randomNumber;
    }

    public static void Main(string[] args)
    {
        try
        {
            // Call the method that might throw an exception
            int nonPrimeNumber = GetRandomNumber();
            Console.WriteLine($" Success! The non-prime number {nonPrimeNumber} was processed.");
        }
        catch (Exception ex)
        {
            // Catch the exception if a prime number was generated
            Console.WriteLine($"\nCaught an exception as expected.");
            Console.WriteLine($"Error Details: {ex.Message}");
        }
    }
}