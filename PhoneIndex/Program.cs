using PhoneIndex.Models;
using PhoneIndex.Services;

namespace PhoneIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            var phoneIndexService = new PhoneIndexService();
            
            Console.WriteLine("=== Phone Index System ===\n");

            // Add sample contacts
            phoneIndexService.AddContact(new Contact("Alice Johnson", "555-0101", "alice@email.com", "123 Main St"));
            phoneIndexService.AddContact(new Contact("Charlie Brown", "555-0102", "charlie@email.com", "456 Oak Ave"));
            phoneIndexService.AddContact(new Contact("Bob Smith", "555-0103", "bob@email.com", "789 Pine Rd"));
            phoneIndexService.AddContact(new Contact("Diana Wilson", "555-0104", "diana@email.com", "321 Elm St"));
            phoneIndexService.AddContact(new Contact("Eve Davis", "555-0105", "eve@email.com", "654 Maple Dr"));

            // Display both indexes
            phoneIndexService.DisplaySortedList();
            phoneIndexService.DisplayHashTable();

            // Test lookups
            Console.WriteLine("\n=== Testing Lookups ===");
            
            var contact1 = phoneIndexService.GetContactSortedList("Bob Smith");
            Console.WriteLine($"SortedList lookup for 'Bob Smith': {(contact1 != null ? contact1.ToString() : "Not found")}");

            var contact2 = phoneIndexService.GetContactHashTable("Bob Smith");
            Console.WriteLine($"HashTable lookup for 'Bob Smith': {(contact2 != null ? contact2.ToString() : "Not found")}");

            var contact3 = phoneIndexService.GetContactSortedList("Non Existent");
            Console.WriteLine($"SortedList lookup for 'Non Existent': {(contact3 != null ? contact3.ToString() : "Not found")}");

            // Show performance comparison
            phoneIndexService.ComparePerformance();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
