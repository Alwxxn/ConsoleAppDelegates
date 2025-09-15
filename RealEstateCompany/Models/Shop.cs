namespace RealEstateCompany.Models
{
    public class Shop : Estate
    {
        public string BusinessType { get; set; } = string.Empty; // Retail, Restaurant, Office, etc.
        public bool HasParking { get; set; }
        public bool HasStorage { get; set; }
        public bool IsGroundFloor { get; set; }
        public bool HasDisplayWindow { get; set; }
        public string ZoningType { get; set; } = string.Empty; // Commercial, Mixed-use, etc.
        public bool HasRestroom { get; set; }

        public Shop(string id, double area, decimal pricePerSquareMeter, string location,
                   string businessType, bool hasParking = false, bool hasStorage = false, 
                   bool isGroundFloor = true, bool hasDisplayWindow = false, string zoningType = "Commercial",
                   bool hasRestroom = false)
            : base(id, area, pricePerSquareMeter, location)
        {
            BusinessType = businessType;
            HasParking = hasParking;
            HasStorage = hasStorage;
            IsGroundFloor = isGroundFloor;
            HasDisplayWindow = hasDisplayWindow;
            ZoningType = zoningType;
            HasRestroom = hasRestroom;
        }

        public override string GetEstateType()
        {
            return "Shop";
        }

        public override string GetDetails()
        {
            var details = base.GetDetails();
            details += $", Business Type: {BusinessType}, Zoning: {ZoningType}";
            
            var features = new List<string>();
            if (HasParking) features.Add("Parking");
            if (HasStorage) features.Add("Storage");
            if (IsGroundFloor) features.Add("Ground Floor");
            if (HasDisplayWindow) features.Add("Display Window");
            if (HasRestroom) features.Add("Restroom");
            
            if (features.Count > 0)
            {
                details += $", Features: {string.Join(", ", features)}";
            }
            
            return details;
        }
    }
}
