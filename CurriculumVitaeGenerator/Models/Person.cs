namespace CurriculumVitaeGenerator.Models
{
    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public string MaritalStatus { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new List<string>();
        public List<Education> EducationHistory { get; set; } = new List<Education>();
        public List<WorkExperience> WorkExperience { get; set; } = new List<WorkExperience>();
        public List<string> Languages { get; set; } = new List<string>();
        public string Objective { get; set; } = string.Empty;
        public List<string> Certifications { get; set; } = new List<string>();

        public Person(string name, string location)
        {
            Name = name;
            Location = location;
        }

        public string GetFileName()
        {
            // Concatenate name and location for filename
            string fileName = $"{Name}_{Location}".Replace(" ", "_");
            // Remove special characters that might cause file system issues
            fileName = System.Text.RegularExpressions.Regex.Replace(fileName, @"[^\w\-_]", "");
            return fileName;
        }

        public int GetAge()
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
