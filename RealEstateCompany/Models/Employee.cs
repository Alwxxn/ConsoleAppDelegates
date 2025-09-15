namespace RealEstateCompany.Models
{
    public class Employee
    {
        public string Name { get; set; } = string.Empty;
        public string WorkPosition { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal Salary { get; set; }
        public string ContactInfo { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public List<Estate> AssignedEstates { get; set; } = new List<Estate>();

        public Employee(string name, string workPosition, int experienceYears, decimal salary = 0, string contactInfo = "")
        {
            Name = name;
            WorkPosition = workPosition;
            ExperienceYears = experienceYears;
            Salary = salary;
            ContactInfo = contactInfo;
            HireDate = DateTime.Now;
        }

        public void AssignEstate(Estate estate)
        {
            if (!AssignedEstates.Contains(estate))
            {
                AssignedEstates.Add(estate);
            }
        }

        public bool IsSeniorAgent()
        {
            return ExperienceYears >= 5;
        }

        public override string ToString()
        {
            return $"{Name} - {WorkPosition} ({ExperienceYears} years experience)";
        }
    }
}
