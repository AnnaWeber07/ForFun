using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace EAndTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(ThreadMethod);
            thread.Start();
            //here I continue the main thread

            Checker checker = new Checker();

            string lines = File.ReadAllText(@"C:\Users\Анна\source\repos\EAndTree\EAndTree\ConstE.txt")
                             .Replace("\n", "");
                           //  .Substring(2);

            var s = Enumerable.Range(0, lines.Length - 10)
                    .Select(x => new { x, s = lines.Substring(x, 10) })
                    .Where(x => !x.s.StartsWith("0"))
                    .First(x => Checker.CheckIfPrime(Convert.ToInt64(x.s)));

            Console.WriteLine(s);

            Console.ReadKey();
        }

        static void ThreadMethod()
        {
            //parallel thread for e computation
            ExpCalculator number = new ExpCalculator(1000);

            number.Calculate();
            number.Print();
            Console.ReadKey();

        }
    }
}
