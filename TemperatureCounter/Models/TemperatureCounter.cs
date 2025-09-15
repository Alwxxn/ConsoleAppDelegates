using TemperatureCounter.Models;

namespace TemperatureCounter.Models
{
    public class TemperatureCounter
    {
        private double _currentTemperature;
        private double _previousTemperature;
        private readonly List<double> _temperatureHistory;
        private readonly Random _random;

        // Event declaration for critical temperature
        public event EventHandler<TemperatureEventArgs>? CriticalTemperatureReached;
        public event EventHandler<TemperatureEventArgs>? TemperatureChanged;

        public TemperatureCounter(double initialTemperature = 20.0)
        {
            _currentTemperature = initialTemperature;
            _previousTemperature = initialTemperature;
            _temperatureHistory = new List<double> { initialTemperature };
            _random = new Random();
        }

        public double CurrentTemperature 
        { 
            get => _currentTemperature;
            private set
            {
                _previousTemperature = _currentTemperature;
                _currentTemperature = value;
                _temperatureHistory.Add(value);
                
                // Raise temperature changed event
                OnTemperatureChanged();
                
                // Check for critical temperature and raise event if necessary
                if (IsCriticalTemperature())
                {
                    OnCriticalTemperatureReached();
                }
            }
        }

        public double PreviousTemperature => _previousTemperature;
        public IReadOnlyList<double> TemperatureHistory => _temperatureHistory.AsReadOnly();

        public void IncreaseTemperature(double amount = 1.0)
        {
            if (amount <= 0)
                throw new ArgumentException("Temperature increase amount must be positive.", nameof(amount));

            CurrentTemperature += amount;
        }

        public void DecreaseTemperature(double amount = 1.0)
        {
            if (amount <= 0)
                throw new ArgumentException("Temperature decrease amount must be positive.", nameof(amount));

            CurrentTemperature -= amount;
        }

        public void SetTemperature(double temperature)
        {
            CurrentTemperature = temperature;
        }

        public void SimulateRandomTemperatureChange()
        {
            // Simulate temperature change between -5 and +5 degrees
            double change = (_random.NextDouble() - 0.5) * 10;
            CurrentTemperature += change;
        }

        public void SimulateWeatherConditions()
        {
            // Simulate different weather conditions
            var conditions = new[]
            {
                "Sunny Day", "Cloudy Day", "Rainy Day", "Stormy Day", 
                "Cold Night", "Hot Afternoon", "Freezing Morning", "Heat Wave"
            };

            string condition = conditions[_random.Next(conditions.Length)];
            double temperatureChange = condition switch
            {
                "Sunny Day" => _random.NextDouble() * 8 + 2, // +2 to +10
                "Cloudy Day" => (_random.NextDouble() - 0.5) * 4, // -2 to +2
                "Rainy Day" => -(_random.NextDouble() * 6 + 2), // -2 to -8
                "Stormy Day" => -(_random.NextDouble() * 8 + 5), // -5 to -13
                "Cold Night" => -(_random.NextDouble() * 12 + 8), // -8 to -20
                "Hot Afternoon" => _random.NextDouble() * 15 + 10, // +10 to +25
                "Freezing Morning" => -(_random.NextDouble() * 15 + 10), // -10 to -25
                "Heat Wave" => _random.NextDouble() * 20 + 15, // +15 to +35
                _ => 0
            };

            Console.WriteLine($"\nWeather Condition: {condition}");
            CurrentTemperature += temperatureChange;
        }

        private bool IsCriticalTemperature()
        {
            return _currentTemperature > 100.0 || _currentTemperature < 0.0;
        }

        public string GetTemperatureStatus()
        {
            return _currentTemperature switch
            {
                > 100 => "CRITICAL - Temperature too high!",
                < 0 => "CRITICAL - Temperature too low!",
                > 80 => "Very Hot",
                > 60 => "Hot",
                > 40 => "Warm",
                > 20 => "Moderate",
                > 5 => "Cool",
                > -5 => "Cold",
                _ => "Very Cold"
            };
        }

        public double GetAverageTemperature()
        {
            return _temperatureHistory.Count > 0 ? _temperatureHistory.Average() : 0;
        }

        public double GetMaxTemperature()
        {
            return _temperatureHistory.Count > 0 ? _temperatureHistory.Max() : 0;
        }

        public double GetMinTemperature()
        {
            return _temperatureHistory.Count > 0 ? _temperatureHistory.Min() : 0;
        }

        public int GetCriticalTemperatureCount()
        {
            return _temperatureHistory.Count(t => t > 100 || t < 0);
        }

        protected virtual void OnCriticalTemperatureReached()
        {
            string message = _currentTemperature > 100 
                ? "Critical temperature reached! Temperature is too high!"
                : "Critical temperature reached! Temperature is too low!";

            var args = new TemperatureEventArgs(_currentTemperature, _previousTemperature, message);
            CriticalTemperatureReached?.Invoke(this, args);
        }

        protected virtual void OnTemperatureChanged()
        {
            string message = $"Temperature changed from {_previousTemperature:F1}°C to {_currentTemperature:F1}°C";
            var args = new TemperatureEventArgs(_currentTemperature, _previousTemperature, message);
            TemperatureChanged?.Invoke(this, args);
        }

        public void DisplayTemperatureInfo()
        {
            Console.WriteLine($"\n=== Temperature Information ===");
            Console.WriteLine($"Current Temperature: {_currentTemperature:F1}°C");
            Console.WriteLine($"Previous Temperature: {_previousTemperature:F1}°C");
            Console.WriteLine($"Status: {GetTemperatureStatus()}");
            Console.WriteLine($"Average Temperature: {GetAverageTemperature():F1}°C");
            Console.WriteLine($"Max Temperature: {GetMaxTemperature():F1}°C");
            Console.WriteLine($"Min Temperature: {GetMinTemperature():F1}°C");
            Console.WriteLine($"Critical Temperature Count: {GetCriticalTemperatureCount()}");
            Console.WriteLine($"Total Readings: {_temperatureHistory.Count}");
        }

        public void DisplayTemperatureHistory()
        {
            Console.WriteLine($"\n=== Temperature History (Last 10 readings) ===");
            var recentReadings = _temperatureHistory.TakeLast(10).ToList();
            
            for (int i = 0; i < recentReadings.Count; i++)
            {
                string status = recentReadings[i] switch
                {
                    > 100 or < 0 => " [CRITICAL]",
                    _ => ""
                };
                Console.WriteLine($"Reading {i + 1}: {recentReadings[i]:F1}°C{status}");
            }
        }
    }
}
