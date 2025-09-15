using CurriculumVitaeGenerator.Models;

namespace CurriculumVitaeGenerator.Services
{
    public class CVGeneratorService
    {
        private readonly string _outputDirectory;

        public CVGeneratorService(string outputDirectory = "CVs")
        {
            _outputDirectory = outputDirectory;
            EnsureOutputDirectoryExists();
        }

        private void EnsureOutputDirectoryExists()
        {
            if (!Directory.Exists(_outputDirectory))
            {
                Directory.CreateDirectory(_outputDirectory);
                Console.WriteLine($"Created directory: {_outputDirectory}");
            }
        }

        public void GenerateCV(Person person)
        {
            try
            {
                string fileName = $"{person.GetFileName()}.txt";
                string filePath = Path.Combine(_outputDirectory, fileName);

                using (var writer = new StreamWriter(filePath))
                {
                    WriteCVContent(writer, person);
                }

                Console.WriteLine($"CV generated successfully: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating CV for {person.Name}: {ex.Message}");
                throw;
            }
        }

        private void WriteCVContent(StreamWriter writer, Person person)
        {
            // Header
            writer.WriteLine("=" * 60);
            writer.WriteLine($"CURRICULUM VITAE");
            writer.WriteLine("=" * 60);
            writer.WriteLine();

            // Personal Information
            writer.WriteLine("PERSONAL INFORMATION");
            writer.WriteLine("-" * 20);
            writer.WriteLine($"Name: {person.Name}");
            writer.WriteLine($"Location: {person.Location}");
            writer.WriteLine($"Email: {person.Email}");
            writer.WriteLine($"Phone: {person.Phone}");
            writer.WriteLine($"Address: {person.Address}");
            writer.WriteLine($"Date of Birth: {person.DateOfBirth:dd/MM/yyyy}");
            writer.WriteLine($"Age: {person.GetAge()} years");
            writer.WriteLine($"Nationality: {person.Nationality}");
            writer.WriteLine($"Marital Status: {person.MaritalStatus}");
            writer.WriteLine();

            // Objective
            if (!string.IsNullOrEmpty(person.Objective))
            {
                writer.WriteLine("CAREER OBJECTIVE");
                writer.WriteLine("-" * 17);
                writer.WriteLine(person.Objective);
                writer.WriteLine();
            }

            // Skills
            if (person.Skills.Count > 0)
            {
                writer.WriteLine("SKILLS");
                writer.WriteLine("-" * 6);
                foreach (var skill in person.Skills)
                {
                    writer.WriteLine($"• {skill}");
                }
                writer.WriteLine();
            }

            // Education
            if (person.EducationHistory.Count > 0)
            {
                writer.WriteLine("EDUCATION");
                writer.WriteLine("-" * 9);
                foreach (var education in person.EducationHistory.OrderByDescending(e => e.StartDate))
                {
                    writer.WriteLine($"{education.Degree} in {education.FieldOfStudy}");
                    writer.WriteLine($"{education.Institution}, {education.Location}");
                    writer.WriteLine($"Duration: {education.GetDuration()}");
                    if (education.GPA > 0)
                    {
                        writer.WriteLine($"GPA: {education.GPA:F2}");
                    }
                    if (education.Achievements.Count > 0)
                    {
                        writer.WriteLine("Achievements:");
                        foreach (var achievement in education.Achievements)
                        {
                            writer.WriteLine($"  • {achievement}");
                        }
                    }
                    writer.WriteLine();
                }
            }

            // Work Experience
            if (person.WorkExperience.Count > 0)
            {
                writer.WriteLine("WORK EXPERIENCE");
                writer.WriteLine("-" * 15);
                foreach (var work in person.WorkExperience.OrderByDescending(w => w.StartDate))
                {
                    writer.WriteLine($"{work.Position}");
                    writer.WriteLine($"{work.Company}, {work.Location}");
                    writer.WriteLine($"Duration: {work.GetDuration()} ({work.GetDurationInMonths()} months)");
                    writer.WriteLine($"Employment Type: {work.EmploymentType}");
                    
                    if (work.Responsibilities.Count > 0)
                    {
                        writer.WriteLine("Responsibilities:");
                        foreach (var responsibility in work.Responsibilities)
                        {
                            writer.WriteLine($"  • {responsibility}");
                        }
                    }
                    
                    if (work.Achievements.Count > 0)
                    {
                        writer.WriteLine("Achievements:");
                        foreach (var achievement in work.Achievements)
                        {
                            writer.WriteLine($"  • {achievement}");
                        }
                    }
                    writer.WriteLine();
                }
            }

            // Languages
            if (person.Languages.Count > 0)
            {
                writer.WriteLine("LANGUAGES");
                writer.WriteLine("-" * 10);
                foreach (var language in person.Languages)
                {
                    writer.WriteLine($"• {language}");
                }
                writer.WriteLine();
            }

            // Certifications
            if (person.Certifications.Count > 0)
            {
                writer.WriteLine("CERTIFICATIONS");
                writer.WriteLine("-" * 14);
                foreach (var certification in person.Certifications)
                {
                    writer.WriteLine($"• {certification}");
                }
                writer.WriteLine();
            }

            // Footer
            writer.WriteLine("=" * 60);
            writer.WriteLine($"Generated on: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            writer.WriteLine("=" * 60);
        }

        public void GenerateMultipleCVs(List<Person> people)
        {
            Console.WriteLine($"Generating {people.Count} CVs...");
            
            foreach (var person in people)
            {
                GenerateCV(person);
            }
            
            Console.WriteLine($"\nAll CVs generated successfully in '{_outputDirectory}' directory.");
        }

        public void DisplayGeneratedCVs()
        {
            try
            {
                var cvFiles = Directory.GetFiles(_outputDirectory, "*.txt");
                
                Console.WriteLine($"\n=== Generated CV Files in '{_outputDirectory}' ===");
                
                if (cvFiles.Length == 0)
                {
                    Console.WriteLine("No CV files found.");
                    return;
                }

                foreach (var file in cvFiles)
                {
                    var fileInfo = new FileInfo(file);
                    Console.WriteLine($"• {fileInfo.Name} ({fileInfo.Length} bytes, Created: {fileInfo.CreationTime:yyyy-MM-dd HH:mm})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying CV files: {ex.Message}");
            }
        }

        public void CreateSamplePeople()
        {
            var samplePeople = new List<Person>
            {
                CreateSamplePerson1(),
                CreateSamplePerson2(),
                CreateSamplePerson3()
            };

            GenerateMultipleCVs(samplePeople);
        }

        private Person CreateSamplePerson1()
        {
            var person = new Person("John Smith", "New York")
            {
                Email = "john.smith@email.com",
                Phone = "+1-555-0123",
                Address = "123 Main Street, New York, NY 10001",
                DateOfBirth = new DateTime(1990, 5, 15),
                Nationality = "American",
                MaritalStatus = "Single",
                Objective = "Seeking a challenging position as a Software Developer to utilize my technical skills and contribute to innovative projects."
            };

            person.Skills.AddRange(new[] { "C#", "Java", "Python", "SQL", "Git", "Agile Development" });
            person.Languages.AddRange(new[] { "English (Native)", "Spanish (Conversational)" });

            person.EducationHistory.Add(new Education("University of Technology", "Bachelor of Science", "Computer Science", new DateTime(2012, 9, 1), new DateTime(2016, 6, 1))
            {
                Location = "New York",
                GPA = 3.8,
                Achievements = { "Dean's List", "Computer Science Society President" }
            });

            person.WorkExperience.Add(new WorkExperience("TechCorp Solutions", "Software Developer", new DateTime(2016, 7, 1), new DateTime(2020, 12, 1))
            {
                Location = "New York",
                Responsibilities = { "Developed web applications using C# and ASP.NET", "Collaborated with cross-functional teams", "Maintained and updated existing software systems" },
                Achievements = { "Led a team of 3 developers", "Improved system performance by 25%" }
            });

            person.WorkExperience.Add(new WorkExperience("InnovateTech", "Senior Software Developer", new DateTime(2021, 1, 1))
            {
                Location = "New York",
                Responsibilities = { "Design and implement scalable software solutions", "Mentor junior developers", "Participate in architecture decisions" },
                Achievements = { "Successfully delivered 5 major projects", "Reduced deployment time by 40%" }
            });

            person.Certifications.AddRange(new[] { "Microsoft Certified Developer", "AWS Solutions Architect" });

            return person;
        }

        private Person CreateSamplePerson2()
        {
            var person = new Person("Sarah Johnson", "London")
            {
                Email = "sarah.johnson@email.com",
                Phone = "+44-20-7946-0958",
                Address = "456 Oxford Street, London, UK",
                DateOfBirth = new DateTime(1988, 3, 22),
                Nationality = "British",
                MaritalStatus = "Married",
                Objective = "Experienced Marketing Manager looking to drive growth and innovation in digital marketing strategies."
            };

            person.Skills.AddRange(new[] { "Digital Marketing", "SEO/SEM", "Social Media Management", "Analytics", "Project Management", "Content Creation" });
            person.Languages.AddRange(new[] { "English (Native)", "French (Fluent)", "German (Intermediate)" });

            person.EducationHistory.Add(new Education("London Business School", "Master of Business Administration", "Marketing", new DateTime(2010, 9, 1), new DateTime(2012, 6, 1))
            {
                Location = "London",
                GPA = 3.9
            });

            person.EducationHistory.Add(new Education("University of Cambridge", "Bachelor of Arts", "Business Studies", new DateTime(2006, 9, 1), new DateTime(2010, 6, 1))
            {
                Location = "Cambridge",
                GPA = 3.7
            });

            person.WorkExperience.Add(new WorkExperience("Global Marketing Ltd", "Marketing Manager", new DateTime(2018, 3, 1))
            {
                Location = "London",
                Responsibilities = { "Develop and execute marketing campaigns", "Manage digital marketing budget", "Lead marketing team of 5 people" },
                Achievements = { "Increased online sales by 150%", "Reduced customer acquisition cost by 30%" }
            });

            person.WorkExperience.Add(new WorkExperience("Digital Solutions Inc", "Marketing Specialist", new DateTime(2015, 6, 1), new DateTime(2018, 2, 1))
            {
                Location = "London",
                Responsibilities = { "Created content for social media platforms", "Analyzed marketing campaign performance", "Collaborated with external agencies" }
            });

            person.Certifications.AddRange(new[] { "Google Analytics Certified", "HubSpot Content Marketing Certified", "PMP Certified" });

            return person;
        }

        private Person CreateSamplePerson3()
        {
            var person = new Person("Ahmed Hassan", "Dubai")
            {
                Email = "ahmed.hassan@email.com",
                Phone = "+971-4-123-4567",
                Address = "789 Sheikh Zayed Road, Dubai, UAE",
                DateOfBirth = new DateTime(1992, 8, 10),
                Nationality = "Emirati",
                MaritalStatus = "Single",
                Objective = "Motivated Financial Analyst seeking to apply analytical skills in investment banking and portfolio management."
            };

            person.Skills.AddRange(new[] { "Financial Analysis", "Excel VBA", "Bloomberg Terminal", "Risk Management", "Investment Research", "Python" });
            person.Languages.AddRange(new[] { "Arabic (Native)", "English (Fluent)", "French (Basic)" });

            person.EducationHistory.Add(new Education("American University of Dubai", "Master of Science", "Finance", new DateTime(2016, 9, 1), new DateTime(2018, 6, 1))
            {
                Location = "Dubai",
                GPA = 3.9,
                Achievements = { "Graduated Summa Cum Laude", "Finance Club President" }
            });

            person.EducationHistory.Add(new Education("University of Sharjah", "Bachelor of Science", "Business Administration", new DateTime(2012, 9, 1), new DateTime(2016, 6, 1))
            {
                Location = "Sharjah",
                GPA = 3.8
            });

            person.WorkExperience.Add(new WorkExperience("Emirates Investment Bank", "Financial Analyst", new DateTime(2018, 7, 1))
            {
                Location = "Dubai",
                Responsibilities = { "Analyze financial statements and market trends", "Prepare investment reports", "Assist in portfolio management" },
                Achievements = { "Identified investment opportunities worth $10M", "Improved portfolio performance by 15%" }
            });

            person.WorkExperience.Add(new WorkExperience("Deloitte UAE", "Financial Advisory Intern", new DateTime(2017, 6, 1), new DateTime(2017, 8, 1))
            {
                Location = "Dubai",
                EmploymentType = "Internship",
                Responsibilities = { "Supported financial due diligence projects", "Prepared financial models", "Assisted in client presentations" }
            });

            person.Certifications.AddRange(new[] { "CFA Level 1", "FRM Certified", "Excel Advanced Certified" });

            return person;
        }
    }
}
