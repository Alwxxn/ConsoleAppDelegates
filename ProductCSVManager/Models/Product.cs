namespace ProductCSVManager.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } = true;

        public Product(int productId, string productName, decimal productPrice, int quantity, string category = "", string description = "")
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = quantity;
            Category = category;
            Description = description;
            CreatedDate = DateTime.Now;
        }

        public decimal GetTotalValue()
        {
            return ProductPrice * Quantity;
        }

        public string ToCSVLine()
        {
            return $"{ProductId},{ProductName},{ProductPrice:F2},{Quantity}";
        }

        public static Product FromCSVLine(string csvLine)
        {
            var parts = csvLine.Split(',');
            if (parts.Length >= 4)
            {
                return new Product(
                    int.Parse(parts[0]),
                    parts[1],
                    decimal.Parse(parts[2]),
                    int.Parse(parts[3])
                );
            }
            throw new ArgumentException("Invalid CSV line format");
        }

        public override string ToString()
        {
            return $"ID: {ProductId}, Name: {ProductName}, Price: ${ProductPrice:F2}, Qty: {Quantity}, Total: ${GetTotalValue():F2}";
        }
    }
}
