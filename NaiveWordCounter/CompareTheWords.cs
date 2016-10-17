using System;
using System.Collections.Generic;
using System.Diagnostics;
using NaiveWordCounter.Interfaces;

namespace NaiveWordCounter
{
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
			Stopwatch sw = new Stopwatch();

			sw.Start();
			var fileContent = _fileReader.ReadTextFile(fileName);
			sw.Stop();
			Console.WriteLine("{0} = file reader", sw.Elapsed);

			Stopwatch sw1 = new Stopwatch();
			sw1.Start();
			var wordCount = _wordCounter.Count(fileContent);
			sw1.Stop();
			Console.WriteLine("{0} = word count", sw1.Elapsed);

			Stopwatch sw2 = new Stopwatch();
			sw2.Start();
			var primes = _primeNumberCalculator.CalculatePrimes(wordCount);
			sw2.Stop();
			Console.WriteLine("{0} = primes", sw2.Elapsed);

			Stopwatch sw3 = new Stopwatch();
			sw3.Start();
			var output = _outputGenerator.GenerateOutput(wordCount, primes);
			sw3.Stop();
			Console.WriteLine("{0} = output", sw3.Elapsed);

			return output;
		}
	}
}
