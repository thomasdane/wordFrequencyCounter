using System;
using System.Diagnostics;
using System.Linq;
using NaiveWordCounter.Interfaces;
using NaiveWordCounter.SecondVersion;

namespace NaiveWordCounter
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

			//Stopwatch sw = new Stopwatch();
			//sw.Start();

			var railwayChildren = compareTheWords.Compare("RailwayChildren.txt");
			//var railwayChildrenTop10Results = railwayChildren.Take(10).ToList();
			//railwayChildrenTop10Results.ForEach(i => Console.WriteLine(i));

			//sw.Stop();
			//Console.WriteLine("{0} = RailWay Children Regular", sw.Elapsed);
		}

		public static void WarAndPeaceVersion1()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			CompareTheWords compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);

			//Stopwatch sw = new Stopwatch();
			//sw.Start();

			var warAndPeace = compareTheWords.Compare("WarAndPeace.txt");
			//var warAndPeaceTop10Results = warAndPeace.Take(10).ToList();
			//warAndPeaceTop10Results.ForEach(i => Console.WriteLine(i));

			//sw.Stop();
			//Console.WriteLine("{0} = War and Peace Regular", sw.Elapsed);
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
}
