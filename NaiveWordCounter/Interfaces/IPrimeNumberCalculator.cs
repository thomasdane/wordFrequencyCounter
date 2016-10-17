using System.Collections.Generic;

namespace NaiveWordCounter.Interfaces
{
	public interface IPrimeNumberCalculator
	{
		List<int> GetDistinctIntegers(IDictionary<string, int> wordCountResults);
		IDictionary<int, bool> CalculatePrimes(IDictionary<string, int> wordCountResults);
		IDictionary<int, bool> GetListOfPrimes(IList<int> listOfIntegers);
		bool IsPrime(int integer);
	}
}
