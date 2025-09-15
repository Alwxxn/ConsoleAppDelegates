using ProductCSVManager.Models;
using System.Globalization;

namespace ProductCSVManager.Services
{
    public class ProductCSVService
    {
        private readonly string _csvFilePath;
        private readonly List<Product> _products;

        public ProductCSVService(string csvFilePath = "products.csv")
        {
            _csvFilePath = csvFilePath;
            _products = new List<Product>();
            LoadProductsFromCSV();
        }

        public void AddProduct(Product product)
        {
            if (_products.Any(p => p.ProductId == product.ProductId))
            {
                Console.WriteLine($"Product with ID {product.ProductId} already exists.");
                return;
            }

            _products.Add(product);
            Console.WriteLine($"Product added: {product.ProductName}");
        }

        public void AddProduct(int productId, string productName, decimal productPrice, int quantity, string category = "", string description = "")
        {
            var product = new Product(productId, productName, productPrice, quantity, category, description);
            AddProduct(product);
        }

        public void RemoveProduct(int productId)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                _products.Remove(product);
                Console.WriteLine($"Product removed: {product.ProductName}");
            }
            else
            {
                Console.WriteLine($"Product with ID {productId} not found.");
            }
        }

        public Product? GetProduct(int productId)
        {
            return _products.FirstOrDefault(p => p.ProductId == productId);
        }

        public List<Product> GetAllProducts()
        {
            return new List<Product>(_products);
        }

        public List<Product> GetProductsByCategory(string category)
        {
            return _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _products.Where(p => p.ProductPrice >= minPrice && p.ProductPrice <= maxPrice).ToList();
        }

        public List<Product> GetLowStockProducts(int threshold = 10)
        {
            return _products.Where(p => p.Quantity <= threshold).ToList();
        }

        public decimal GetTotalInventoryValue()
        {
            return _products.Sum(p => p.GetTotalValue());
        }

        public void DisplayAllProducts()
        {
            Console.WriteLine("\n=== All Products ===");
            if (_products.Count == 0)
            {
                Console.WriteLine("No products available.");
                return;
            }

            Console.WriteLine($"{"ID",-5} {"Name",-20} {"Price",-10} {"Qty",-5} {"Total Value",-12} {"Category",-15}");
            Console.WriteLine(new string('-', 80));

            foreach (var product in _products.OrderBy(p => p.ProductId))
            {
                Console.WriteLine($"{product.ProductId,-5} {product.ProductName,-20} ${product.ProductPrice,-9:F2} {product.Quantity,-5} ${product.GetTotalValue(),-11:F2} {product.Category,-15}");
            }

            Console.WriteLine($"\nTotal Products: {_products.Count}");
            Console.WriteLine($"Total Inventory Value: ${GetTotalInventoryValue():F2}");
        }

        public void DisplayProductsByCategory(string category)
        {
            var products = GetProductsByCategory(category);
            Console.WriteLine($"\n=== Products in Category: {category} ===");
            
            if (products.Count == 0)
            {
                Console.WriteLine($"No products found in category '{category}'.");
                return;
            }

            foreach (var product in products)
            {
                Console.WriteLine(product.ToString());
            }
        }

        public void DisplayLowStockProducts(int threshold = 10)
        {
            var lowStockProducts = GetLowStockProducts(threshold);
            Console.WriteLine($"\n=== Low Stock Products (≤ {threshold} units) ===");
            
            if (lowStockProducts.Count == 0)
            {
                Console.WriteLine("No low stock products found.");
                return;
            }

            foreach (var product in lowStockProducts)
            {
                Console.WriteLine(product.ToString());
            }
        }

        public void SaveToCSV()
        {
            try
            {
                using (var writer = new StreamWriter(_csvFilePath))
                {
                    // Write CSV header
                    writer.WriteLine("ProductId,ProductName,ProductPrice,Quantity");

                    // Write product data
                    foreach (var product in _products)
                    {
                        writer.WriteLine(product.ToCSVLine());
                    }
                }

                Console.WriteLine($"Products saved to CSV file: {_csvFilePath}");
                Console.WriteLine($"Total products saved: {_products.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to CSV: {ex.Message}");
                throw;
            }
        }

        public void LoadProductsFromCSV()
        {
            try
            {
                if (!File.Exists(_csvFilePath))
                {
                    Console.WriteLine($"CSV file not found: {_csvFilePath}");
                    return;
                }

                _products.Clear();
                var lines = File.ReadAllLines(_csvFilePath);

                // Skip header line
                for (int i = 1; i < lines.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(lines[i]))
                    {
                        try
                        {
                            var product = Product.FromCSVLine(lines[i]);
                            _products.Add(product);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error parsing line {i + 1}: {ex.Message}");
                        }
                    }
                }

                Console.WriteLine($"Loaded {_products.Count} products from CSV file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from CSV: {ex.Message}");
            }
        }

        public void CreateSampleProducts()
        {
            Console.WriteLine("Creating 10 sample products...");

            var sampleProducts = new List<Product>
            {
                new Product(100, "Apple iPhone 15", 999.99m, 50, "Electronics", "Latest iPhone model"),
                new Product(101, "Samsung Galaxy S24", 899.99m, 30, "Electronics", "Android smartphone"),
                new Product(102, "Dell Laptop XPS 13", 1299.99m, 25, "Electronics", "High-performance laptop"),
                new Product(103, "Nike Air Max", 129.99m, 100, "Clothing", "Running shoes"),
                new Product(104, "Adidas T-Shirt", 29.99m, 200, "Clothing", "Cotton t-shirt"),
                new Product(105, "Coffee Maker", 79.99m, 15, "Appliances", "Automatic coffee maker"),
                new Product(106, "Wireless Headphones", 199.99m, 40, "Electronics", "Noise-cancelling headphones"),
                new Product(107, "Book: Programming C#", 49.99m, 75, "Books", "Learn C# programming"),
                new Product(108, "Gaming Mouse", 59.99m, 60, "Electronics", "RGB gaming mouse"),
                new Product(109, "Office Chair", 299.99m, 20, "Furniture", "Ergonomic office chair")
            };

            foreach (var product in sampleProducts)
            {
                AddProduct(product);
            }

            Console.WriteLine("Sample products created successfully!");
        }

        public void SearchProducts(string searchTerm)
        {
            var searchResults = _products.Where(p => 
                p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            Console.WriteLine($"\n=== Search Results for '{searchTerm}' ===");
            
            if (searchResults.Count == 0)
            {
                Console.WriteLine("No products found matching the search term.");
                return;
            }

            foreach (var product in searchResults)
            {
                Console.WriteLine(product.ToString());
            }
        }

        public void DisplayProductStatistics()
        {
            Console.WriteLine("\n=== Product Statistics ===");
            Console.WriteLine($"Total Products: {_products.Count}");
            Console.WriteLine($"Total Inventory Value: ${GetTotalInventoryValue():F2}");
            Console.WriteLine($"Average Product Price: ${_products.Count > 0 ? _products.Average(p => p.ProductPrice):F2:F2}");
            Console.WriteLine($"Total Quantity: {_products.Sum(p => p.Quantity)}");

            var categoryGroups = _products.GroupBy(p => p.Category);
            Console.WriteLine("\nProducts by Category:");
            foreach (var group in categoryGroups.OrderByDescending(g => g.Count()))
            {
                Console.WriteLine($"  {group.Key}: {group.Count()} products, Total Value: ${group.Sum(p => p.GetTotalValue()):F2}");
            }

            var lowStockCount = GetLowStockProducts().Count;
            Console.WriteLine($"\nLow Stock Products: {lowStockCount}");
        }
    }
}
