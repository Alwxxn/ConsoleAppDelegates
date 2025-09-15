namespace RealEstateCompany.Models
{
    public class House : Estate
    {
        public double UndevelopedArea { get; set; } // yard area in square meters
        public double DevelopedArea { get; set; } // house area in square meters
        public int NumberOfFloors { get; set; }
        public bool IsFurnished { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public bool HasGarage { get; set; }
        public bool HasGarden { get; set; }
        public bool HasSwimmingPool { get; set; }

        public House(string id, double totalArea, decimal pricePerSquareMeter, string location,
                    double undevelopedArea, int numberOfFloors, bool isFurnished, int numberOfBedrooms = 3,
                    int numberOfBathrooms = 2, bool hasGarage = false, bool hasGarden = false, bool hasSwimmingPool = false)
            : base(id, totalArea, pricePerSquareMeter, location)
        {
            UndevelopedArea = undevelopedArea;
            DevelopedArea = totalArea - undevelopedArea;
            NumberOfFloors = numberOfFloors;
            IsFurnished = isFurnished;
            NumberOfBedrooms = numberOfBedrooms;
            NumberOfBathrooms = numberOfBathrooms;
            HasGarage = hasGarage;
            HasGarden = hasGarden;
            HasSwimmingPool = hasSwimmingPool;
        }

        public override string GetEstateType()
        {
            return "House";
        }

        public override string GetDetails()
        {
            var details = base.GetDetails();
            details += $", Floors: {NumberOfFloors}, Bedrooms: {NumberOfBedrooms}, Bathrooms: {NumberOfBathrooms}";
            details += $", House Area: {DevelopedArea:F1} sqm, Yard Area: {UndevelopedArea:F1} sqm";
            
            var features = new List<string>();
            if (IsFurnished) features.Add("Furnished");
            if (HasGarage) features.Add("Garage");
            if (HasGarden) features.Add("Garden");
            if (HasSwimmingPool) features.Add("Swimming Pool");
            
            if (features.Count > 0)
            {
                details += $", Features: {string.Join(", ", features)}";
            }
            
            return details;
        }
    }
}
