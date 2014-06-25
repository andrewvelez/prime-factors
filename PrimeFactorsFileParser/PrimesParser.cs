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
        private List<bool> _PartialPrimeSieve;
        private Dictionary<int, bool> _Primes;

        public PrimesParser()
        {
            _InputIntegers = new List<int>();
            _PartialPrimeSieve = new List<bool>();
            _PartialPrimeSieve[0] = false;
            _PartialPrimeSieve[1] = false;
            _PartialPrimeSieve[2] = true;
            _PartialPrimeSieve[3] = true;
            _Primes = new Dictionary<int, bool>();
            _Primes.Add(0, false);
            _Primes.Add(1, false);
            _Primes.Add(2, true);
            _Primes.Add(3, true);
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
            string result = "";

            for (int i = 1; i <= number; i++)
            {
                if (number % i == 0)
                {
                    result += i.ToString() + ",";
                }
            }

            result = result.Trim(',');
            return result;
        }

        private bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }
            else if (_Primes.ContainsKey(number))
            {
                return _Primes[number];
            }
            else
            {
                return false;
            }
        }

    }
}