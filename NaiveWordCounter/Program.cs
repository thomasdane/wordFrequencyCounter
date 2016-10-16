using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace NaiveWordCounter
{
	class Program
	{
		static void Main(string[] args)
		{
			var compareTheWords = new CompareTheWords();
			var results = compareTheWords.Compare("RailwayChildren.txt");
			var top10Results = results.Take(10).ToList<string>(); 

			top10Results.ForEach(i => Console.WriteLine(i));
			Console.ReadLine();
		}
	}

	public class CompareTheWords
	{
		public List<string> Compare(string textFile)
		{
			var fileHandler = new FileReader();
			var content = fileHandler.ReadTextFile(textFile);

			var wordCounter = new WordCounter();
			var wordCount = wordCounter.Count(content);

			var primeNumberCalculator = new PrimeNumberCalculator();
			var primes = primeNumberCalculator.GetPrimes(wordCount);

			var outputGenerator = new OutputGenerator();
			var output = outputGenerator.GenerateOutput(wordCount, primes);

			return output;
		}

		public IDictionary<string, int> GetWordCount(string fileName)
		{
			var fileHandler = new FileReader();
			var content = fileHandler.ReadTextFile(fileName);
			var wordCounter = new WordCounter();
			var result = wordCounter.Count(content);
			return result;
		}

		public IDictionary<int, bool> GetPrimes(IDictionary<string, int> wordCount)
		{
			var primeNumberCalculator = new PrimeNumberCalculator();
			var integers = primeNumberCalculator.GetDistinctIntegers(wordCount);
			var primes = primeNumberCalculator.GetListOfPrimes(integers);
			return primes;
		}
	}
}

