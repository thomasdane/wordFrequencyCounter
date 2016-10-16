using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NaiveWordCounter.Interfaces;

namespace NaiveWordCounter.Tests.Integration
{

	[TestFixture]
	public class NaiveWordCounterIntegrationTests
	{
		[Test]
		public void CompareTheTextFile_ShouldReturnCorrectResult_WhenPassedWarAndPeace()
		{
			//Arrange
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			var compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);
			var input = "WarAndPeace.txt";
			var expectedOutput = new string[]
			{
				"the, 34562, False", 
				"and, 22148, False",
				"to, 16709, False",
				"of, 14990, False",
				"a, 10513, True",
			};
			
			//Act
			var actualOutput = compareTheWords.Compare(input);
			var actualOutputTop5 = actualOutput.Take(5).ToList();

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutputTop5);
		}

		[Test]
		public void CompareTheTextFile_ShouldReturnWordFrequencyAndIsPrime_WhenPassedTwoCopiesOfWarAndPeace()
		{
			//Arrange
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			var compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);
			var input = "TwoCopiesOfWarAndPeace.txt";
			var expectedOutput = new string[]
			{
				"the, 69124, False", 
				"and, 44296, False",
				"to, 33418, False",
				"of, 29980, False",
				"a, 21026, False",
			};

			//Act
			var actualOutput = compareTheWords.Compare(input);
			var actualOutputTop5 = actualOutput.Take(5).ToList();

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutputTop5);
		}

		[Test]
		public void CompareTheTextFile_ShouldReturnCorrectResults_WhenPassedRailwayChildren()
		{
			//Arrange
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			var compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);
			var input = "RailwayChildren.txt";
			var expectedOutput = new string[]
			{
				"the, 3344, False", 
				"and, 2390, False",
				"to, 1525, False",
				"a, 1157, False",
				"said, 1141, False",
			};

			//Act
			var actualOutput = compareTheWords.Compare(input);
			var actualOutputTop5 = actualOutput.Take(5).ToList();

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutputTop5);
		}

		[Test]
		//Another integration test
		public void GetResults_ShouldReturnCorrectCount_WhenPassedThePlacesYoullGo()
		{
			//Arrange
			IFileReader fileReader = new FileReader();
			IWordCounter wordCounter = new WordCounter();
			IPrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
			IOutputGenerator outputGenerator = new OutputGenerator();
			var compareTheWords = new CompareTheWords(fileReader, wordCounter, primeNumberCalculator, outputGenerator);
			var input = "ThePlacesYou'llGo.txt";
			var expectedOutput = new List<string>() //Used http://www.writewords.org.uk/word_count.asp to verify the results
			{
				"you, 22, False",
				"and, 16, False",
				"youll, 15, False",
				"your, 13, True",
				"to, 10, False",
				"the, 9, False",
				"go, 8, False",
				"be, 6, False",
				"of, 6, False",
				"great, 5, True"
			};

			//Act
			var actualOutput = compareTheWords.Compare(input);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput.Take(10));
		}
	}
}
