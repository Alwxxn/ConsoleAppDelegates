using System.IO;

namespace ConsoleAppFileHandling
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //StreamWriter -- SreamReader

            //1 Define the file path
            //@ is known as verbatim identifier
            //string bookFile = @"D:\Faith\Faith Camp 5\StreamReader.txt"; // Absolute path

            string bookFile = "StreamReader.txt"; // Relative path


            //2 Write contents to file using StreamWriter
            //using ( StreamWriter sw = new StreamWriter(bookFile))
            //{
            //    sw.WriteLine(System.DateTime.Now);
            //    sw.WriteLine("Welcome to Faith Infotech");
            //    sw.WriteLine("We are located in Trivandrum");
            //}

            //3 Read contents from file using StreamReader
            Console.WriteLine("Reading contents from file:");
            using (StreamReader sr = new StreamReader(bookFile))
            {
                Console.WriteLine(sr.ReadToEnd());
            }

            Console.WriteLine("Visit StreamReader.txt to see the contents.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


        }
    }
}
