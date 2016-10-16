using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NaiveWordCounter.Interfaces;

namespace NaiveWordCounter
{
	class Program
	{
		static void Main(string[] args)
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();

			var compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);

			var results = compareTheWords.Compare("RailwayChildren.txt");
			var top10Results = results.Take(10).ToList<string>(); 

			top10Results.ForEach(i => Console.WriteLine(i));
			Console.ReadLine();
		}
	}

	public class CompareTheWords
	{
		protected readonly IFileReader _fileReader;
		protected readonly IWordCounter _wordCounter;
		protected readonly IPrimeNumberCalculator _primeNumberCalculator;
		protected readonly IOutputGenerator _outputGenerator;

		public CompareTheWords(IFileReader fileReader, IWordCounter wordCounter, IPrimeNumberCalculator primeNumberCalculator, IOutputGenerator outputGenerator)
		{
			_fileReader = fileReader;
			_wordCounter = wordCounter;
			_primeNumberCalculator = primeNumberCalculator;
			_outputGenerator = outputGenerator;
		}
		
		public List<string> Compare(string fileName)
		{		
			var fileContent = _fileReader.ReadTextFile(fileName);
			var wordCount = _wordCounter.Count(fileContent);
			var primes = _primeNumberCalculator.CalculatePrimes(wordCount);
			var output = _outputGenerator.GenerateOutput(wordCount, primes);

			return output;
		}
	}
}

