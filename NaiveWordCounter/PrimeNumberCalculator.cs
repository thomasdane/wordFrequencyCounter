using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveWordCounter
{
	public class PrimeNumberCalculator
	{
		public List<int> GetDistinctIntegers(IDictionary<string, int> wordCountResults)
		{
			var result = wordCountResults.Values.Distinct().ToList();
			return result;
		}

		public IDictionary<int, bool> GetPrimes(IDictionary<string, int> wordCountResults)
		{
			var distinctIntegers = GetDistinctIntegers(wordCountResults);
			var listOfPrimes = GetListOfPrimes(distinctIntegers);
			return listOfPrimes;
		}

		public IDictionary<int, bool> GetListOfPrimes(IList<int> ListOfIntegers)
		{
			var result = new Dictionary<int, bool>() { };

			foreach (var integer in ListOfIntegers)
			{
				if (IsPrime(integer))
				{
					result.Add(integer, true);
				}
			}

			return result;
		}

		public bool IsPrime(int integer)
		{
			//this is the most naive way to find primes
			//most obviously, it loops through all numbers, even if we can discount them
			//for example, once we know that the integer is not divisible by 5
			//we do not need to check 10, 15, 20 and so on
			if (integer == 1) return false;
			if (integer == 2) return true;

			if (integer % 2 == 0) return false; //Even number     

			for (int i = 3; i < integer; i += 2)
			{
				if (integer % i == 0) return false;
			}

			return true;
		}
	}
}
