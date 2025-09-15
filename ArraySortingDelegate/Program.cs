using ArraySortingDelegate.Services;

namespace ArraySortingDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Array Operations with Delegates ===\n");

            // Create delegate instances
            ArrayOperationDelegate sortDelegate = ArrayOperations.SortArray;
            ArrayOperationDelegate reverseDelegate = ArrayOperations.ReverseArray;
            ArrayOperationDelegate sortDescendingDelegate = ArrayOperations.SortDescendingArray;
            ArrayOperationDelegate shuffleDelegate = ArrayOperations.ShuffleArray;

            // Generate sample array
            int[] originalArray = ArrayOperations.GenerateRandomArray(10, 1, 50);
            ArrayOperations.DisplayArray(originalArray, "Original Array");

            // Menu-driven interface
            while (true)
            {
                Console.WriteLine("\n=== Array Operations Menu ===");
                Console.WriteLine("1. Sort Array (Ascending)");
                Console.WriteLine("2. Reverse Array");
                Console.WriteLine("3. Sort Array (Descending)");
                Console.WriteLine("4. Shuffle Array");
                Console.WriteLine("5. Generate New Array");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice (1-6): ");

                string? choice = Console.ReadLine();
                
                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                switch (choice.Trim())
                {
                    case "1":
                        ExecuteWithDelegate(sortDelegate, originalArray, "Sort (Ascending)");
                        break;
                    case "2":
                        ExecuteWithDelegate(reverseDelegate, originalArray, "Reverse");
                        break;
                    case "3":
                        ExecuteWithDelegate(sortDescendingDelegate, originalArray, "Sort (Descending)");
                        break;
                    case "4":
                        ExecuteWithDelegate(shuffleDelegate, originalArray, "Shuffle");
                        break;
                    case "5":
                        originalArray = ArrayOperations.GenerateRandomArray(10, 1, 50);
                        ArrayOperations.DisplayArray(originalArray, "New Original Array");
                        break;
                    case "6":
                        Console.WriteLine("Thank you for using Array Operations!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1-6.");
                        break;
                }
            }
        }

        static void ExecuteWithDelegate(ArrayOperationDelegate operation, int[] array, string operationName)
        {
            // Demonstrate delegate usage
            Console.WriteLine($"\nUsing delegate to execute: {operationName}");
            ArrayOperations.ExecuteArrayOperation(operation, array, operationName);
            
            // Show delegate information
            Console.WriteLine($"\nDelegate Information:");
            Console.WriteLine($"Method: {operation.Method.Name}");
            Console.WriteLine($"Target: {operation.Target?.GetType().Name ?? "Static Method"}");
        }
    }
}
