namespace CurriculumVitaeGenerator.Models
{
    public class Education
    {
        public string Institution { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string FieldOfStudy { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double GPA { get; set; }
        public string Location { get; set; } = string.Empty;
        public List<string> Achievements { get; set; } = new List<string>();

        public Education(string institution, string degree, string fieldOfStudy, DateTime startDate, DateTime? endDate = null)
        {
            Institution = institution;
            Degree = degree;
            FieldOfStudy = fieldOfStudy;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool IsCompleted => EndDate.HasValue;
        public bool IsCurrentlyStudying => !EndDate.HasValue;

        public string GetDuration()
        {
            if (IsCurrentlyStudying)
                return $"{StartDate:MMM yyyy} - Present";
            
            return $"{StartDate:MMM yyyy} - {EndDate:MMM yyyy}";
        }
    }
}
