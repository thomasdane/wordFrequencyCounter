using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NaiveWordCounter.Interfaces;
using System.Diagnostics;

namespace NaiveWordCounter
{
	class Program
	{
		static void Main()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			var compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);

			//Benchmark for Railway Children

			Stopwatch sw = new Stopwatch();
			sw.Start();
			var railwayChildren = compareTheWords.Compare("RailwayChildren.txt");
			var railwayChildrenTop10Results = railwayChildren.Take(10).ToList<string>();
			railwayChildrenTop10Results.ForEach(i => Console.WriteLine(i));
			sw.Stop();
			Console.WriteLine("Elapsed={0}", sw.Elapsed);

			//Benchmark for War and Peace

			Stopwatch sw2 = new Stopwatch();
			sw2.Start();
			var warAndPeace = compareTheWords.Compare("WarAndPeace.txt");
			var warAndPeaceTop10Results = warAndPeace.Take(10).ToList<string>();
			warAndPeaceTop10Results.ForEach(i => Console.WriteLine(i));
			sw2.Stop();
			Console.WriteLine("Elapsed={0}", sw2.Elapsed);
			Console.ReadLine();
		}
	}

	public class CompareTheWords
	{
		private readonly IFileReader _fileReader;
		private readonly IWordCounter _wordCounter;
		private readonly IPrimeNumberCalculator _primeNumberCalculator;
		private readonly IOutputGenerator _outputGenerator;

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

