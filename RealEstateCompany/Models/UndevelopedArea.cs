namespace RealEstateCompany.Models
{
    public class UndevelopedArea : Estate
    {
        public string LandType { get; set; } = string.Empty; // Residential, Commercial, Agricultural, etc.
        public bool HasUtilities { get; set; } // Water, electricity, gas connections
        public bool HasRoadAccess { get; set; }
        public string SoilType { get; set; } = string.Empty;
        public bool IsZonedForDevelopment { get; set; }
        public double FrontageLength { get; set; } // meters
        public string Topography { get; set; } = string.Empty; // Flat, Sloping, etc.

        public UndevelopedArea(string id, double area, decimal pricePerSquareMeter, string location,
                              string landType, bool hasUtilities = false, bool hasRoadAccess = true,
                              string soilType = "", bool isZonedForDevelopment = false, double frontageLength = 0,
                              string topography = "Flat")
            : base(id, area, pricePerSquareMeter, location)
        {
            LandType = landType;
            HasUtilities = hasUtilities;
            HasRoadAccess = hasRoadAccess;
            SoilType = soilType;
            IsZonedForDevelopment = isZonedForDevelopment;
            FrontageLength = frontageLength;
            Topography = topography;
        }

        public override string GetEstateType()
        {
            return "Undeveloped Area";
        }

        public override string GetDetails()
        {
            var details = base.GetDetails();
            details += $", Land Type: {LandType}, Topography: {Topography}";
            
            var features = new List<string>();
            if (HasUtilities) features.Add("Utilities Available");
            if (HasRoadAccess) features.Add("Road Access");
            if (IsZonedForDevelopment) features.Add("Zoned for Development");
            if (FrontageLength > 0) features.Add($"Frontage: {FrontageLength}m");
            if (!string.IsNullOrEmpty(SoilType)) features.Add($"Soil: {SoilType}");
            
            if (features.Count > 0)
            {
                details += $", Features: {string.Join(", ", features)}";
            }
            
            return details;
        }
    }
}
