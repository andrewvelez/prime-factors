using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeFactorsFileParser
{
    internal class PrimesParser
    {

        private List<int> _InputIntegers;
        public string ErrorMessage { get; private set; }

        public void LoadFile(string filePath)
        {
            if (!File.Exists(filePath))
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

    }
}