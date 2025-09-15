using FileCopyUtility.Services;

namespace FileCopyUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== File Copy Utility ===\n");

            var fileCopyService = new FileCopyService();
            string currentDirectory = Directory.GetCurrentDirectory();

            Console.WriteLine("This application copies files to user-specified destinations.");
            Console.WriteLine($"Current directory: {currentDirectory}\n");

            // Menu-driven interface
            while (true)
            {
                Console.WriteLine("\n=== File Copy Menu ===");
                Console.WriteLine("1. Copy file to destination");
                Console.WriteLine("2. Copy file with progress tracking");
                Console.WriteLine("3. Create sample file for testing");
                Console.WriteLine("4. Display file information");
                Console.WriteLine("5. Compare two files");
                Console.WriteLine("6. Show recent files in directory");
                Console.WriteLine("7. Batch copy multiple files");
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
                            CopySingleFile(fileCopyService);
                            break;

                        case "2":
                            CopyFileWithProgress(fileCopyService);
                            break;

                        case "3":
                            CreateSampleFile(fileCopyService);
                            break;

                        case "4":
                            DisplayFileInfo(fileCopyService);
                            break;

                        case "5":
                            CompareFiles(fileCopyService);
                            break;

                        case "6":
                            ShowRecentFiles(fileCopyService);
                            break;

                        case "7":
                            BatchCopyFiles(fileCopyService);
                            break;

                        case "8":
                            Console.WriteLine("Thank you for using File Copy Utility!");
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

        static void CopySingleFile(FileCopyService fileCopyService)
        {
            Console.WriteLine("\n=== Copy Single File ===");

            Console.Write("Enter source file path: ");
            string? sourcePath = Console.ReadLine();
            if (string.IsNullOrEmpty(sourcePath))
            {
                Console.WriteLine("Source path cannot be empty.");
                return;
            }

            Console.Write("Enter destination file path: ");
            string? destinationPath = Console.ReadLine();
            if (string.IsNullOrEmpty(destinationPath))
            {
                Console.WriteLine("Destination path cannot be empty.");
                return;
            }

            Console.Write("Overwrite if destination exists? (y/n): ");
            string? overwriteInput = Console.ReadLine();
            bool overwrite = overwriteInput?.ToLower() == "y";

            fileCopyService.CopyFile(sourcePath, destinationPath, overwrite);
        }

        static void CopyFileWithProgress(FileCopyService fileCopyService)
        {
            Console.WriteLine("\n=== Copy File with Progress ===");

            Console.Write("Enter source file path: ");
            string? sourcePath = Console.ReadLine();
            if (string.IsNullOrEmpty(sourcePath))
            {
                Console.WriteLine("Source path cannot be empty.");
                return;
            }

            Console.Write("Enter destination file path: ");
            string? destinationPath = Console.ReadLine();
            if (string.IsNullOrEmpty(destinationPath))
            {
                Console.WriteLine("Destination path cannot be empty.");
                return;
            }

            Console.Write("Overwrite if destination exists? (y/n): ");
            string? overwriteInput = Console.ReadLine();
            bool overwrite = overwriteInput?.ToLower() == "y";

            fileCopyService.CopyFileWithProgress(sourcePath, destinationPath, overwrite);
        }

        static void CreateSampleFile(FileCopyService fileCopyService)
        {
            Console.WriteLine("\n=== Create Sample File ===");

            Console.Write("Enter file path: ");
            string? filePath = Console.ReadLine();
            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("File path cannot be empty.");
                return;
            }

            Console.Write("Enter file size in KB (default 1024): ");
            string? sizeInput = Console.ReadLine();
            int sizeInKB = 1024;
            if (!string.IsNullOrEmpty(sizeInput) && int.TryParse(sizeInput, out int customSize))
            {
                sizeInKB = customSize;
            }

            fileCopyService.CreateSampleFile(filePath, sizeInKB);
        }

        static void DisplayFileInfo(FileCopyService fileCopyService)
        {
            Console.WriteLine("\n=== Display File Information ===");

            Console.Write("Enter file path: ");
            string? filePath = Console.ReadLine();
            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("File path cannot be empty.");
                return;
            }

            fileCopyService.DisplayFileInfo(filePath);
        }

        static void CompareFiles(FileCopyService fileCopyService)
        {
            Console.WriteLine("\n=== Compare Files ===");

            Console.Write("Enter first file path: ");
            string? file1 = Console.ReadLine();
            if (string.IsNullOrEmpty(file1))
            {
                Console.WriteLine("First file path cannot be empty.");
                return;
            }

            Console.Write("Enter second file path: ");
            string? file2 = Console.ReadLine();
            if (string.IsNullOrEmpty(file2))
            {
                Console.WriteLine("Second file path cannot be empty.");
                return;
            }

            fileCopyService.CompareFiles(file1, file2);
        }

        static void ShowRecentFiles(FileCopyService fileCopyService)
        {
            Console.WriteLine("\n=== Show Recent Files ===");

            Console.Write("Enter directory path (or press Enter for current directory): ");
            string? directory = Console.ReadLine();
            if (string.IsNullOrEmpty(directory))
            {
                directory = Directory.GetCurrentDirectory();
            }

            Console.Write("Number of recent files to show (default 10): ");
            string? countInput = Console.ReadLine();
            int count = 10;
            if (!string.IsNullOrEmpty(countInput) && int.TryParse(countInput, out int customCount))
            {
                count = customCount;
            }

            var recentFiles = fileCopyService.GetRecentFiles(directory, count);
            
            if (recentFiles.Count == 0)
            {
                Console.WriteLine("No files found in the directory.");
                return;
            }

            Console.WriteLine($"\nRecent files in {directory}:");
            for (int i = 0; i < recentFiles.Count; i++)
            {
                var fileInfo = new FileInfo(recentFiles[i]);
                Console.WriteLine($"{i + 1}. {fileInfo.Name} - {fileInfo.LastWriteTime:yyyy-MM-dd HH:mm:ss} - {fileInfo.Length} bytes");
            }
        }

        static void BatchCopyFiles(FileCopyService fileCopyService)
        {
            Console.WriteLine("\n=== Batch Copy Files ===");

            Console.Write("Enter source directory: ");
            string? sourceDir = Console.ReadLine();
            if (string.IsNullOrEmpty(sourceDir) || !Directory.Exists(sourceDir))
            {
                Console.WriteLine("Invalid source directory.");
                return;
            }

            Console.Write("Enter destination directory: ");
            string? destDir = Console.ReadLine();
            if (string.IsNullOrEmpty(destDir))
            {
                Console.WriteLine("Destination directory cannot be empty.");
                return;
            }

            Console.Write("Enter file extension filter (e.g., .txt, .pdf) or press Enter for all files: ");
            string? extension = Console.ReadLine();

            Console.Write("Overwrite if destination exists? (y/n): ");
            string? overwriteInput = Console.ReadLine();
            bool overwrite = overwriteInput?.ToLower() == "y";

            try
            {
                var files = Directory.GetFiles(sourceDir);
                if (!string.IsNullOrEmpty(extension))
                {
                    files = files.Where(f => f.EndsWith(extension, StringComparison.OrdinalIgnoreCase)).ToArray();
                }

                if (files.Length == 0)
                {
                    Console.WriteLine("No files found matching the criteria.");
                    return;
                }

                Console.WriteLine($"Found {files.Length} files to copy.");

                int successCount = 0;
                int failCount = 0;

                foreach (var file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string destPath = Path.Combine(destDir, fileName);

                    Console.WriteLine($"\nCopying: {fileName}");
                    bool success = fileCopyService.CopyFile(file, destPath, overwrite);
                    
                    if (success)
                        successCount++;
                    else
                        failCount++;
                }

                Console.WriteLine($"\n=== Batch Copy Summary ===");
                Console.WriteLine($"Total files: {files.Length}");
                Console.WriteLine($"Successfully copied: {successCount}");
                Console.WriteLine($"Failed: {failCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during batch copy: {ex.Message}");
            }
        }
    }
}
