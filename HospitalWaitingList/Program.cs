using HospitalWaitingList.Models;
using HospitalWaitingList.Services;

namespace HospitalWaitingList
{
    class Program
    {
        static void Main(string[] args)
        {
            var hospitalService = new HospitalWaitingListService();
            
            Console.WriteLine("=== Hospital Patient Waiting List System ===\n");

            // Add some sample patients
            hospitalService.AddPatient(new Patient("John Smith", 1, "Regular checkup"));
            hospitalService.AddPatient(new Patient("Sarah Johnson", 2, "High fever"));
            hospitalService.AddPatient(new Patient("Mike Davis", 1, "Cough and cold"));
            hospitalService.AddPatient(new Patient("Emergency Patient", 3, "Severe chest pain"));
            hospitalService.AddPatient(new Patient("Lisa Brown", 1, "Annual physical"));

            Console.WriteLine($"\nTotal patients waiting: {hospitalService.GetWaitingCount()}");
            hospitalService.DisplayWaitingList();

            Console.WriteLine("\n--- Processing Patients ---");
            
            // Process patients one by one
            while (hospitalService.HasPatientsWaiting())
            {
                Console.WriteLine("\nPress Enter to call next patient...");
                Console.ReadKey();
                
                var nextPatient = hospitalService.GetNextPatient();
                if (nextPatient != null)
                {
                    Console.WriteLine($"Current waiting count: {hospitalService.GetWaitingCount()}");
                }
            }

            Console.WriteLine("\nAll patients have been processed!");
            hospitalService.DisplayProcessedPatients();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
