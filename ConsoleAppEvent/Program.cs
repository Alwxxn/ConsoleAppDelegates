namespace ConsoleAppEvent
{
    //1 Defining delegate (blueprint for event handlers)
    public delegate string WelcomeDelegate(string userName);
    internal class Program
    {
        //2 Iniatialize / 2 Declaring an event using the delegate
        //this is the publisher
        // the event lives ere and will notify subscribes
        //global (below theclass not below the method
        event WelcomeDelegate welcomeEvent;
        public Program()
        {
            //3 attaching the method /function to the event
            //this is the ** subscribers ** (welcome method subscribers to the event)
            welcomeEvent += new WelcomeDelegate(this.Welcome);

        }
        static void Main(string[] args)
        {
            Program objProgram = new Program();
            string result = objProgram.welcomeEvent("Libiya");
            Console.WriteLine(result);
            Console.ReadKey();
        }
        //simple function --- Subscriber's method
        //this is the subscribers method 
        //(it will be executed when event is raised)
        public string Welcome(string userName)
        {
            return "Welcome:" + userName;
        }
    }
}

