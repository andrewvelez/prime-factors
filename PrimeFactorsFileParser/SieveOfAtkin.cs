using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PrimeFactorsFileParser
{
    /// <summary>
    /// Attribution:
    /// http://stackoverflow.com/questions/1569127/c-implementation-of-the-sieve-of-atkin
    /// Modifications:
    /// Removed silly Any() call and added IsPrime method
    /// </summary>
    public class SieveOfAtkin : IEnumerable<ulong>
    {

        private readonly List<ulong> primes;
        private readonly ulong limit;

        public SieveOfAtkin(ulong limit)
        {
            this.limit = limit;
            primes = new List<ulong>();
        }

        public bool IsPrime(ulong number)
        {
            if (primes.Count == 0)
            {
                FindPrimes();
            }

            return primes.Contains(number);
        }

        private void FindPrimes()
        {
            var isPrime = new bool[limit + 1];
            var sqrt = Math.Sqrt(limit);

            for (ulong x = 1; x <= sqrt; x++)
                for (ulong y = 1; y <= sqrt; y++)
                {
                    var n = 4 * x * x + y * y;
                    if (n <= limit && (n % 12 == 1 || n % 12 == 5))
                        isPrime[n] ^= true;

                    n = 3 * x * x + y * y;
                    if (n <= limit && n % 12 == 7)
                        isPrime[n] ^= true;

                    n = 3 * x * x - y * y;
                    if (x > y && n <= limit && n % 12 == 11)
                        isPrime[n] ^= true;
                }

            for (ulong n = 5; n <= sqrt; n++)
                if (isPrime[n])
                    for (ulong k = n * n; k <= limit; k *= k)
                        isPrime[k] = false;

            primes.Add(2);
            primes.Add(3);
            for (ulong n = 5; n <= limit; n++)
                if (isPrime[n])
                    primes.Add(n);
        }

        public IEnumerator<ulong> GetEnumerator()
        {
            if (primes.Count == 0)
            {
                FindPrimes();
            }

            foreach (var p in primes)
                yield return p;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}