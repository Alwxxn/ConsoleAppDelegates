namespace AutoPartsStore.Models
{
    public class AutoPart
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // suspension, tires, engine, accessories, etc.
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public List<CarModel> CompatibleCarModels { get; set; } = new List<CarModel>();
        public ManufacturingCompany ManufacturingCompany { get; set; }
        public int StockQuantity { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;

        public AutoPart(string code, string name, string category, decimal purchasePrice, 
                       decimal salePrice, ManufacturingCompany manufacturingCompany)
        {
            Code = code;
            Name = name;
            Category = category;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            ManufacturingCompany = manufacturingCompany;
        }

        public void AddCompatibleCarModel(CarModel carModel)
        {
            if (!CompatibleCarModels.Contains(carModel))
            {
                CompatibleCarModels.Add(carModel);
            }
        }

        public void AddCompatibleCarModels(List<CarModel> carModels)
        {
            foreach (var carModel in carModels)
            {
                AddCompatibleCarModel(carModel);
            }
        }

        public bool IsCompatibleWith(CarModel carModel)
        {
            return CompatibleCarModels.Contains(carModel);
        }

        public decimal GetProfitMargin()
        {
            return SalePrice - PurchasePrice;
        }

        public decimal GetProfitPercentage()
        {
            if (PurchasePrice == 0) return 0;
            return ((SalePrice - PurchasePrice) / PurchasePrice) * 100;
        }

        public bool IsInStock()
        {
            return StockQuantity > 0;
        }

        public void UpdateStock(int quantity)
        {
            StockQuantity = Math.Max(0, quantity);
        }

        public override string ToString()
        {
            return $"{Code} - {Name} ({Category})\n" +
                   $"Manufacturer: {ManufacturingCompany.Name}\n" +
                   $"Purchase: ${PurchasePrice:F2}, Sale: ${SalePrice:F2}, Stock: {StockQuantity}\n" +
                   $"Compatible with {CompatibleCarModels.Count} car models";
        }
    }
}
