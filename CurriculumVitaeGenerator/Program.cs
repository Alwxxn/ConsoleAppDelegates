using CurriculumVitaeGenerator.Models;
using CurriculumVitaeGenerator.Services;

namespace CurriculumVitaeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Curriculum Vitae Generator ===\n");

            var cvGenerator = new CVGeneratorService();

            Console.WriteLine("This application generates CV files based on person entities.");
            Console.WriteLine("File names are created by concatenating Name and Location.");
            Console.WriteLine($"CVs will be saved in: {Path.Combine(Directory.GetCurrentDirectory(), "CVs")}\n");

            // Menu-driven interface
            while (true)
            {
                Console.WriteLine("\n=== CV Generator Menu ===");
                Console.WriteLine("1. Generate sample CVs (3 people)");
                Console.WriteLine("2. Create custom person and generate CV");
                Console.WriteLine("3. Display generated CV files");
                Console.WriteLine("4. Read and display a CV file");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice (1-5): ");

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
                            Console.WriteLine("\nGenerating sample CVs...");
                            cvGenerator.CreateSamplePeople();
                            break;

                        case "2":
                            CreateCustomPerson(cvGenerator);
                            break;

                        case "3":
                            cvGenerator.DisplayGeneratedCVs();
                            break;

                        case "4":
                            ReadAndDisplayCV();
                            break;

                        case "5":
                            Console.WriteLine("Thank you for using CV Generator!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1-5.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void CreateCustomPerson(CVGeneratorService cvGenerator)
        {
            Console.WriteLine("\n=== Create Custom Person ===");

            Console.Write("Enter name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            Console.Write("Enter location: ");
            string? location = Console.ReadLine();
            if (string.IsNullOrEmpty(location))
            {
                Console.WriteLine("Location cannot be empty.");
                return;
            }

            var person = new Person(name, location);

            Console.Write("Enter email: ");
            person.Email = Console.ReadLine() ?? "";

            Console.Write("Enter phone: ");
            person.Phone = Console.ReadLine() ?? "";

            Console.Write("Enter address: ");
            person.Address = Console.ReadLine() ?? "";

            Console.Write("Enter date of birth (dd/MM/yyyy): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dob))
            {
                person.DateOfBirth = dob;
            }

            Console.Write("Enter nationality: ");
            person.Nationality = Console.ReadLine() ?? "";

            Console.Write("Enter marital status: ");
            person.MaritalStatus = Console.ReadLine() ?? "";

            Console.Write("Enter career objective: ");
            person.Objective = Console.ReadLine() ?? "";

            // Add skills
            Console.WriteLine("Enter skills (press Enter after each skill, empty line to finish):");
            string? skill;
            while (!string.IsNullOrEmpty(skill = Console.ReadLine()))
            {
                person.Skills.Add(skill);
            }

            // Add languages
            Console.WriteLine("Enter languages (press Enter after each language, empty line to finish):");
            string? language;
            while (!string.IsNullOrEmpty(language = Console.ReadLine()))
            {
                person.Languages.Add(language);
            }

            // Add education
            Console.WriteLine("\nAdd education details? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                AddEducation(person);
            }

            // Add work experience
            Console.WriteLine("\nAdd work experience? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                AddWorkExperience(person);
            }

            // Generate CV
            Console.WriteLine($"\nGenerating CV for {person.Name}...");
            cvGenerator.GenerateCV(person);
            Console.WriteLine($"CV filename: {person.GetFileName()}.txt");
        }

        static void AddEducation(Person person)
        {
            Console.WriteLine("\n=== Add Education ===");

            Console.Write("Enter institution: ");
            string? institution = Console.ReadLine();
            if (string.IsNullOrEmpty(institution)) return;

            Console.Write("Enter degree: ");
            string? degree = Console.ReadLine();
            if (string.IsNullOrEmpty(degree)) return;

            Console.Write("Enter field of study: ");
            string? field = Console.ReadLine();
            if (string.IsNullOrEmpty(field)) return;

            Console.Write("Enter start date (dd/MM/yyyy): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate)) return;

            Console.Write("Enter end date (dd/MM/yyyy) or press Enter if ongoing: ");
            DateTime? endDate = null;
            if (DateTime.TryParse(Console.ReadLine(), out DateTime end))
            {
                endDate = end;
            }

            var education = new Education(institution, degree, field, startDate, endDate);

            Console.Write("Enter location: ");
            education.Location = Console.ReadLine() ?? "";

            Console.Write("Enter GPA (optional): ");
            if (double.TryParse(Console.ReadLine(), out double gpa))
            {
                education.GPA = gpa;
            }

            person.EducationHistory.Add(education);
            Console.WriteLine("Education added successfully.");
        }

        static void AddWorkExperience(Person person)
        {
            Console.WriteLine("\n=== Add Work Experience ===");

            Console.Write("Enter company: ");
            string? company = Console.ReadLine();
            if (string.IsNullOrEmpty(company)) return;

            Console.Write("Enter position: ");
            string? position = Console.ReadLine();
            if (string.IsNullOrEmpty(position)) return;

            Console.Write("Enter start date (dd/MM/yyyy): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate)) return;

            Console.Write("Enter end date (dd/MM/yyyy) or press Enter if current job: ");
            DateTime? endDate = null;
            if (DateTime.TryParse(Console.ReadLine(), out DateTime end))
            {
                endDate = end;
            }

            var work = new WorkExperience(company, position, startDate, endDate);

            Console.Write("Enter location: ");
            work.Location = Console.ReadLine() ?? "";

            Console.Write("Enter employment type (Full-time/Part-time/Contract/Internship): ");
            work.EmploymentType = Console.ReadLine() ?? "Full-time";

            Console.WriteLine("Enter responsibilities (press Enter after each, empty line to finish):");
            string? responsibility;
            while (!string.IsNullOrEmpty(responsibility = Console.ReadLine()))
            {
                work.Responsibilities.Add(responsibility);
            }

            person.WorkExperience.Add(work);
            Console.WriteLine("Work experience added successfully.");
        }

        static void ReadAndDisplayCV()
        {
            Console.WriteLine("\n=== Read CV File ===");

            Console.WriteLine("Available CV files:");
            string cvDirectory = Path.Combine(Directory.GetCurrentDirectory(), "CVs");
            if (!Directory.Exists(cvDirectory))
            {
                Console.WriteLine("CV directory does not exist.");
                return;
            }

            var cvFiles = Directory.GetFiles(cvDirectory, "*.txt");
            if (cvFiles.Length == 0)
            {
                Console.WriteLine("No CV files found.");
                return;
            }

            for (int i = 0; i < cvFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(cvFiles[i])}");
            }

            Console.Write("Enter file number to read: ");
            if (int.TryParse(Console.ReadLine(), out int fileNumber) && fileNumber >= 1 && fileNumber <= cvFiles.Length)
            {
                string filePath = cvFiles[fileNumber - 1];
                try
                {
                    string content = File.ReadAllText(filePath);
                    Console.WriteLine($"\n=== Content of {Path.GetFileName(filePath)} ===");
                    Console.WriteLine(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid file number.");
            }
        }
    }
}
