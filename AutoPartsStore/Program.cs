using AutoPartsStore.Models;

namespace AutoPartsStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Auto Parts Store Management System ===\n");

            // Create the store system
            var autoPartsStore = new AutoPartsStoreSystem("AutoMax Parts", "123 Main Street, Auto City");

            // Create manufacturing companies
            var toyotaMotor = new ManufacturingCompany(
                "Toyota Motor Corporation", "Japan", 
                "1 Toyota-cho, Toyota City, Aichi Prefecture", 
                "+81-565-28-2121", "+81-565-23-5600");

            var bosch = new ManufacturingCompany(
                "Robert Bosch GmbH", "Germany", 
                "Robert-Bosch-Platz 1, 70839 Gerlingen-Schillerhöhe", 
                "+49-711-811-0", "+49-711-811-25777");

            var delphi = new ManufacturingCompany(
                "Delphi Technologies", "United Kingdom", 
                "1 Angel Court, London EC2R 7HJ", 
                "+44-20-7379-5000", "+44-20-7379-5001");

            autoPartsStore.AddManufacturingCompany(toyotaMotor);
            autoPartsStore.AddManufacturingCompany(bosch);
            autoPartsStore.AddManufacturingCompany(delphi);

            // Create car models
            var toyotaCamry = new CarModel("Toyota", "Camry", 2020, "V6 3.5L", "Sedan");
            var hondaCivic = new CarModel("Honda", "Civic", 2021, "4-Cylinder 1.5L", "Sedan");
            var fordF150 = new CarModel("Ford", "F-150", 2022, "V8 5.0L", "Pickup Truck");
            var bmwX5 = new CarModel("BMW", "X5", 2023, "V6 3.0L", "SUV");
            var mercedesC320 = new CarModel("Mercedes", "C320", 2008, "V6 3.2L", "Sedan");

            // Create auto parts
            var brakePad = new AutoPart("BP001", "Premium Brake Pads", "Brakes", 45.00m, 75.00m, bosch);
            brakePad.AddCompatibleCarModels(new List<CarModel> { toyotaCamry, hondaCivic, bmwX5 });
            brakePad.StockQuantity = 50;
            brakePad.Description = "High-performance ceramic brake pads for improved stopping power";

            var airFilter = new AutoPart("AF002", "Engine Air Filter", "Engine", 25.00m, 45.00m, toyotaMotor);
            airFilter.AddCompatibleCarModels(new List<CarModel> { toyotaCamry, hondaCivic });
            airFilter.StockQuantity = 100;
            airFilter.Description = "OEM quality air filter for optimal engine performance";

            var oilFilter = new AutoPart("OF003", "Oil Filter", "Engine", 15.00m, 28.00m, bosch);
            oilFilter.AddCompatibleCarModels(new List<CarModel> { toyotaCamry, hondaCivic, fordF150, bmwX5 });
            oilFilter.StockQuantity = 75;
            oilFilter.Description = "High-efficiency oil filter with extended service life";

            var suspensionStrut = new AutoPart("SS004", "Front Suspension Strut", "Suspension", 180.00m, 320.00m, delphi);
            suspensionStrut.AddCompatibleCarModels(new List<CarModel> { fordF150, bmwX5 });
            suspensionStrut.StockQuantity = 20;
            suspensionStrut.Description = "Heavy-duty suspension strut for improved ride quality";

            var sparkPlug = new AutoPart("SP005", "Iridium Spark Plug", "Engine", 12.00m, 22.00m, bosch);
            sparkPlug.AddCompatibleCarModels(new List<CarModel> { mercedesC320, bmwX5, toyotaCamry });
            sparkPlug.StockQuantity = 0; // Out of stock
            sparkPlug.Description = "Iridium spark plug for enhanced fuel efficiency";

            var tireSet = new AutoPart("TS006", "All-Season Tire Set", "Tires and Wheels", 400.00m, 650.00m, bosch);
            tireSet.AddCompatibleCarModels(new List<CarModel> { toyotaCamry, hondaCivic, fordF150 });
            tireSet.StockQuantity = 15;
            tireSet.Description = "Set of 4 all-season tires with 60,000 mile warranty";

            var ledHeadlight = new AutoPart("LH007", "LED Headlight Assembly", "Accessories", 150.00m, 250.00m, delphi);
            ledHeadlight.AddCompatibleCarModels(new List<CarModel> { bmwX5, fordF150 });
            ledHeadlight.StockQuantity = 8;
            ledHeadlight.Description = "Bright LED headlight with automatic leveling";

            // Add parts to store
            autoPartsStore.AddAutoPart(brakePad);
            autoPartsStore.AddAutoPart(airFilter);
            autoPartsStore.AddAutoPart(oilFilter);
            autoPartsStore.AddAutoPart(suspensionStrut);
            autoPartsStore.AddAutoPart(sparkPlug);
            autoPartsStore.AddAutoPart(tireSet);
            autoPartsStore.AddAutoPart(ledHeadlight);

            // Display store information
            Console.WriteLine($"Welcome to {autoPartsStore.StoreName}");
            Console.WriteLine($"Location: {autoPartsStore.Location}\n");

            // Display inventory summary
            autoPartsStore.DisplayInventorySummary();

            // Display manufacturing companies
            autoPartsStore.DisplayManufacturingCompanies();

            // Display parts by category
            autoPartsStore.DisplayPartsByCategory("Engine");
            autoPartsStore.DisplayPartsByCategory("Brakes");
            autoPartsStore.DisplayPartsByCategory("Suspension");

            // Search for parts
            Console.WriteLine("\n=== Searching for 'filter' parts ===");
            var filterParts = autoPartsStore.SearchParts("filter");
            foreach (var part in filterParts)
            {
                Console.WriteLine($"Found: {part.Name} - {part.Code}");
            }

            // Find compatible parts for a specific car
            Console.WriteLine($"\n=== Parts compatible with {toyotaCamry.GetFullName()} ===");
            var compatibleParts = autoPartsStore.GetCompatibleParts(toyotaCamry);
            foreach (var part in compatibleParts)
            {
                Console.WriteLine($"✓ {part.Name} - ${part.SalePrice:F2} (Stock: {part.StockQuantity})");
            }

            // Display out of stock items
            Console.WriteLine("\n=== Out of Stock Items ===");
            var outOfStock = autoPartsStore.GetPartsOutOfStock();
            if (outOfStock.Count == 0)
            {
                Console.WriteLine("All items are in stock!");
            }
            else
            {
                foreach (var part in outOfStock)
                {
                    Console.WriteLine($"⚠ {part.Name} - {part.Code}");
                }
            }

            // Display profit margins
            Console.WriteLine("\n=== Parts with Highest Profit Margins ===");
            var sortedByProfit = autoPartsStore.AutoParts
                .OrderByDescending(p => p.GetProfitMargin())
                .Take(3);
            
            foreach (var part in sortedByProfit)
            {
                Console.WriteLine($"{part.Name}: ${part.GetProfitMargin():F2} profit ({part.GetProfitPercentage():F1}%)");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
