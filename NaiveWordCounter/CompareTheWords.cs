using System;
using System.Collections.Generic;
using System.Diagnostics;
using WordFrequencyCounter.Tests.Unit.Interfaces;

namespace WordFrequencyCounter.Tests.Unit
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
			var fileContent = _fileReader.ReadTextFile(fileName);

			var wordCount = _wordCounter.Count(fileContent);

			var primes = _primeNumberCalculator.CalculatePrimes(wordCount);

			var output = _outputGenerator.GenerateOutput(wordCount, primes);

			return output;
		}
	}
}
