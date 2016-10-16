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

		}
	}

	public class CompareTheTextFile
	{
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

			return result;
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

