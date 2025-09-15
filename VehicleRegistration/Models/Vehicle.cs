namespace VehicleRegistration.Models
{
    public class Vehicle
    {
        public string LicenseNumber { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Color { get; set; } = string.Empty;
        public string VIN { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string VehicleType { get; set; } = string.Empty; // Car, Truck, Motorcycle, etc.

        public Vehicle(string licenseNumber, string make, string model, int year, string color, 
                      string vin, string ownerName, string vehicleType = "Car")
        {
            LicenseNumber = licenseNumber;
            Make = make;
            Model = model;
            Year = year;
            Color = color;
            VIN = vin;
            OwnerName = ownerName;
            VehicleType = vehicleType;
            RegistrationDate = DateTime.Now;
            ExpiryDate = DateTime.Now.AddYears(1);
        }

        public bool IsExpired()
        {
            return DateTime.Now > ExpiryDate;
        }

        public int DaysUntilExpiry()
        {
            return (ExpiryDate - DateTime.Now).Days;
        }

        public override string ToString()
        {
            return $"{Year} {Make} {Model} ({Color}) - License: {LicenseNumber}, Owner: {OwnerName}, Type: {VehicleType}, Expires: {ExpiryDate:MM/dd/yyyy}";
        }
    }
}
