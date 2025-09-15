using System.Collections;
using PhoneIndex.Models;

namespace PhoneIndex.Services
{
    public class PhoneIndexService
    {
        private readonly SortedList<string, Contact> _sortedListIndex;
        private readonly Hashtable _hashTableIndex;

        public PhoneIndexService()
        {
            _sortedListIndex = new SortedList<string, Contact>();
            _hashTableIndex = new Hashtable();
        }

        public void AddContact(Contact contact)
        {
            _sortedListIndex.Add(contact.Name.ToLower(), contact);
            _hashTableIndex.Add(contact.Name.ToLower(), contact);
            Console.WriteLine($"Contact {contact.Name} added to both indexes.");
        }

        public Contact? GetContactSortedList(string name)
        {
            string key = name.ToLower();
            return _sortedListIndex.ContainsKey(key) ? _sortedListIndex[key] : null;
        }

        public Contact? GetContactHashTable(string name)
        {
            string key = name.ToLower();
            return _hashTableIndex.ContainsKey(key) ? (Contact)_hashTableIndex[key]! : null;
        }

        public void DisplaySortedList()
        {
            Console.WriteLine("\n=== SortedList Index (Sorted by Name) ===");
            if (_sortedListIndex.Count == 0)
            {
                Console.WriteLine("No contacts in SortedList.");
                return;
            }

            foreach (var kvp in _sortedListIndex)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }

        public void DisplayHashTable()
        {
            Console.WriteLine("\n=== HashTable Index (Unsorted) ===");
            if (_hashTableIndex.Count == 0)
            {
                Console.WriteLine("No contacts in HashTable.");
                return;
            }

            foreach (DictionaryEntry entry in _hashTableIndex)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }

        public void ComparePerformance()
        {
            Console.WriteLine("\n=== Performance Comparison ===");
            Console.WriteLine($"SortedList Count: {_sortedListIndex.Count}");
            Console.WriteLine($"HashTable Count: {_hashTableIndex.Count}");

            Console.WriteLine("\nKey Differences:");
            Console.WriteLine("1. SortedList:");
            Console.WriteLine("   - Automatically sorted by key");
            Console.WriteLine("   - Slower insertion/deletion (O(n))");
            Console.WriteLine("   - Faster lookup than array but slower than HashTable");
            Console.WriteLine("   - Uses less memory than HashTable");
            Console.WriteLine("   - Keys must be unique and comparable");

            Console.WriteLine("\n2. HashTable:");
            Console.WriteLine("   - No automatic sorting");
            Console.WriteLine("   - Very fast insertion/deletion/lookup (O(1) average)");
            Console.WriteLine("   - Uses more memory due to hash table overhead");
            Console.WriteLine("   - Keys must be unique");
            Console.WriteLine("   - Thread-safe for multiple readers");
        }
    }
}
