using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PrimeFactorsFileParser
{
    public class SieveOfAtkinTests
    {

        [Fact]
        public void IsPrime_Limitof11_HasCorrectPrimes()
        {
            var sieve = new SieveOfAtkin(11);
            Assert.Equal(false, sieve.IsPrime(0));
            Assert.Equal(false, sieve.IsPrime(1));
            Assert.Equal(true, sieve.IsPrime(2));
            Assert.Equal(true, sieve.IsPrime(3));
            Assert.Equal(false, sieve.IsPrime(4));
            Assert.Equal(true, sieve.IsPrime(11));
        }

    }
}