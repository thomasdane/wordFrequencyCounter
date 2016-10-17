using System.Collections.Generic;
using System.Linq;
using NaiveWordCounter.Interfaces;

namespace NaiveWordCounter
{
	public class PrimeNumberCalculator : IPrimeNumberCalculator
	{
		public IDictionary<int, bool> CalculatePrimes(IDictionary<string, int> wordCountResults)
		{
			var distinctIntegers = GetDistinctIntegers(wordCountResults);
			var listOfPrimes = GetListOfPrimes(distinctIntegers);
			return listOfPrimes;
		}
		
		public List<int> GetDistinctIntegers(IDictionary<string, int> wordCountResults)
		{
			var result = wordCountResults.Values.Distinct().ToList();
			return result;
		}

		public IDictionary<int, bool> GetListOfPrimes(IList<int> listOfIntegers)
		{
			var result = new Dictionary<int, bool>();

			foreach (var integer in listOfIntegers)
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
			if (integer == 1) return false;
			if (integer == 2) return true;
			if (integer % 2 == 0) return false; //Even numbers   

			for (int i = 3; i < integer; i += 2)
			{
				if (integer % i == 0) return false;
			}

			return true;
		}
	}
}
