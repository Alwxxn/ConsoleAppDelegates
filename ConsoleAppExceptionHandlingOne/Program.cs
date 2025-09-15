using System;

// A custom exception class for invalid age
public class InvalidAgeException : Exception
{
    public InvalidAgeException(string message) : base(message)
    {
    }
}

public class Employee
{
    public string Name { get; set; }
    private int _age;

    public int Age
    {
        get { return _age; }
        set
        {
            // Validate age when setting the property
            if (value < 18 || value > 60)
            {
                // Throw the custom exception if age is out of range
                throw new InvalidAgeException("Employee age must be between 18 and 60.");
            }
            _age = value;
        }
    }
}

public class AgeValidation
{
    public static void Main(string[] args)
    {
        try
        {
            Employee emp = new Employee();

            Console.Write("Enter Employee Name: ");
            emp.Name = Console.ReadLine();

            Console.Write("Enter Employee Age: ");
            emp.Age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\n✅ Employee data saved successfully!");
            Console.WriteLine($"Name: {emp.Name}, Age: {emp.Age}");
        }
        catch (InvalidAgeException ex)
        {
            // Catch the specific age-related exception
            Console.WriteLine($"\n❌ Error: {ex.Message}");
        }
        catch (FormatException)
        {
            // Catch errors if the user enters non-numeric input for age
            Console.WriteLine("\n❌ Error: Invalid input. Please enter a valid number for age.");
        }
        catch (Exception ex)
        {
            // Catch any other unexpected errors
            Console.WriteLine($"\n❌ An unexpected error occurred: {ex.Message}");
        }
    }
}