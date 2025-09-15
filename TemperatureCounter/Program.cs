using TemperatureCounter.Models;

namespace TemperatureCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Temperature Counter with Critical Temperature Monitoring ===\n");

            // Create temperature counter instance
            var temperatureCounter = new TemperatureCounter(25.0); // Start at 25°C

            // Subscribe to events
            temperatureCounter.CriticalTemperatureReached += OnCriticalTemperatureReached;
            temperatureCounter.TemperatureChanged += OnTemperatureChanged;

            Console.WriteLine("Temperature monitoring started. Initial temperature: 25.0°C");
            Console.WriteLine("Critical temperature range: < 0°C or > 100°C\n");

            // Menu-driven interface
            while (true)
            {
                Console.WriteLine("\n=== Temperature Control Menu ===");
                Console.WriteLine("1. Increase Temperature");
                Console.WriteLine("2. Decrease Temperature");
                Console.WriteLine("3. Set Specific Temperature");
                Console.WriteLine("4. Simulate Random Change");
                Console.WriteLine("5. Simulate Weather Conditions");
                Console.WriteLine("6. Display Temperature Information");
                Console.WriteLine("7. Display Temperature History");
                Console.WriteLine("8. Reset Temperature");
                Console.WriteLine("9. Exit");
                Console.Write("Enter your choice (1-9): ");

                string? choice = Console.ReadLine();

                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                try
                {
                    switch (choice.Trim())
                    {
                        case "1":
                            Console.Write("Enter temperature increase amount (default 5.0): ");
                            if (double.TryParse(Console.ReadLine(), out double increaseAmount))
                                temperatureCounter.IncreaseTemperature(increaseAmount);
                            else
                                temperatureCounter.IncreaseTemperature(5.0);
                            break;

                        case "2":
                            Console.Write("Enter temperature decrease amount (default 5.0): ");
                            if (double.TryParse(Console.ReadLine(), out double decreaseAmount))
                                temperatureCounter.DecreaseTemperature(decreaseAmount);
                            else
                                temperatureCounter.DecreaseTemperature(5.0);
                            break;

                        case "3":
                            Console.Write("Enter new temperature: ");
                            if (double.TryParse(Console.ReadLine(), out double newTemperature))
                                temperatureCounter.SetTemperature(newTemperature);
                            else
                                Console.WriteLine("Invalid temperature value.");
                            break;

                        case "4":
                            temperatureCounter.SimulateRandomTemperatureChange();
                            break;

                        case "5":
                            temperatureCounter.SimulateWeatherConditions();
                            break;

                        case "6":
                            temperatureCounter.DisplayTemperatureInfo();
                            break;

                        case "7":
                            temperatureCounter.DisplayTemperatureHistory();
                            break;

                        case "8":
                            temperatureCounter.SetTemperature(25.0);
                            Console.WriteLine("Temperature reset to 25.0°C");
                            break;

                        case "9":
                            Console.WriteLine("Thank you for using Temperature Counter!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1-9.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        // Event handler for critical temperature
        static void OnCriticalTemperatureReached(object? sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n🚨 CRITICAL ALERT! 🚨");
            Console.WriteLine($"Time: {e.Timestamp:HH:mm:ss}");
            Console.WriteLine($"Temperature: {e.CurrentTemperature:F1}°C");
            Console.WriteLine($"Previous: {e.PreviousTemperature:F1}°C");
            Console.WriteLine($"Message: {e.Message}");
            Console.WriteLine("⚠️  IMMEDIATE ACTION REQUIRED! ⚠️");
            Console.ResetColor();
        }

        // Event handler for temperature changes
        static void OnTemperatureChanged(object? sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n📊 Temperature Update: {e.Message}");
            Console.WriteLine($"Current Status: {((TemperatureCounter)sender!).GetTemperatureStatus()}");
            Console.ResetColor();
        }
    }
}
