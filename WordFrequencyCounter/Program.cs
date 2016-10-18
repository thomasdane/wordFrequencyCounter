using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using WordFrequencyCounter.Tests.Unit.Interfaces;
using WordFrequencyCounter.Tests.Unit.Interfaces;

namespace WordFrequencyCounter.Tests.Unit
{
	class Program
	{
		static void Main()
		{
			//Outputter.Output();

			BenchMarksVersion1.RailwayChildrenVersion1();
			BenchMarks_WordCounterWithoutRegex.RailwayChildren_NoRegex();
			//BenchMarks_HardCodedPrimes.RailwayChildren_HardCodedPrimes();

			BenchMarksVersion1.WarAndPeaceVersion1();
			BenchMarks_WordCounterWithoutRegex.WarAndPeace_NoRegex();
			//BenchMarks_HardCodedPrimes.WarAndPeace_HardCodedPrimes();

			Console.ReadLine();
		}
	}

	static class Outputter
	{
		public static void Output()
		{
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			CompareTheWords compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);

			var railwayChildren = compareTheWords.Compare("RailwayChildren.txt");
			var railwayChildrenTop10Results = railwayChildren.Take(10).ToList();
			railwayChildrenTop10Results.ForEach(i => Console.WriteLine(i));
		}		
	}
}

