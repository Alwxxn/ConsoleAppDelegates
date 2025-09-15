namespace FileCopyUtility.Services
{
    public class FileCopyService
    {
        public bool CopyFile(string sourcePath, string destinationPath, bool overwrite = false)
        {
            try
            {
                // Validate source file
                if (!File.Exists(sourcePath))
                {
                    Console.WriteLine($"Error: Source file does not exist: {sourcePath}");
                    return false;
                }

                // Check if destination file exists and overwrite is not allowed
                if (File.Exists(destinationPath) && !overwrite)
                {
                    Console.WriteLine($"Error: Destination file already exists: {destinationPath}");
                    Console.WriteLine("Use overwrite option or choose a different destination.");
                    return false;
                }

                // Ensure destination directory exists
                string? destinationDirectory = Path.GetDirectoryName(destinationPath);
                if (!string.IsNullOrEmpty(destinationDirectory) && !Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                    Console.WriteLine($"Created destination directory: {destinationDirectory}");
                }

                // Perform the copy operation
                File.Copy(sourcePath, destinationPath, overwrite);

                // Verify the copy was successful
                if (File.Exists(destinationPath))
                {
                    var sourceInfo = new FileInfo(sourcePath);
                    var destInfo = new FileInfo(destinationPath);
                    
                    Console.WriteLine("File copied successfully!");
                    Console.WriteLine($"Source: {sourcePath} ({sourceInfo.Length} bytes)");
                    Console.WriteLine($"Destination: {destinationPath} ({destInfo.Length} bytes)");
                    
                    // Verify file sizes match
                    if (sourceInfo.Length == destInfo.Length)
                    {
                        Console.WriteLine("✓ File sizes match - copy verified.");
                    }
                    else
                    {
                        Console.WriteLine("⚠ Warning: File sizes don't match!");
                    }
                    
                    return true;
                }
                else
                {
                    Console.WriteLine("Error: Copy operation failed - destination file not found.");
                    return false;
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: Access denied. Check file permissions.");
                return false;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Error: Directory not found. Check the path.");
                return false;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: I/O operation failed - {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Unexpected error occurred - {ex.Message}");
                return false;
            }
        }

        public bool CopyFileWithProgress(string sourcePath, string destinationPath, bool overwrite = false)
        {
            try
            {
                if (!File.Exists(sourcePath))
                {
                    Console.WriteLine($"Error: Source file does not exist: {sourcePath}");
                    return false;
                }

                if (File.Exists(destinationPath) && !overwrite)
                {
                    Console.WriteLine($"Error: Destination file already exists: {destinationPath}");
                    return false;
                }

                // Ensure destination directory exists
                string? destinationDirectory = Path.GetDirectoryName(destinationPath);
                if (!string.IsNullOrEmpty(destinationDirectory) && !Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                var sourceInfo = new FileInfo(sourcePath);
                long totalBytes = sourceInfo.Length;
                long bytesCopied = 0;

                Console.WriteLine($"Copying file: {sourceInfo.Name}");
                Console.WriteLine($"File size: {FormatFileSize(totalBytes)}");

                using (var sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
                using (var destStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[8192]; // 8KB buffer
                    int bytesRead;

                    while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        destStream.Write(buffer, 0, bytesRead);
                        bytesCopied += bytesRead;

                        // Show progress
                        double percentage = (double)bytesCopied / totalBytes * 100;
                        Console.Write($"\rProgress: {percentage:F1}% ({FormatFileSize(bytesCopied)} / {FormatFileSize(totalBytes)})");
                    }
                }

                Console.WriteLine("\n✓ File copied successfully with progress tracking!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError during copy with progress: {ex.Message}");
                return false;
            }
        }

        public void DisplayFileInfo(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File does not exist: {filePath}");
                    return;
                }

                var fileInfo = new FileInfo(filePath);
                
                Console.WriteLine($"\n=== File Information ===");
                Console.WriteLine($"Name: {fileInfo.Name}");
                Console.WriteLine($"Full Path: {fileInfo.FullName}");
                Console.WriteLine($"Size: {FormatFileSize(fileInfo.Length)} ({fileInfo.Length} bytes)");
                Console.WriteLine($"Created: {fileInfo.CreationTime:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"Last Accessed: {fileInfo.LastAccessTime:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"Directory: {fileInfo.DirectoryName}");
                Console.WriteLine($"Extension: {fileInfo.Extension}");
                Console.WriteLine($"Attributes: {fileInfo.Attributes}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting file information: {ex.Message}");
            }
        }

        public void CompareFiles(string file1, string file2)
        {
            try
            {
                if (!File.Exists(file1))
                {
                    Console.WriteLine($"File 1 does not exist: {file1}");
                    return;
                }

                if (!File.Exists(file2))
                {
                    Console.WriteLine($"File 2 does not exist: {file2}");
                    return;
                }

                var file1Info = new FileInfo(file1);
                var file2Info = new FileInfo(file2);

                Console.WriteLine($"\n=== File Comparison ===");
                Console.WriteLine($"File 1: {file1Info.Name} ({FormatFileSize(file1Info.Length)})");
                Console.WriteLine($"File 2: {file2Info.Name} ({FormatFileSize(file2Info.Length)})");

                if (file1Info.Length == file2Info.Length)
                {
                    Console.WriteLine("✓ File sizes are identical");
                    
                    // Perform byte-by-byte comparison for small files
                    if (file1Info.Length < 10 * 1024 * 1024) // Less than 10MB
                    {
                        bool filesIdentical = true;
                        using (var stream1 = new FileStream(file1, FileMode.Open, FileAccess.Read))
                        using (var stream2 = new FileStream(file2, FileMode.Open, FileAccess.Read))
                        {
                            byte[] buffer1 = new byte[1024];
                            byte[] buffer2 = new byte[1024];
                            
                            while (filesIdentical)
                            {
                                int bytesRead1 = stream1.Read(buffer1, 0, buffer1.Length);
                                int bytesRead2 = stream2.Read(buffer2, 0, buffer2.Length);
                                
                                if (bytesRead1 != bytesRead2)
                                {
                                    filesIdentical = false;
                                    break;
                                }
                                
                                if (bytesRead1 == 0) break;
                                
                                for (int i = 0; i < bytesRead1; i++)
                                {
                                    if (buffer1[i] != buffer2[i])
                                    {
                                        filesIdentical = false;
                                        break;
                                    }
                                }
                            }
                        }
                        
                        if (filesIdentical)
                        {
                            Console.WriteLine("✓ Files are identical (byte-by-byte comparison)");
                        }
                        else
                        {
                            Console.WriteLine("⚠ Files have same size but different content");
                        }
                    }
                    else
                    {
                        Console.WriteLine("⚠ Files have same size (content comparison skipped for large files)");
                    }
                }
                else
                {
                    Console.WriteLine("⚠ Files have different sizes");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error comparing files: {ex.Message}");
            }
        }

        public void CreateSampleFile(string filePath, int sizeInKB = 1024)
        {
            try
            {
                string? directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string content = "This is a sample file for testing file copy operations.\n";
                content += $"Created on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";
                content += "Lorem ipsum dolor sit amet, consectetur adipiscing elit.\n";
                content += "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.\n";

                // Repeat content to reach desired size
                while (content.Length < sizeInKB * 1024)
                {
                    content += content;
                }

                // Truncate to exact size
                content = content.Substring(0, sizeInKB * 1024);

                File.WriteAllText(filePath, content);
                Console.WriteLine($"Sample file created: {filePath} ({FormatFileSize(content.Length)})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating sample file: {ex.Message}");
            }
        }

        public List<string> GetRecentFiles(string directory, int count = 10)
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    Console.WriteLine($"Directory does not exist: {directory}");
                    return new List<string>();
                }

                var files = Directory.GetFiles(directory)
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(f => f.LastWriteTime)
                    .Take(count)
                    .Select(f => f.FullName)
                    .ToList();

                return files;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting recent files: {ex.Message}");
                return new List<string>();
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }
    }
}
