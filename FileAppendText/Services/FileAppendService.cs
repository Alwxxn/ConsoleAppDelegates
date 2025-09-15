namespace FileAppendText.Services
{
    public class FileAppendService
    {
        private readonly string _filePath;
        private FileStream? _fileStream;
        private StreamWriter? _streamWriter;

        public FileAppendService(string filePath)
        {
            _filePath = filePath;
        }

        public void OpenFileForWriting()
        {
            try
            {
                // Create directory if it doesn't exist
                string? directory = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Open file for writing with exclusive access
                _fileStream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                _streamWriter = new StreamWriter(_fileStream);
                
                Console.WriteLine($"File opened for writing: {_filePath}");
                Console.WriteLine("Other applications can only read this file while it's open.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening file: {ex.Message}");
                throw;
            }
        }

        public void AppendText(string text)
        {
            if (_streamWriter == null)
            {
                throw new InvalidOperationException("File is not open for writing. Call OpenFileForWriting() first.");
            }

            try
            {
                _streamWriter.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {text}");
                _streamWriter.Flush(); // Ensure data is written immediately
                
                Console.WriteLine($"Text appended: {text}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending text: {ex.Message}");
                throw;
            }
        }

        public void CloseFile()
        {
            try
            {
                _streamWriter?.Close();
                _fileStream?.Close();
                
                _streamWriter = null;
                _fileStream = null;
                
                Console.WriteLine("File closed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing file: {ex.Message}");
                throw;
            }
        }

        public string ReadFileContent()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return "File does not exist.";
                }

                return File.ReadAllText(_filePath);
            }
            catch (Exception ex)
            {
                return $"Error reading file: {ex.Message}";
            }
        }

        public void DisplayFileInfo()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine("File does not exist.");
                    return;
                }

                var fileInfo = new FileInfo(_filePath);
                Console.WriteLine($"\n=== File Information ===");
                Console.WriteLine($"Path: {_filePath}");
                Console.WriteLine($"Size: {fileInfo.Length} bytes");
                Console.WriteLine($"Created: {fileInfo.CreationTime:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"Is Open for Writing: {_streamWriter != null}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting file info: {ex.Message}");
            }
        }

        public void SimulateOtherApplicationRead()
        {
            try
            {
                Console.WriteLine("\n=== Simulating Other Application Reading File ===");
                string content = ReadFileContent();
                
                if (content == "File does not exist.")
                {
                    Console.WriteLine("Other application: File does not exist yet.");
                    return;
                }

                Console.WriteLine("Other application successfully read the file:");
                Console.WriteLine("--- File Content ---");
                Console.WriteLine(content);
                Console.WriteLine("--- End Content ---");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Other application error: {ex.Message}");
            }
        }

        ~FileAppendService()
        {
            CloseFile();
        }
    }
}
