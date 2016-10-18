using System;
using System.Diagnostics;
using System.Linq;
using WordFrequencyCounter.Interfaces;
using WordFrequencyCounter.SecondVersion;
using WordFrequencyCounter.Tests.Unit;
using WordFrequencyCounter.Tests.Unit.SecondVersion;

namespace WordFrequencyCounter
{
	public static class BenchMarksVersion1
	{
		private static readonly CompareTheWords CompareTheWords;

		static BenchMarksVersion1()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			CompareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);
		}
		
		public static void RailwayChildrenVersion1()
		{
			var sw = new Stopwatch();
			sw.Start();

			CompareTheWords.Compare("RailwayChildren.txt");

			sw.Stop();
			Console.WriteLine("{0} = RailWay Children Regular", sw.Elapsed);
		}

		public static void WarAndPeaceVersion1()
		{
			var sw = new Stopwatch();
			sw.Start();

			CompareTheWords.Compare("WarAndPeace.txt");

			sw.Stop();
			Console.WriteLine("{0} = War and Peace Regular", sw.Elapsed);
		}
	}

	public static class BenchMarksHardCodedPrimes
	{
		private static readonly CompareTheWords CompareTheWords;

		static BenchMarksHardCodedPrimes()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator_HardCoded();
			IOutputGenerator outputGenerator = new OutputGenerator();
			CompareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);
		}
		
		public static void RailwayChildren_HardCodedPrimes()
		{
			var sw = new Stopwatch();
			sw.Start();

			CompareTheWords.Compare("RailwayChildren.txt");

			sw.Stop();
			Console.WriteLine("{0} = Railway Children hardcoded primes", sw.Elapsed);
		}

		public static void WarAndPeace_HardCodedPrimes()
		{
			var sw = new Stopwatch();
			sw.Start();

			CompareTheWords.Compare("WarAndPeace.txt");

			sw.Stop();
			Console.WriteLine("{0} = War and Peace hardcoded primes", sw.Elapsed);
		}
	}

	public static class BenchMarksWordCounterWithoutRegex
	{
		private static readonly CompareTheWords CompareTheWords;

		static BenchMarksWordCounterWithoutRegex()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounterWithoutRegex();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			CompareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);
		}
		
		public static void RailwayChildren_NoRegex()
		{
			var sw = new Stopwatch();
			sw.Start();

			CompareTheWords.Compare("RailwayChildren.txt");

			sw.Stop();
			Console.WriteLine("{0} = Railway Children no regex", sw.Elapsed);
		}

		public static void WarAndPeace_NoRegex()
		{
			var sw = new Stopwatch();
			sw.Start();

			CompareTheWords.Compare("WarAndPeace.txt");

			sw.Stop();
			Console.WriteLine("{0} = War and Peace no regex", sw.Elapsed);
		}
	}

	public static class Results
	{
		public static void Output()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			var compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);

			var railwayChildren = compareTheWords.Compare("RailwayChildren.txt");
			var railwayChildrenTop10Results = railwayChildren.Take(10).ToList();
			Console.WriteLine("Word Frequency Count for Railway Childen");
			railwayChildrenTop10Results.ForEach(Console.WriteLine);
		}
	}
}
