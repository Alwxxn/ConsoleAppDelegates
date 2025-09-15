using FileCharacterRetrieval.Services;

namespace FileCharacterRetrieval
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== File Character Retrieval System ===\n");

            string defaultFilePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "sample.txt");
            var characterService = new CharacterRetrievalService();

            Console.WriteLine("This application retrieves 5 characters starting from the 3rd character in a file.");
            Console.WriteLine($"Default file path: {defaultFilePath}\n");

            // Create sample file if it doesn't exist
            if (!File.Exists(defaultFilePath))
            {
                Console.WriteLine("Creating sample file...");
                characterService.CreateSampleFile(defaultFilePath);
            }

            // Menu-driven interface
            while (true)
            {
                Console.WriteLine("\n=== Character Retrieval Menu ===");
                Console.WriteLine("1. Retrieve 5 characters from 3rd position (Assignment Requirement)");
                Console.WriteLine("2. Retrieve custom characters from custom position");
                Console.WriteLine("3. Create new sample file");
                Console.WriteLine("4. Use different file path");
                Console.WriteLine("5. Display file analysis");
                Console.WriteLine("6. Display character position analysis");
                Console.WriteLine("7. Display file content");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice (1-8): ");

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
                            Console.WriteLine("\n=== Assignment Requirement: 5 characters from 3rd position ===");
                            string result = characterService.RetrieveCharactersFromFile(defaultFilePath, 3, 5);
                            Console.WriteLine($"Retrieved characters: '{result}'");
                            characterService.DisplayCharacterPositions(defaultFilePath, 3, 5);
                            break;

                        case "2":
                            Console.Write("Enter start position (1-based): ");
                            if (int.TryParse(Console.ReadLine(), out int startPos))
                            {
                                Console.Write("Enter number of characters to retrieve: ");
                                if (int.TryParse(Console.ReadLine(), out int charCount))
                                {
                                    Console.WriteLine($"\nRetrieving {charCount} characters from position {startPos}...");
                                    string customResult = characterService.RetrieveCharactersFromFile(defaultFilePath, startPos, charCount);
                                    Console.WriteLine($"Retrieved characters: '{customResult}'");
                                    characterService.DisplayCharacterPositions(defaultFilePath, startPos, charCount);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid character count.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid start position.");
                            }
                            break;

                        case "3":
                            Console.Write("Enter file path (or press Enter for default): ");
                            string? newPath = Console.ReadLine();
                            string filePath = string.IsNullOrEmpty(newPath) ? defaultFilePath : newPath;
                            characterService.CreateSampleFile(filePath);
                            break;

                        case "4":
                            Console.Write("Enter new file path: ");
                            string? customPath = Console.ReadLine();
                            if (!string.IsNullOrEmpty(customPath))
                            {
                                defaultFilePath = customPath;
                                Console.WriteLine($"File path changed to: {defaultFilePath}");
                            }
                            break;

                        case "5":
                            characterService.DisplayFileAnalysis(defaultFilePath);
                            break;

                        case "6":
                            Console.WriteLine("\n=== Character Position Analysis for Assignment ===");
                            characterService.DisplayCharacterPositions(defaultFilePath, 3, 5);
                            break;

                        case "7":
                            characterService.DisplayFileContent(defaultFilePath);
                            break;

                        case "8":
                            Console.WriteLine("Thank you for using Character Retrieval System!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1-8.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
