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
			BenchMarksVersion1.RailwayChildrenVersion1();
			BenchMarks_HardCodedPrimes.RailwayChildren_HardCodedPrimes();
			BenchMarksVersion1.WarAndPeaceVersion1();
			BenchMarks_HardCodedPrimes.WarAndPeace_HardCodedPrimes();

			Console.ReadLine();
		}
	}
}

