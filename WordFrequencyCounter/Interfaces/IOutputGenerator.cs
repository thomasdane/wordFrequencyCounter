﻿using System.Collections.Generic;

namespace WordFrequencyCounter.Interfaces
{
	public interface IOutputGenerator
	{
		List<string> GenerateOutput(IDictionary<string, int> wordCountResults, IDictionary<int, bool> listOfPrimes);
	}
}
