using System;

public class HandleIndexOutOfRangeException
{
    public static void Main(string[] args)
    {
        try
        {
            // Create an array with 10 elements (indices 0 through 9)
            int[] numbers = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Console.WriteLine("Array created with 10 elements.");

            // Attempt to access the 11th element at index 10, which does not exist
            Console.WriteLine("Attempting to access the element at index 10...");
            int element = numbers[10];

            Console.WriteLine($"Element at index 10 is: {element}"); // This line will not be reached
        }
        catch (IndexOutOfRangeException ex)
        {
            // Catch the specific exception for accessing an array with an invalid index
            Console.WriteLine("\n❌ Error: The index you tried to access is outside the bounds of the array.");
            Console.WriteLine($"Exception details: {ex.Message}");
        }
    }
}