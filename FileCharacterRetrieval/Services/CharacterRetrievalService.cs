namespace FileCharacterRetrieval.Services
{
    public class CharacterRetrievalService
    {
        public string RetrieveCharactersFromFile(string filePath, int startPosition, int characterCount)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return "Error: File does not exist.";
                }

                string content = File.ReadAllText(filePath);
                
                if (string.IsNullOrEmpty(content))
                {
                    return "Error: File is empty.";
                }

                if (startPosition < 1)
                {
                    return "Error: Start position must be 1 or greater.";
                }

                if (startPosition > content.Length)
                {
                    return $"Error: Start position ({startPosition}) exceeds file length ({content.Length}).";
                }

                int actualStartIndex = startPosition - 1; // Convert to 0-based index
                int endIndex = Math.Min(actualStartIndex + characterCount, content.Length);
                int actualCharacterCount = endIndex - actualStartIndex;

                string retrievedCharacters = content.Substring(actualStartIndex, actualCharacterCount);
                
                return retrievedCharacters;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public void DisplayFileAnalysis(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Error: File does not exist.");
                    return;
                }

                string content = File.ReadAllText(filePath);
                
                Console.WriteLine($"\n=== File Analysis ===");
                Console.WriteLine($"File Path: {filePath}");
                Console.WriteLine($"Total Characters: {content.Length}");
                Console.WriteLine($"Total Lines: {content.Split('\n').Length}");
                Console.WriteLine($"Total Words: {content.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length}");
                
                if (content.Length > 0)
                {
                    Console.WriteLine($"First Character: '{content[0]}'");
                    Console.WriteLine($"Last Character: '{content[content.Length - 1]}'");
                    
                    if (content.Length >= 3)
                    {
                        Console.WriteLine($"3rd Character: '{content[2]}'");
                    }
                    
                    if (content.Length >= 7)
                    {
                        Console.WriteLine($"Characters 3-7: '{content.Substring(2, 5)}'");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error analyzing file: {ex.Message}");
            }
        }

        public void DisplayCharacterPositions(string filePath, int startPosition, int characterCount)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Error: File does not exist.");
                    return;
                }

                string content = File.ReadAllText(filePath);
                
                Console.WriteLine($"\n=== Character Position Analysis ===");
                Console.WriteLine($"Requested: {characterCount} characters starting from position {startPosition}");
                
                if (startPosition < 1 || startPosition > content.Length)
                {
                    Console.WriteLine("Invalid start position for this file.");
                    return;
                }

                int actualStartIndex = startPosition - 1;
                int endIndex = Math.Min(actualStartIndex + characterCount, content.Length);
                int actualCharacterCount = endIndex - actualStartIndex;

                Console.WriteLine($"Actual characters retrieved: {actualCharacterCount}");
                Console.WriteLine($"Character positions: {startPosition} to {startPosition + actualCharacterCount - 1}");
                
                string retrievedCharacters = content.Substring(actualStartIndex, actualCharacterCount);
                
                Console.WriteLine("\nRetrieved Characters:");
                for (int i = 0; i < retrievedCharacters.Length; i++)
                {
                    char character = retrievedCharacters[i];
                    int position = startPosition + i;
                    string displayChar = char.IsControl(character) ? $"\\{(int)character}" : character.ToString();
                    Console.WriteLine($"  Position {position}: '{displayChar}'");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error analyzing character positions: {ex.Message}");
            }
        }

        public void CreateSampleFile(string filePath)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath) ?? "";
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string sampleContent = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!@#$%^&*()";
                
                File.WriteAllText(filePath, sampleContent);
                Console.WriteLine($"Sample file created: {filePath}");
                Console.WriteLine($"Content: {sampleContent}");
                Console.WriteLine($"Length: {sampleContent.Length} characters");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating sample file: {ex.Message}");
            }
        }

        public void DisplayFileContent(string filePath, int maxCharacters = 100)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Error: File does not exist.");
                    return;
                }

                string content = File.ReadAllText(filePath);
                
                Console.WriteLine($"\n=== File Content ===");
                if (content.Length <= maxCharacters)
                {
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine(content.Substring(0, maxCharacters) + "...");
                    Console.WriteLine($"[Content truncated - showing first {maxCharacters} of {content.Length} characters]");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying file content: {ex.Message}");
            }
        }
    }
}
