namespace AutoPartsStore.Models
{
    public class ManufacturingCompany
    {
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public List<AutoPart> ManufacturedParts { get; set; } = new List<AutoPart>();

        public ManufacturingCompany(string name, string country, string address, string phone, string fax = "")
        {
            Name = name;
            Country = country;
            Address = address;
            Phone = phone;
            Fax = fax;
        }

        public void AddManufacturedPart(AutoPart part)
        {
            ManufacturedParts.Add(part);
        }

        public override string ToString()
        {
            return $"{Name} ({Country}) - Phone: {Phone}, Fax: {Fax}\nAddress: {Address}";
        }
    }
}
