using System.Linq;

namespace RealEstateCompany.Models
{
    public class RealEstateCompany
    {
        public string CompanyName { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string TaxId { get; set; } = string.Empty;
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Estate> EstatesForSale { get; set; } = new List<Estate>();
        public string ContactInfo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime EstablishedDate { get; set; }

        public RealEstateCompany(string companyName, string owner, string taxId, string contactInfo = "", string address = "")
        {
            CompanyName = companyName;
            Owner = owner;
            TaxId = taxId;
            ContactInfo = contactInfo;
            Address = address;
            EstablishedDate = DateTime.Now;
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public void AddEstate(Estate estate)
        {
            EstatesForSale.Add(estate);
        }

        public List<Estate> GetEstatesByType<T>() where T : Estate
        {
            return EstatesForSale.OfType<T>().ToList();
        }

        public List<Estate> GetAvailableEstates()
        {
            return EstatesForSale.Where(e => e.IsAvailable).ToList();
        }

        public List<Estate> GetEstatesByLocation(string location)
        {
            return EstatesForSale.Where(e => e.Location.Contains(location, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Estate> GetEstatesByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return EstatesForSale.Where(e => e.TotalPrice >= minPrice && e.TotalPrice <= maxPrice).ToList();
        }

        public List<Estate> GetEstatesByArea(double minArea, double maxArea)
        {
            return EstatesForSale.Where(e => e.Area >= minArea && e.Area <= maxArea).ToList();
        }

        public decimal GetTotalValueOfEstates()
        {
            return EstatesForSale.Sum(e => e.TotalPrice);
        }

        public decimal GetAverageEstatePrice()
        {
            return EstatesForSale.Count > 0 ? EstatesForSale.Average(e => e.TotalPrice) : 0;
        }

        public Employee? GetTopPerformingAgent()
        {
            return Employees.OrderByDescending(e => e.AssignedEstates.Count).FirstOrDefault();
        }

        public void DisplayCompanyInfo()
        {
            Console.WriteLine($"=== {CompanyName} ===");
            Console.WriteLine($"Owner: {Owner}");
            Console.WriteLine($"Tax ID: {TaxId}");
            Console.WriteLine($"Address: {Address}");
            Console.WriteLine($"Contact: {ContactInfo}");
            Console.WriteLine($"Established: {EstablishedDate:yyyy-MM-dd}");
            Console.WriteLine($"Employees: {Employees.Count}");
            Console.WriteLine($"Estates for Sale: {EstatesForSale.Count}");
            Console.WriteLine($"Total Value: ${GetTotalValueOfEstates():F2}");
        }

        public void DisplayAllEstates()
        {
            Console.WriteLine($"\n=== {CompanyName} - All Estates for Sale ===");
            if (EstatesForSale.Count == 0)
            {
                Console.WriteLine("No estates available for sale.");
                return;
            }

            foreach (var estate in EstatesForSale)
            {
                Console.WriteLine(estate.ToString());
                if (estate.AssignedAgent != null)
                {
                    Console.WriteLine($"  Agent: {estate.AssignedAgent.Name}");
                }
                Console.WriteLine("---");
            }
        }

        public void DisplayEstatesByType<T>() where T : Estate
        {
            var estates = GetEstatesByType<T>();
            var typeName = typeof(T).Name;
            
            Console.WriteLine($"\n=== {typeName}s ===");
            if (estates.Count == 0)
            {
                Console.WriteLine($"No {typeName.ToLower()}s available.");
                return;
            }

            foreach (var estate in estates)
            {
                Console.WriteLine(estate.ToString());
                Console.WriteLine("---");
            }
        }

        public void DisplayEmployees()
        {
            Console.WriteLine($"\n=== {CompanyName} - Employees ===");
            if (Employees.Count == 0)
            {
                Console.WriteLine("No employees registered.");
                return;
            }

            foreach (var employee in Employees)
            {
                Console.WriteLine($"{employee} - Assigned Estates: {employee.AssignedEstates.Count}");
            }
        }

        public void DisplayStatistics()
        {
            Console.WriteLine($"\n=== {CompanyName} - Statistics ===");
            Console.WriteLine($"Total Estates: {EstatesForSale.Count}");
            Console.WriteLine($"Apartments: {GetEstatesByType<Apartment>().Count}");
            Console.WriteLine($"Houses: {GetEstatesByType<House>().Count}");
            Console.WriteLine($"Shops: {GetEstatesByType<Shop>().Count}");
            Console.WriteLine($"Undeveloped Areas: {GetEstatesByType<UndevelopedArea>().Count}");
            Console.WriteLine($"Available Estates: {GetAvailableEstates().Count}");
            Console.WriteLine($"Average Price: ${GetAverageEstatePrice():F2}");
            
            var topAgent = GetTopPerformingAgent();
            if (topAgent != null)
            {
                Console.WriteLine($"Top Agent: {topAgent.Name} ({topAgent.AssignedEstates.Count} estates)");
            }
        }
    }
}
