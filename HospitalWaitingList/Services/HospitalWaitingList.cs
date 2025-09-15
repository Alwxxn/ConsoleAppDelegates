using HospitalWaitingList.Models;

namespace HospitalWaitingList.Services
{
    public class HospitalWaitingListService
    {
        private readonly Queue<Patient> _waitingQueue;
        private readonly List<Patient> _processedPatients;

        public HospitalWaitingListService()
        {
            _waitingQueue = new Queue<Patient>();
            _processedPatients = new List<Patient>();
        }

        public void AddPatient(Patient patient)
        {
            _waitingQueue.Enqueue(patient);
            Console.WriteLine($"Patient {patient.Name} added to waiting list.");
        }

        public Patient? GetNextPatient()
        {
            if (_waitingQueue.Count == 0)
            {
                Console.WriteLine("No patients in waiting list.");
                return null;
            }

            var nextPatient = _waitingQueue.Dequeue();
            _processedPatients.Add(nextPatient);
            Console.WriteLine($"Patient {nextPatient.Name} has been called to see the doctor.");
            return nextPatient;
        }

        public void DisplayWaitingList()
        {
            if (_waitingQueue.Count == 0)
            {
                Console.WriteLine("No patients currently waiting.");
                return;
            }

            Console.WriteLine("\nCurrent Waiting List:");
            Console.WriteLine("=====================");
            var queueArray = _waitingQueue.ToArray();
            for (int i = 0; i < queueArray.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {queueArray[i]}");
            }
        }

        public void DisplayProcessedPatients()
        {
            if (_processedPatients.Count == 0)
            {
                Console.WriteLine("No patients have been processed yet.");
                return;
            }

            Console.WriteLine("\nProcessed Patients:");
            Console.WriteLine("==================");
            foreach (var patient in _processedPatients)
            {
                Console.WriteLine($"✓ {patient}");
            }
        }

        public int GetWaitingCount()
        {
            return _waitingQueue.Count;
        }

        public bool HasPatientsWaiting()
        {
            return _waitingQueue.Count > 0;
        }
    }
}
