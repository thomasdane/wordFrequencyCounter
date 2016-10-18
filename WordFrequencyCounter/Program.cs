using System;

namespace WordFrequencyCounter
{
	internal class Program
	{
		private static void Main()
		{		
			Results.Output();

			BenchMarksVersion1.RailwayChildrenVersion1();
			BenchMarksWordCounterWithoutRegex.RailwayChildren_NoRegex();
			//BenchMarksHardCodedPrimes.RailwayChildren_HardCodedPrimes(); Comment these back in if you would like to see the performance of hardcoded primes.

			BenchMarksVersion1.WarAndPeaceVersion1();
			BenchMarksWordCounterWithoutRegex.WarAndPeace_NoRegex();
			//BenchMarksHardCodedPrimes.WarAndPeace_HardCodedPrimes();

			Console.ReadLine();
		}
	}
}

