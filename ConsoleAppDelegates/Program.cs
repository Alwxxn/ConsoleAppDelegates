namespace ConsoleAppDelegates
{
    internal class Program
    {
        //Defining global declaration of Delegate 
        //Access specifier --> return type and parameters of delegates
        //It should be same as method declaration

        public delegate void FindDelegates(int first, int second);
        public delegate void PrintToConsoleDelegates(string msg);
        public delegate string DisplayDelegates(string name);
        //three are created because three different method signatures and different return types
        public class DemoDelegateApp
        {
            //entry point
            static void Main(string[] args)
            {
                DemoDelegateApp demo = new DemoDelegateApp();       //creating object to call 
                                                                    //Instatiating delegates
                FindDelegates fd = new FindDelegates(demo.FindSum);
                //invoke
                fd.Invoke(100, 200);

                PrintToConsoleDelegates pc = new PrintToConsoleDelegates(PrintToConsole);
                pc.Invoke("Hello from static method");

                DisplayDelegates dp = new DisplayDelegates(Display);
                dp.Invoke("Message displayed");

                Console.ReadKey();
            }

            //Non-static method
            public void FindSum(int first, int second)
            {
                Console.WriteLine("Sum is: " + (first + second));
            }

            //Static method
            public static void PrintToConsole(string msg)
            {
                Console.WriteLine(msg);
            }

            //Static with return
            public static string Display(string name)
            {
                return "Hi :" + name;
            }
        }

    }
}
