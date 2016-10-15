﻿using System;
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

	public class Runner
	{
		public IDictionary<string, int> GetResults(string fileName)
		{
			var fileHandler = new FileHandler();
			var content = fileHandler.ReadTextFile(fileName);
			var wordCounter = new WordCounter();
			var result = wordCounter.Count(content);
			return result;
		}
	}

	public class PrimeNumberCalculator
	{
		public List<int> GetDistinctIntegers(IDictionary<string, int> wordCountResults)
		{
			var result = wordCountResults.Values.Distinct().ToList();
			return result;
		}
	
		public IDictionary<int, bool> IsPrime(IList<int> ListOfIntegers)
		{
			var result = new Dictionary<int, bool>() { };
			bool isPrime = false;

			foreach (var integer in ListOfIntegers)
			{
				if (integer == 1) { isPrime = false; }
				if (integer == 2) { isPrime = true; }
				
				var boundary = (int)Math.Floor(Math.Sqrt(integer));

				for ( int i = 3; i <= boundary; ++i)
				{
					if (integer % i == 0)
					{
						isPrime = false;
						break;
					}
					
					isPrime = true;
				}

				result.Add(integer, isPrime);
			}

			return result;
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

