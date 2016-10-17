using System;
using System.Collections.Generic;
using NaiveWordCounter.Interfaces;

namespace NaiveWordCounter
{
	public class OutputGenerator : IOutputGenerator
	{
		public List<string> GenerateOutput(IDictionary<string, int> wordCountResults, IDictionary<int, bool> ListOfPrimes)
		{
			var output = new List<string>();

			foreach (KeyValuePair<string, int> wordCount in wordCountResults)
			{
				//is the word count number present in the list of primes? 
				var isPrime = ListOfPrimes.Keys.Contains(wordCount.Value);

				var formattedString = String.Format("{0}, {1}, {2}", wordCount.Key, wordCount.Value, isPrime);

				output.Add(formattedString);
			}

			return output;
		}
	}
}
