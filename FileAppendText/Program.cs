using FileAppendText.Services;

namespace FileAppendText
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== File Append Text with Read-Only Access ===\n");

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "appendfile.txt");
            var fileService = new FileAppendService(filePath);

            Console.WriteLine($"Working with file: {filePath}");
            Console.WriteLine("This application will open the file for writing with exclusive access.");
            Console.WriteLine("Other applications can only read the file while it's open.\n");

            // Menu-driven interface
            while (true)
            {
                Console.WriteLine("\n=== File Operations Menu ===");
                Console.WriteLine("1. Open File for Writing");
                Console.WriteLine("2. Append Text to File");
                Console.WriteLine("3. Read File Content (as our app)");
                Console.WriteLine("4. Simulate Other Application Reading");
                Console.WriteLine("5. Display File Information");
                Console.WriteLine("6. Close File");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice (1-7): ");

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
                            fileService.OpenFileForWriting();
                            break;

                        case "2":
                            Console.Write("Enter text to append: ");
                            string? text = Console.ReadLine();
                            if (!string.IsNullOrEmpty(text))
                            {
                                fileService.AppendText(text);
                            }
                            else
                            {
                                Console.WriteLine("No text entered.");
                            }
                            break;

                        case "3":
                            Console.WriteLine("\n=== File Content (Our Application) ===");
                            string content = fileService.ReadFileContent();
                            Console.WriteLine(content);
                            break;

                        case "4":
                            fileService.SimulateOtherApplicationRead();
                            break;

                        case "5":
                            fileService.DisplayFileInfo();
                            break;

                        case "6":
                            fileService.CloseFile();
                            break;

                        case "7":
                            fileService.CloseFile(); // Ensure file is closed before exit
                            Console.WriteLine("Thank you for using File Append Service!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1-7.");
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
