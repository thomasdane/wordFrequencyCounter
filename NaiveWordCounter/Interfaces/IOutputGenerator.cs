using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaiveWordCounter.Interfaces
{
	public interface IOutputGenerator
	{
		List<string> GenerateOutput(IDictionary<string, int> wordCountResults, IDictionary<int, bool> ListOfPrimes);
	}
}
