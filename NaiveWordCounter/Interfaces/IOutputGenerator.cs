using System.Collections.Generic;

namespace NaiveWordCounter.Interfaces
{
	public interface IOutputGenerator
	{
		List<string> GenerateOutput(IDictionary<string, int> wordCountResults, IDictionary<int, bool> ListOfPrimes);
	}
}
