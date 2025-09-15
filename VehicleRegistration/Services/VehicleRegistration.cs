using System.Collections;
using VehicleRegistration.Models;

namespace VehicleRegistration.Services
{
    public class VehicleRegistrationService
    {
        private readonly Dictionary<string, Vehicle> _vehicleRegistry;

        public VehicleRegistrationService()
        {
            _vehicleRegistry = new Dictionary<string, Vehicle>();
        }

        public void RegisterVehicle(Vehicle vehicle)
        {
            if (_vehicleRegistry.ContainsKey(vehicle.LicenseNumber))
            {
                Console.WriteLine($"Vehicle with license number {vehicle.LicenseNumber} is already registered.");
                return;
            }

            _vehicleRegistry.Add(vehicle.LicenseNumber, vehicle);
            Console.WriteLine($"Vehicle registered successfully: {vehicle}");
        }

        public Vehicle? GetVehicleByLicenseNumber(string licenseNumber)
        {
            string normalizedLicense = licenseNumber.ToUpper().Replace(" ", "");
            
            if (_vehicleRegistry.ContainsKey(normalizedLicense))
            {
                return _vehicleRegistry[normalizedLicense];
            }

            // Try to find with partial match
            var matchingVehicle = _vehicleRegistry.FirstOrDefault(kvp => 
                kvp.Key.Contains(normalizedLicense) || 
                normalizedLicense.Contains(kvp.Key));

            return matchingVehicle.Value;
        }

        public List<Vehicle> GetAllVehicles()
        {
            return _vehicleRegistry.Values.ToList();
        }

        public List<Vehicle> GetVehiclesByOwner(string ownerName)
        {
            return _vehicleRegistry.Values
                .Where(v => v.OwnerName.Equals(ownerName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Vehicle> GetExpiredVehicles()
        {
            return _vehicleRegistry.Values.Where(v => v.IsExpired()).ToList();
        }

        public List<Vehicle> GetVehiclesExpiringSoon(int daysThreshold = 30)
        {
            return _vehicleRegistry.Values
                .Where(v => v.DaysUntilExpiry() <= daysThreshold && !v.IsExpired())
                .ToList();
        }

        public void DisplayAllVehicles()
        {
            Console.WriteLine("\n=== All Registered Vehicles ===");
            if (_vehicleRegistry.Count == 0)
            {
                Console.WriteLine("No vehicles registered.");
                return;
            }

            foreach (var vehicle in _vehicleRegistry.Values)
            {
                Console.WriteLine(vehicle.ToString());
            }
        }

        public void DisplayVehiclesByOwner(string ownerName)
        {
            var vehicles = GetVehiclesByOwner(ownerName);
            Console.WriteLine($"\n=== Vehicles owned by {ownerName} ===");
            
            if (vehicles.Count == 0)
            {
                Console.WriteLine($"No vehicles found for owner: {ownerName}");
                return;
            }

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle.ToString());
            }
        }

        public void DisplayExpiredVehicles()
        {
            var expiredVehicles = GetExpiredVehicles();
            Console.WriteLine("\n=== Expired Vehicle Registrations ===");
            
            if (expiredVehicles.Count == 0)
            {
                Console.WriteLine("No expired registrations found.");
                return;
            }

            foreach (var vehicle in expiredVehicles)
            {
                Console.WriteLine($"{vehicle} - EXPIRED {(DateTime.Now - vehicle.ExpiryDate).Days} days ago");
            }
        }

        public void DisplayVehiclesExpiringSoon(int daysThreshold = 30)
        {
            var expiringSoon = GetVehiclesExpiringSoon(daysThreshold);
            Console.WriteLine($"\n=== Vehicles Expiring Within {daysThreshold} Days ===");
            
            if (expiringSoon.Count == 0)
            {
                Console.WriteLine($"No vehicles expiring within {daysThreshold} days.");
                return;
            }

            foreach (var vehicle in expiringSoon)
            {
                Console.WriteLine($"{vehicle} - Expires in {vehicle.DaysUntilExpiry()} days");
            }
        }

        public int GetTotalRegisteredVehicles()
        {
            return _vehicleRegistry.Count;
        }
    }
}
