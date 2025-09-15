namespace RealEstateCompany.Models
{
    public abstract class Estate
    {
        public string Id { get; set; } = string.Empty;
        public double Area { get; set; } // in square meters
        public decimal PricePerSquareMeter { get; set; }
        public string Location { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTime ListingDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public Employee? AssignedAgent { get; set; }

        protected Estate(string id, double area, decimal pricePerSquareMeter, string location)
        {
            Id = id;
            Area = area;
            PricePerSquareMeter = pricePerSquareMeter;
            Location = location;
            TotalPrice = (decimal)area * pricePerSquareMeter;
            ListingDate = DateTime.Now;
        }

        public void AssignAgent(Employee agent)
        {
            AssignedAgent = agent;
            agent.AssignEstate(this);
        }

        public virtual string GetEstateType()
        {
            return "Estate";
        }

        public virtual string GetDetails()
        {
            return $"ID: {Id}, Area: {Area:F1} sqm, Price: ${TotalPrice:F2}, Location: {Location}";
        }

        public override string ToString()
        {
            return $"{GetEstateType()} - {GetDetails()}";
        }
    }
}
