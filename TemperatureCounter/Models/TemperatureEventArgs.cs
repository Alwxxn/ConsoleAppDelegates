namespace TemperatureCounter.Models
{
    public class TemperatureEventArgs : EventArgs
    {
        public double CurrentTemperature { get; }
        public double PreviousTemperature { get; }
        public string Message { get; }
        public DateTime Timestamp { get; }

        public TemperatureEventArgs(double currentTemperature, double previousTemperature, string message)
        {
            CurrentTemperature = currentTemperature;
            PreviousTemperature = previousTemperature;
            Message = message;
            Timestamp = DateTime.Now;
        }
    }
}
