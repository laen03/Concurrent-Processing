using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static readonly string txtPath = @"C:\Users\chech\OneDrive - Estudiantes ITCR\TEC\SI 2022\Principios de Sistemas Operativos\Procesamiento Concurrente\ConsoleApp1\Modified";

        static readonly string copyrightDisclaimer = "\nCopyright Disclaimer under Section 107 of the copyright act 1976, allowance is made for fair use for purposes such as criticism, \n" +
            " comment, news reporting, scholarship, and research. Fair use is a use permitted by copyright statute that might otherwise be infringing. Non-profit, educational or \n" +
            "personal use tips the balance in favour of fair use.\n";

        static readonly string endOfFile = "=========================================================EOF=========================================================";

        static readonly Stopwatch stopWatch = new Stopwatch();

        static int fileCounter = 1;

        public static void Modify(string path)
        {
            try
            {
                int wordCounter = 0;
                StreamReader reader = new StreamReader(path);
                string line = reader.ReadToEnd();

                string[] fileWords = line.Split(" ");

                reader.Close();

                foreach (string word in fileWords)
                {
                    wordCounter++;
                }

                int copyrightTimes = wordCounter / 30000;

                StreamWriter writer = new StreamWriter(path, true, Encoding.ASCII);
                while (copyrightTimes > 0)
                {
                    writer.Write(copyrightDisclaimer);
                    writer.WriteLine();
                    copyrightTimes--;
                }
                writer.Write(endOfFile);
                writer.Close();

                Console.WriteLine("Words in file {0}: {1} words!", fileCounter, wordCounter);
                Console.WriteLine();
                fileCounter++;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public static void Secuential()
        {
            stopWatch.Start();

            string[] array = Directory.GetFiles(txtPath);
            for (int i = 0; i < array.Length; i++)
            {
                string txtFilePath = array[i];
                Modify(txtFilePath);
            }
            Console.WriteLine("Tiempo de ejecución del Foreach secuencial: {0} segundos\n", stopWatch.Elapsed.TotalSeconds);
            stopWatch.Reset();
        }

        public static void Parallel_()
        {
            stopWatch.Start();

            Parallel.ForEach(Directory.GetFiles(txtPath), txtFile =>
                {
                    Modify(txtFile);
                }
            );

            Console.WriteLine("Tiempo de ejecución del Foreach paralelo: {0} segundos\n", stopWatch.Elapsed.TotalSeconds);
            stopWatch.Reset();
        }

        static void Main(string[] args)
        {
            //Secuential();
            Parallel_();
        }
    }
}
