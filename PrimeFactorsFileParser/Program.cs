using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeFactorsFileParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length == 0 || string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine("Program must be called with a valid file path as the first argument");
                return;
            }

            PrimesParser parser = new PrimesParser();
            parser.LoadFile(args[0]);

            if (!string.IsNullOrWhiteSpace(parser.ErrorMessage))
            {
                Console.WriteLine(parser.ErrorMessage);
                return;
            }

            Console.WriteLine(parser.GetPrimesOutput());
            Console.ReadLine();
        }
    }
}