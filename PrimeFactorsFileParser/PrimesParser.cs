using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PrimeFactorsFileParser
{
    internal class PrimesParser
    {
        
        public string ErrorMessage { get; private set; }
        private List<int> _InputIntegers;

        public PrimesParser()
        {
            _InputIntegers = new List<int>();
        }

        public void LoadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                LoadFileValidPath(filePath);
            }
            else
            {
                ErrorMessage = "File does not exist";
            }
        }

        public string GetPrimesOutput()
        {
            StringBuilder sb = new StringBuilder();

            foreach (int input in _InputIntegers)
            {
                sb.AppendLine(GetCsvPrimes(input));
            }

            return sb.ToString();
        }

        private void LoadFileValidPath(string filePath)
        {
            List<string> inputList = ReadLinesInputFile(filePath);

            int tempInt;
            foreach (string s in inputList)
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    continue;
                }

                if (int.TryParse(s, out tempInt))
                {
                    _InputIntegers.Add(tempInt);
                }
                else
                {
                    ErrorMessage = "One of the lines in the file was not an integer: " + s;
                    _InputIntegers = new List<int>();
                    return;
                }
            }
        }

        private List<string> ReadLinesInputFile(string filePath)
        {
            List<string> listInputLines = new List<string>();

            try
            {
                listInputLines = File.ReadAllLines(filePath).ToList<string>();
            }
            catch (IOException ex)
            {
                ErrorMessage = string.Format("{0}: {1}", ex.GetType().Name, ex.Message);
            }

            return listInputLines;
        }

        private string GetCsvPrimes(int number)
        {
            if (number == 1)
            {
                return "1";
            }
            
            StringBuilder result = new StringBuilder();
            var primes = new SieveOfAtkin((ulong)(System.Math.Sqrt(number) + 1));

            foreach (int p in primes)
            {
                if (p * p > number)
                    break;

                while (number % p == 0)
                {
                    result.Append(p.ToString());
                    result.Append(",");
                    number /= p;
                }
            }

            if (number > 1)
            {
                result.Append(number.ToString());
                result.Append(",");
            }

            string csv = result.ToString();
            return csv.Trim(',');
        }

    }
}