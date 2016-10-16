using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace NaiveWordCounter
{
	class Program
	{
		static void Main(string[] args)
		{
			var railWayChildren = "RailwayChildren.txt";
			var compareTheWords = new CompareTheWords();
			var results = compareTheWords.Compare(railWayChildren);
			var top30Results = results.Take(30).ToList<string>(); 
			top30Results.ForEach(i => Console.WriteLine(i));
			Console.ReadLine();
		}
	}

	public class OutputGenerator
	{
		public List<string> GenerateOutput(IDictionary<string, int> wordCountResults, IDictionary<int, bool> ListOfPrimes)
		{
			var output = new List<string> { };
			bool isPrime;

			foreach(KeyValuePair<string, int> wordCount in wordCountResults){

				//is the word count number present in the list of primes? 
				isPrime = ListOfPrimes.Keys.Contains(wordCount.Value) ? true : false;

				var formattedString = String.Format("{0}, {1}, {2}", wordCount.Key.ToString(), wordCount.Value.ToString(), isPrime.ToString());

				output.Add(formattedString);			
			}

			return output;
		}
	}

	public class CompareTheWords
	{
		public List<string> Compare(string textFile)
		{
			var fileHandler = new FileHandler();
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
			var fileHandler = new FileHandler();
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

	public interface IWordCounter
	{
		IDictionary<string, int> Count(string[] linesOfText);
	}

	public class WordCounter : IWordCounter
	{	
		public IDictionary<string, int> Count(string[] linesOfText)
		{
			var result = new ConcurrentDictionary<string, int>();
			var regex = new Regex("[^\\p{L}]");

			Parallel.For(0, linesOfText.Length, x =>
			{
				var line = linesOfText[x];
				var lineChunks = line.Split(' ');

				foreach (var chunk in lineChunks)
				{
					var word = regex.Replace(chunk, "").ToLower();
			
					if (!string.IsNullOrEmpty(word))
					{
						result.AddOrUpdate(word, 1, (key, value) => value + 1);
						//AddOrUpdate: 
						//Add the word (first param) with a value of 1 (second param) if it does NOT exist
						//Otherwise, add it as a new key, and add 1 to its value (third param)
						//Good explanation here https://www.dotnetperls.com/concurrentdictionary
					}
				}
			});

			var orderedResult = result.OrderByDescending(x => x.Value).ToDictionary(i => i.Key, i => i.Value);;
			return orderedResult;
		}
	}

	public class FileHandler
	{
		public string[] ReadTextFile(string fileName)
		{
			var bookFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Books"));
			var filePath = Path.Combine(bookFolder, fileName);
			var content = File.ReadAllLines(filePath);
			return content;
		}
	}
}

