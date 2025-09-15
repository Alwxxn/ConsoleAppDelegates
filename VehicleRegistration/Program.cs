using VehicleRegistration.Models;
using VehicleRegistration.Services;

namespace VehicleRegistration
{
    class Program
    {
        static void Main(string[] args)
        {
            var registrationService = new VehicleRegistrationService();
            
            Console.WriteLine("=== Vehicle Registration System ===\n");

            // Register sample vehicles
            registrationService.RegisterVehicle(new Vehicle(
                "ABC123", "Toyota", "Camry", 2020, "Silver", 
                "1HGBH41JXMN109186", "John Smith", "Car"));

            registrationService.RegisterVehicle(new Vehicle(
                "XYZ789", "Honda", "Civic", 2019, "Blue", 
                "1HGBH41JXMN109187", "Jane Doe", "Car"));

            registrationService.RegisterVehicle(new Vehicle(
                "DEF456", "Ford", "F-150", 2021, "Black", 
                "1HGBH41JXMN109188", "John Smith", "Truck"));

            registrationService.RegisterVehicle(new Vehicle(
                "GHI012", "BMW", "X5", 2022, "White", 
                "1HGBH41JXMN109189", "Mike Johnson", "SUV"));

            registrationService.RegisterVehicle(new Vehicle(
                "JKL345", "Harley", "Davidson", 2020, "Red", 
                "1HGBH41JXMN109190", "Sarah Wilson", "Motorcycle"));

            // Display all vehicles
            registrationService.DisplayAllVehicles();

            // Test license number lookup
            Console.WriteLine("\n=== License Number Lookup ===");
            
            var vehicle1 = registrationService.GetVehicleByLicenseNumber("ABC123");
            Console.WriteLine($"Lookup 'ABC123': {(vehicle1 != null ? vehicle1.ToString() : "Not found")}");

            var vehicle2 = registrationService.GetVehicleByLicenseNumber("XYZ789");
            Console.WriteLine($"Lookup 'XYZ789': {(vehicle2 != null ? vehicle2.ToString() : "Not found")}");

            var vehicle3 = registrationService.GetVehicleByLicenseNumber("INVALID");
            Console.WriteLine($"Lookup 'INVALID': {(vehicle3 != null ? vehicle3.ToString() : "Not found")}");

            // Display vehicles by owner
            registrationService.DisplayVehiclesByOwner("John Smith");
            registrationService.DisplayVehiclesByOwner("Jane Doe");

            // Display expiring vehicles (simulate some expired ones by creating old vehicles)
            var oldVehicle = new Vehicle(
                "OLD999", "Chevrolet", "Malibu", 2018, "Green", 
                "1HGBH41JXMN109191", "Old Owner", "Car");
            oldVehicle.ExpiryDate = DateTime.Now.AddDays(-10); // Expired 10 days ago
            registrationService.RegisterVehicle(oldVehicle);

            registrationService.DisplayExpiredVehicles();
            registrationService.DisplayVehiclesExpiringSoon(60);

            Console.WriteLine($"\nTotal registered vehicles: {registrationService.GetTotalRegisteredVehicles()}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
