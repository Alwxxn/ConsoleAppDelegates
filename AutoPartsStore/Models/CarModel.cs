namespace AutoPartsStore.Models
{
    public class CarModel
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string EngineType { get; set; } = string.Empty;
        public string BodyType { get; set; } = string.Empty; // Sedan, SUV, Hatchback, etc.

        public CarModel(string brand, string model, int year, string engineType = "", string bodyType = "")
        {
            Brand = brand;
            Model = model;
            Year = year;
            EngineType = engineType;
            BodyType = bodyType;
        }

        public string GetFullName()
        {
            return $"{Brand} {Model} ({Year})";
        }

        public override string ToString()
        {
            return GetFullName();
        }

        public override bool Equals(object? obj)
        {
            if (obj is CarModel other)
            {
                return Brand.Equals(other.Brand, StringComparison.OrdinalIgnoreCase) &&
                       Model.Equals(other.Model, StringComparison.OrdinalIgnoreCase) &&
                       Year == other.Year;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Brand.ToLower(), Model.ToLower(), Year);
        }
    }
}
