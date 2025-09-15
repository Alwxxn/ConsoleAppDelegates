namespace CurriculumVitaeGenerator.Models
{
    public class WorkExperience
    {
        public string Company { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public List<string> Responsibilities { get; set; } = new List<string>();
        public List<string> Achievements { get; set; } = new List<string>();
        public string EmploymentType { get; set; } = "Full-time"; // Full-time, Part-time, Contract, Internship

        public WorkExperience(string company, string position, DateTime startDate, DateTime? endDate = null)
        {
            Company = company;
            Position = position;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool IsCurrentJob => !EndDate.HasValue;

        public string GetDuration()
        {
            if (IsCurrentJob)
                return $"{StartDate:MMM yyyy} - Present";
            
            return $"{StartDate:MMM yyyy} - {EndDate:MMM yyyy}";
        }

        public int GetDurationInMonths()
        {
            var endDate = EndDate ?? DateTime.Now;
            var duration = endDate - StartDate;
            return (int)(duration.TotalDays / 30.44); // Average days per month
        }
    }
}
