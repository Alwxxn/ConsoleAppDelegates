namespace RealEstateCompany.Models
{
    public class Apartment : Estate
    {
        public int FloorNumber { get; set; }
        public bool HasElevator { get; set; }
        public bool IsFurnished { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public bool HasBalcony { get; set; }
        public bool HasParking { get; set; }

        public Apartment(string id, double area, decimal pricePerSquareMeter, string location,
                        int floorNumber, bool hasElevator, bool isFurnished, int numberOfRooms = 2, 
                        int numberOfBathrooms = 1, bool hasBalcony = false, bool hasParking = false)
            : base(id, area, pricePerSquareMeter, location)
        {
            FloorNumber = floorNumber;
            HasElevator = hasElevator;
            IsFurnished = isFurnished;
            NumberOfRooms = numberOfRooms;
            NumberOfBathrooms = numberOfBathrooms;
            HasBalcony = hasBalcony;
            HasParking = hasParking;
        }

        public override string GetEstateType()
        {
            return "Apartment";
        }

        public override string GetDetails()
        {
            var details = base.GetDetails();
            details += $", Floor: {FloorNumber}, Rooms: {NumberOfRooms}, Bathrooms: {NumberOfBathrooms}";
            
            var features = new List<string>();
            if (HasElevator) features.Add("Elevator");
            if (IsFurnished) features.Add("Furnished");
            if (HasBalcony) features.Add("Balcony");
            if (HasParking) features.Add("Parking");
            
            if (features.Count > 0)
            {
                details += $", Features: {string.Join(", ", features)}";
            }
            
            return details;
        }
    }
}
