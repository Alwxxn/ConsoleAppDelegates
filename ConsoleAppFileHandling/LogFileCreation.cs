using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFileHandling
{
    public class LogFileCreation
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter first number");
                int firstNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter second number");
                int secondNumber = Convert.ToInt32(Console.ReadLine());
                int result = firstNumber / secondNumber;
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {

                string location = "logerror.txt";
                StreamWriter sw;
                if (File.Exists(location))
                {
                    sw = new StreamWriter(location, true);        //file exists + open in append mode

                }
                else
                {
                    //File does not exist + create new file
                    sw = new StreamWriter(location);
                }
                sw.WriteLine(DateTime.Now.ToString() + " " + ex.Message);
                sw.Close();
                Console.WriteLine("Error! see the log file for details.");
            }


            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

    }
}

