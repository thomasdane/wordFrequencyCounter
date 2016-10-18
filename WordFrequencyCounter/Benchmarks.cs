using System;
using System.Diagnostics;
using System.Linq;
using WordFrequencyCounter.Tests.Unit.Interfaces;
using WordFrequencyCounter.Tests.Unit.SecondVersion;

namespace WordFrequencyCounter.Tests.Unit
{
	public static class BenchMarksVersion1
	{
		public static void RailwayChildrenVersion1()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			CompareTheWords compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);

			Stopwatch sw = new Stopwatch();
			sw.Start();

			compareTheWords.Compare("RailwayChildren.txt");

			sw.Stop();
			Console.WriteLine("{0} = RailWay Children Regular", sw.Elapsed);
		}

		public static void WarAndPeaceVersion1()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			CompareTheWords compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);

			Stopwatch sw = new Stopwatch();
			sw.Start();

			compareTheWords.Compare("WarAndPeace.txt");

			sw.Stop();
			Console.WriteLine("{0} = War and Peace Regular", sw.Elapsed);
		}
	}

	public static class BenchMarks_HardCodedPrimes
	{
		public static void RailwayChildren_HardCodedPrimes()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator_HardCoded();
			IOutputGenerator outputGenerator = new OutputGenerator();
			CompareTheWords compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);

			Stopwatch sw = new Stopwatch();
			sw.Start();

			compareTheWords.Compare("RailwayChildren.txt");

			sw.Stop();
			Console.WriteLine("{0} = Railway Children hardcoded primes", sw.Elapsed);
		}

		public static void WarAndPeace_HardCodedPrimes()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator_HardCoded();
			IOutputGenerator outputGenerator = new OutputGenerator();
			CompareTheWords compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);

			Stopwatch sw = new Stopwatch();
			sw.Start();

			compareTheWords.Compare("WarAndPeace.txt");

			sw.Stop();
			Console.WriteLine("{0} = War and Peace hardcoded primes", sw.Elapsed);
		}
	}

	public static class BenchMarks_WordCounterWithoutRegex
	{
		public static void RailwayChildren_NoRegex()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounterWithoutRegex();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			CompareTheWords compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);

			Stopwatch sw = new Stopwatch();
			sw.Start();

			compareTheWords.Compare("RailwayChildren.txt");

			sw.Stop();
			Console.WriteLine("{0} = Railway Children no regex", sw.Elapsed);
		}

		public static void WarAndPeace_NoRegex()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounterWithoutRegex();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			CompareTheWords compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);

			Stopwatch sw = new Stopwatch();
			sw.Start();

			compareTheWords.Compare("WarAndPeace.txt");

			sw.Stop();
			Console.WriteLine("{0} = War and Peace no regex", sw.Elapsed);
		}
	}
}
