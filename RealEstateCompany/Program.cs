using RealEstateCompany.Models;

namespace RealEstateCompany
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Real Estate Company Management System ===\n");

            // Create the real estate company
            var realEstateCompany = new RealEstateCompany(
                "Premier Real Estate Group", 
                "John Smith", 
                "RE-2024-001", 
                "555-REAL-ESTATE", 
                "123 Business District, Metro City");

            // Create employees
            var seniorAgent = new Employee("Sarah Johnson", "Senior Real Estate Agent", 8, 75000m, "sarah@premier.com");
            var juniorAgent = new Employee("Mike Davis", "Real Estate Agent", 3, 45000m, "mike@premier.com");
            var manager = new Employee("Lisa Brown", "Property Manager", 10, 85000m, "lisa@premier.com");
            var marketingSpecialist = new Employee("Tom Wilson", "Marketing Specialist", 2, 40000m, "tom@premier.com");

            realEstateCompany.AddEmployee(seniorAgent);
            realEstateCompany.AddEmployee(juniorAgent);
            realEstateCompany.AddEmployee(manager);
            realEstateCompany.AddEmployee(marketingSpecialist);

            // Create apartments
            var luxuryApartment = new Apartment(
                "APT001", 120.5, 8500m, "Downtown Business District, Metro City",
                15, true, true, 3, 2, true, true);

            var affordableApartment = new Apartment(
                "APT002", 85.0, 4200m, "Suburban Area, Metro City",
                5, false, false, 2, 1, false, false);

            var modernApartment = new Apartment(
                "APT003", 95.0, 6500m, "Arts District, Metro City",
                8, true, false, 2, 1, true, true);

            // Create houses
            var luxuryHouse = new House(
                "HSE001", 350.0, 4500m, "Beverly Hills, Metro City",
                200.0, 2, true, 4, 3, true, true, true);

            var familyHouse = new House(
                "HSE002", 280.0, 3200m, "Family Suburbs, Metro City",
                150.0, 2, false, 3, 2, true, true, false);

            var starterHouse = new House(
                "HSE003", 180.0, 2800m, "Starter Neighborhood, Metro City",
                100.0, 1, false, 2, 1, false, false, false);

            // Create shops
            var retailShop = new Shop(
                "SHP001", 80.0, 12000m, "Shopping Mall, Metro City",
                "Retail", true, true, true, true, "Commercial", true);

            var restaurant = new Shop(
                "SHP002", 150.0, 8500m, "Food District, Metro City",
                "Restaurant", true, false, true, false, "Commercial", true);

            var officeSpace = new Shop(
                "SHP003", 200.0, 9500m, "Business Center, Metro City",
                "Office", true, true, false, false, "Commercial", true);

            // Create undeveloped areas
            var residentialLand = new UndevelopedArea(
                "LND001", 500.0, 800m, "Outskirts, Metro City",
                "Residential", true, true, "Clay", true, 25.0, "Slightly Sloping");

            var commercialLand = new UndevelopedArea(
                "LND002", 1000.0, 1200m, "Industrial Zone, Metro City",
                "Commercial", true, true, "Rocky", true, 50.0, "Flat");

            var agriculturalLand = new UndevelopedArea(
                "LND003", 2000.0, 400m, "Rural Area, Metro City",
                "Agricultural", false, true, "Fertile", false, 100.0, "Flat");

            // Add estates to company
            realEstateCompany.AddEstate(luxuryApartment);
            realEstateCompany.AddEstate(affordableApartment);
            realEstateCompany.AddEstate(modernApartment);
            realEstateCompany.AddEstate(luxuryHouse);
            realEstateCompany.AddEstate(familyHouse);
            realEstateCompany.AddEstate(starterHouse);
            realEstateCompany.AddEstate(retailShop);
            realEstateCompany.AddEstate(restaurant);
            realEstateCompany.AddEstate(officeSpace);
            realEstateCompany.AddEstate(residentialLand);
            realEstateCompany.AddEstate(commercialLand);
            realEstateCompany.AddEstate(agriculturalLand);

            // Assign agents to estates
            luxuryApartment.AssignAgent(seniorAgent);
            luxuryHouse.AssignAgent(seniorAgent);
            retailShop.AssignAgent(seniorAgent);
            residentialLand.AssignAgent(seniorAgent);

            affordableApartment.AssignAgent(juniorAgent);
            familyHouse.AssignAgent(juniorAgent);
            restaurant.AssignAgent(juniorAgent);

            modernApartment.AssignAgent(manager);
            starterHouse.AssignAgent(manager);
            officeSpace.AssignAgent(manager);
            commercialLand.AssignAgent(manager);

            agriculturalLand.AssignAgent(marketingSpecialist);

            // Display company information
            realEstateCompany.DisplayCompanyInfo();

            // Display employees
            realEstateCompany.DisplayEmployees();

            // Display statistics
            realEstateCompany.DisplayStatistics();

            // Display estates by type
            realEstateCompany.DisplayEstatesByType<Apartment>();
            realEstateCompany.DisplayEstatesByType<House>();
            realEstateCompany.DisplayEstatesByType<Shop>();
            realEstateCompany.DisplayEstatesByType<UndevelopedArea>();

            // Search examples
            Console.WriteLine("\n=== Search Results ===");

            Console.WriteLine("\nEstates in 'Downtown Business District':");
            var downtownEstates = realEstateCompany.GetEstatesByLocation("Downtown Business District");
            foreach (var estate in downtownEstates)
            {
                Console.WriteLine($"  {estate}");
            }

            Console.WriteLine("\nEstates under $500,000:");
            var affordableEstates = realEstateCompany.GetEstatesByPriceRange(0, 500000m);
            foreach (var estate in affordableEstates)
            {
                Console.WriteLine($"  {estate}");
            }

            Console.WriteLine("\nEstates over 200 sqm:");
            var largeEstates = realEstateCompany.GetEstatesByArea(200, double.MaxValue);
            foreach (var estate in largeEstates)
            {
                Console.WriteLine($"  {estate}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
