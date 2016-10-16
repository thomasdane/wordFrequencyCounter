using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaiveWordCounter.Interfaces
{
	public interface IPrimeNumberCalculator
	{
		List<int> GetDistinctIntegers(IDictionary<string, int> wordCountResults);
		IDictionary<int, bool> GetPrimes(IDictionary<string, int> wordCountResults);
		IDictionary<int, bool> GetListOfPrimes(IList<int> ListOfIntegers);
		bool IsPrime(int integer);
	}
}
