using System.Diagnostics;

namespace project1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Task.Run(PrimeCount);
            while (true)
            {
                var input = Console.ReadLine();
                Console.WriteLine(input.ToUpper());
            }
        }
        static void PrimeCount()
        {
            Stopwatch sw = Stopwatch.StartNew();
            int n = 1340000000;
            int count = 0;
            for (int i = 0; i <= n; i++)
            {
                count++;
            }
            Console.WriteLine(sw.Elapsed);
        }
    }
}