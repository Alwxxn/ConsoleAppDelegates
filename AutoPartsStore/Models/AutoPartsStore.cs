using System.Linq;

namespace AutoPartsStore.Models
{
    public class AutoPartsStoreSystem
    {
        public string StoreName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public List<AutoPart> AutoParts { get; set; } = new List<AutoPart>();
        public List<ManufacturingCompany> ManufacturingCompanies { get; set; } = new List<ManufacturingCompany>();

        public AutoPartsStoreSystem(string storeName, string location)
        {
            StoreName = storeName;
            Location = location;
        }

        public void AddAutoPart(AutoPart autoPart)
        {
            AutoParts.Add(autoPart);
            if (!ManufacturingCompanies.Contains(autoPart.ManufacturingCompany))
            {
                ManufacturingCompanies.Add(autoPart.ManufacturingCompany);
                autoPart.ManufacturingCompany.AddManufacturedPart(autoPart);
            }
        }

        public void AddManufacturingCompany(ManufacturingCompany company)
        {
            if (!ManufacturingCompanies.Contains(company))
            {
                ManufacturingCompanies.Add(company);
            }
        }

        public List<AutoPart> GetPartsByCategory(string category)
        {
            return AutoParts.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<AutoPart> GetPartsByManufacturer(string manufacturerName)
        {
            return AutoParts.Where(p => p.ManufacturingCompany.Name.Equals(manufacturerName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<AutoPart> GetCompatibleParts(CarModel carModel)
        {
            return AutoParts.Where(p => p.IsCompatibleWith(carModel)).ToList();
        }

        public List<AutoPart> GetPartsInStock()
        {
            return AutoParts.Where(p => p.IsInStock()).ToList();
        }

        public List<AutoPart> GetPartsOutOfStock()
        {
            return AutoParts.Where(p => !p.IsInStock()).ToList();
        }

        public List<AutoPart> SearchParts(string searchTerm)
        {
            return AutoParts.Where(p => 
                p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Code.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        public decimal GetTotalInventoryValue()
        {
            return AutoParts.Sum(p => p.PurchasePrice * p.StockQuantity);
        }

        public decimal GetTotalPotentialSalesValue()
        {
            return AutoParts.Sum(p => p.SalePrice * p.StockQuantity);
        }

        public void DisplayAllParts()
        {
            Console.WriteLine($"\n=== {StoreName} - All Auto Parts ===");
            if (AutoParts.Count == 0)
            {
                Console.WriteLine("No parts available.");
                return;
            }

            foreach (var part in AutoParts)
            {
                Console.WriteLine(part.ToString());
                Console.WriteLine("---");
            }
        }

        public void DisplayPartsByCategory(string category)
        {
            var parts = GetPartsByCategory(category);
            Console.WriteLine($"\n=== {StoreName} - {category} Parts ===");
            
            if (parts.Count == 0)
            {
                Console.WriteLine($"No {category.ToLower()} parts found.");
                return;
            }

            foreach (var part in parts)
            {
                Console.WriteLine(part.ToString());
                Console.WriteLine("---");
            }
        }

        public void DisplayManufacturingCompanies()
        {
            Console.WriteLine($"\n=== {StoreName} - Manufacturing Companies ===");
            if (ManufacturingCompanies.Count == 0)
            {
                Console.WriteLine("No manufacturing companies registered.");
                return;
            }

            foreach (var company in ManufacturingCompanies)
            {
                Console.WriteLine(company.ToString());
                Console.WriteLine($"Manufactures {company.ManufacturedParts.Count} parts");
                Console.WriteLine("---");
            }
        }

        public void DisplayInventorySummary()
        {
            Console.WriteLine($"\n=== {StoreName} - Inventory Summary ===");
            Console.WriteLine($"Total Parts: {AutoParts.Count}");
            Console.WriteLine($"Parts in Stock: {GetPartsInStock().Count}");
            Console.WriteLine($"Parts Out of Stock: {GetPartsOutOfStock().Count}");
            Console.WriteLine($"Total Inventory Value: ${GetTotalInventoryValue():F2}");
            Console.WriteLine($"Total Potential Sales Value: ${GetTotalPotentialSalesValue():F2}");
        }
    }
}
