namespace ArraySortingDelegate.Services
{
    // Delegate definition for array operations
    public delegate int[] ArrayOperationDelegate(int[] array);

    public class ArrayOperations
    {
        // Method to sort array in ascending order
        public static int[] SortArray(int[] array)
        {
            int[] sortedArray = new int[array.Length];
            Array.Copy(array, sortedArray, array.Length);
            Array.Sort(sortedArray);
            return sortedArray;
        }

        // Method to reverse array
        public static int[] ReverseArray(int[] array)
        {
            int[] reversedArray = new int[array.Length];
            Array.Copy(array, reversedArray, array.Length);
            Array.Reverse(reversedArray);
            return reversedArray;
        }

        // Method to sort array in descending order
        public static int[] SortDescendingArray(int[] array)
        {
            int[] sortedArray = new int[array.Length];
            Array.Copy(array, sortedArray, array.Length);
            Array.Sort(sortedArray);
            Array.Reverse(sortedArray);
            return sortedArray;
        }

        // Method to shuffle array
        public static int[] ShuffleArray(int[] array)
        {
            int[] shuffledArray = new int[array.Length];
            Array.Copy(array, shuffledArray, array.Length);
            
            Random random = new Random();
            for (int i = shuffledArray.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (shuffledArray[i], shuffledArray[j]) = (shuffledArray[j], shuffledArray[i]);
            }
            
            return shuffledArray;
        }

        // Method to display array
        public static void DisplayArray(int[] array, string title)
        {
            Console.WriteLine($"\n{title}:");
            Console.WriteLine($"[{string.Join(", ", array)}]");
        }

        // Method to generate random array
        public static int[] GenerateRandomArray(int size, int minValue = 1, int maxValue = 100)
        {
            int[] array = new int[size];
            Random random = new Random();
            
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(minValue, maxValue + 1);
            }
            
            return array;
        }

        // Method to execute array operation using delegate
        public static void ExecuteArrayOperation(ArrayOperationDelegate operation, int[] array, string operationName)
        {
            Console.WriteLine($"\nExecuting {operationName}...");
            int[] result = operation(array);
            DisplayArray(result, $"Result after {operationName}");
        }
    }
}
