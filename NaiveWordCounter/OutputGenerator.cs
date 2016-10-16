using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveWordCounter.Interfaces;


namespace NaiveWordCounter
{
	public class OutputGenerator : IOutputGenerator
	{
		public List<string> GenerateOutput(IDictionary<string, int> wordCountResults, IDictionary<int, bool> ListOfPrimes)
		{
			var output = new List<string> { };
			bool isPrime;

			foreach (KeyValuePair<string, int> wordCount in wordCountResults)
			{
				//is the word count number present in the list of primes? 
				isPrime = ListOfPrimes.Keys.Contains(wordCount.Value) ? true : false;

				var formattedString = String.Format("{0}, {1}, {2}", wordCount.Key.ToString(), wordCount.Value.ToString(), isPrime.ToString());

				output.Add(formattedString);
			}

			return output;
		}
	}
}
