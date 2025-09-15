using ProductCSVManager.Models;
using ProductCSVManager.Services;

namespace ProductCSVManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Product CSV Manager ===\n");

            var productService = new ProductCSVService();
            
            Console.WriteLine("This application manages 10 products and converts data to CSV format.");
            Console.WriteLine($"CSV file location: {Path.Combine(Directory.GetCurrentDirectory(), "products.csv")}\n");

            // Menu-driven interface
            while (true)
            {
                Console.WriteLine("\n=== Product Management Menu ===");
                Console.WriteLine("1. Create 10 sample products");
                Console.WriteLine("2. Add new product");
                Console.WriteLine("3. Display all products");
                Console.WriteLine("4. Search products");
                Console.WriteLine("5. Display products by category");
                Console.WriteLine("6. Display low stock products");
                Console.WriteLine("7. Save products to CSV file");
                Console.WriteLine("8. Load products from CSV file");
                Console.WriteLine("9. Display product statistics");
                Console.WriteLine("10. Remove product");
                Console.WriteLine("11. Exit");
                Console.Write("Enter your choice (1-11): ");

                string? choice = Console.ReadLine();

                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                try
                {
                    switch (choice.Trim())
                    {
                        case "1":
                            productService.CreateSampleProducts();
                            break;

                        case "2":
                            AddNewProduct(productService);
                            break;

                        case "3":
                            productService.DisplayAllProducts();
                            break;

                        case "4":
                            SearchProducts(productService);
                            break;

                        case "5":
                            DisplayProductsByCategory(productService);
                            break;

                        case "6":
                            DisplayLowStockProducts(productService);
                            break;

                        case "7":
                            productService.SaveToCSV();
                            break;

                        case "8":
                            productService.LoadProductsFromCSV();
                            break;

                        case "9":
                            productService.DisplayProductStatistics();
                            break;

                        case "10":
                            RemoveProduct(productService);
                            break;

                        case "11":
                            Console.WriteLine("Thank you for using Product CSV Manager!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1-11.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void AddNewProduct(ProductCSVService productService)
        {
            Console.WriteLine("\n=== Add New Product ===");

            Console.Write("Enter Product ID: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid Product ID.");
                return;
            }

            Console.Write("Enter Product Name: ");
            string? productName = Console.ReadLine();
            if (string.IsNullOrEmpty(productName))
            {
                Console.WriteLine("Product name cannot be empty.");
                return;
            }

            Console.Write("Enter Product Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal productPrice))
            {
                Console.WriteLine("Invalid Product Price.");
                return;
            }

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid Quantity.");
                return;
            }

            Console.Write("Enter Category (optional): ");
            string? category = Console.ReadLine();

            Console.Write("Enter Description (optional): ");
            string? description = Console.ReadLine();

            productService.AddProduct(productId, productName, productPrice, quantity, category ?? "", description ?? "");
        }

        static void SearchProducts(ProductCSVService productService)
        {
            Console.Write("\nEnter search term: ");
            string? searchTerm = Console.ReadLine();
            
            if (string.IsNullOrEmpty(searchTerm))
            {
                Console.WriteLine("Search term cannot be empty.");
                return;
            }

            productService.SearchProducts(searchTerm);
        }

        static void DisplayProductsByCategory(ProductCSVService productService)
        {
            Console.Write("\nEnter category: ");
            string? category = Console.ReadLine();
            
            if (string.IsNullOrEmpty(category))
            {
                Console.WriteLine("Category cannot be empty.");
                return;
            }

            productService.DisplayProductsByCategory(category);
        }

        static void DisplayLowStockProducts(ProductCSVService productService)
        {
            Console.Write("\nEnter low stock threshold (default 10): ");
            string? input = Console.ReadLine();
            
            int threshold = 10;
            if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int customThreshold))
            {
                threshold = customThreshold;
            }

            productService.DisplayLowStockProducts(threshold);
        }

        static void RemoveProduct(ProductCSVService productService)
        {
            Console.Write("\nEnter Product ID to remove: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                productService.RemoveProduct(productId);
            }
            else
            {
                Console.WriteLine("Invalid Product ID.");
            }
        }
    }
}
