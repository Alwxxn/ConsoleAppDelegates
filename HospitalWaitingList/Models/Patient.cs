namespace HospitalWaitingList.Models
{
    public class Patient
    {
        public string Name { get; set; } = string.Empty;
        public int Priority { get; set; } = 1; // 1 = Normal, 2 = High, 3 = Emergency
        public DateTime ArrivalTime { get; set; }
        public string MedicalCondition { get; set; } = string.Empty;

        public Patient(string name, int priority = 1, string medicalCondition = "")
        {
            Name = name;
            Priority = priority;
            ArrivalTime = DateTime.Now;
            MedicalCondition = medicalCondition;
        }

        public override string ToString()
        {
            return $"{Name} (Priority: {Priority}, Condition: {MedicalCondition}, Arrived: {ArrivalTime:HH:mm})";
        }
    }
}
