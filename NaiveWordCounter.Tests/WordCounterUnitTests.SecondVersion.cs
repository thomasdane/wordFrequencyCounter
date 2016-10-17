using System.Collections.Generic;
using System.Linq;
using NaiveWordCounter.SecondVersion;
using NUnit.Framework;

namespace NaiveWordCounter.Tests
{
	[TestFixture]
	public class NaiveWordCounterUnitTestsSecondVersion
	{
		//PrimeNumberCalculatorHardCoded Tests
		[Test]
		public void GetListOfPrimes_ShouldReturnCorrectResults_WhenUsingHardCodedPrimes()
		{
			//Arrange
			var primeNumberCalculator = new PrimeNumberCalculator_HardCoded();
			var input = new List<int> { 1, 2, 97, 98 };
			var expectedOutput = new Dictionary<int, bool>
			{
				{2, true},
				{97, true}
			};

			//Act
			var actualOutput = primeNumberCalculator.GetListOfPrimes(input);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}

		[Test]
		public void GetPrimes_ShouldReturnCorrectPrimes_WhenUsingHardCodedPrimes()
		{
			//Arrange
			var primeNumberCalculator = new PrimeNumberCalculator_HardCoded();
			var input = new Dictionary<string, int> { 
				{"you", 22},
				{"your", 13},
				{"to", 10},
				{"be", 7},
				{"of", 6},
				{"great", 5}
			};
			var expectedOutput = new Dictionary<int, bool> {
				{13, true},
				{7, true},
				{5, true}
			};

			//Act
			var actualOutput = primeNumberCalculator.CalculatePrimes(input);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}

		//WordCounterWithoutRegex Tests
		[Test]
		public void CountWords_ShouldReturnDictionaryOfCorrectNumbers_WhenPassedStringArray()
		{
			//Arrange
			var wordCounter = new WordCounterWithoutRegex();
			var input = new[] { "hello world world", "hello world world" };
			var expectedOutput = new Dictionary<string, int>{
				{"hello", 2},
				{"world", 4}
			};

			//Act
			var actualOutput = wordCounter.Count(input);

			//Arrange
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}

		[Test]
		public void CountWords_ShouldIgnorePunctuationAndNumbers_WhenPassedEnglishText()
		{
			//Arrange
			var wordCounter = new WordCounterWithoutRegex();
			var input = new[] { "Hello worlD & World 1", "hello WORLD, world 1" };
			var expectedOutput = new Dictionary<string, int>{
				{"hello", 2},
				{"world", 4}
			};

			//Act
			var actualOutput = wordCounter.Count(input);

			//Arrange
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}

		[Test]
		public void WordCounter_ShouldIgnorePunctuationAndNumbers_WhenTextInOtherLanguages()
		{
			//Arrange
			var wordCounter = new WordCounterWithoutRegex();
			var input = new[] { "«здравствулте мир»", "「你好世界」。" }; //'Hello World' in Chinese and Russian, with native punctuation
			var expectedOutput = new Dictionary<string, int>{
				{"здравствулте", 1},
				{"мир", 1},
				{"你好世界", 1}
			};

			//Act
			var actualOutput = wordCounter.Count(input);
			//One does not simply compare two unicode strings. They need to be escaped. 
			//More information: http://stackoverflow.com/questions/9461971/nunit-how-to-compare-strings-containing-composite-unicode-characters
			var ActualChineseHelloWorld = System.Uri.UnescapeDataString(actualOutput.Keys.Last());
			var ActualRussianHelloWorld = System.Uri.UnescapeDataString(actualOutput.Keys.First());
			var ExpectedChineseHelloWorld = System.Uri.UnescapeDataString(expectedOutput.Keys.Last());
			var ExpectedRussianHelloWorld = System.Uri.UnescapeDataString(expectedOutput.Keys.First());

			//Assert
			Assert.AreEqual(ExpectedChineseHelloWorld, ActualChineseHelloWorld);
			Assert.AreEqual(ExpectedRussianHelloWorld, ActualRussianHelloWorld);
		}

		[Test]
		public void WordCounter_ShouldReturnCorrectCount_WhenPassedSentencesWithFullStops()
		{
			//Regression test to address issue with full stops. 
			//Arrange
			var wordCounter = new WordCounterWithoutRegex();
			var input = new[]{
				"hello world!",
				"hello world.",
				"Let's test."
			};
			var expectedOutput = new Dictionary<string, int> { 
				{"hello", 2},
				{"world", 2},
				{"lets", 1},
				{"test", 1}
			};

			//Act
			var actualOutput = wordCounter.Count(input);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}
	}
}
