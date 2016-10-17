using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NaiveWordCounter.Tests
{
	[TestFixture]
    public class NaiveWordCounterUnitTests
    {
		//I am going to be committing more than usual here. I want to show thought process. 
		//At work, I would prefer to rebase into smaller clean commits before merging to master. 

		//Normally each class would have its own test file, but for ease of review I'll keep them all here.
 		//FileReader Tests
		[Test]
		public void ReadTextFile_ShouldReturnText_WhenProvidedTxtFile()
		{
			//Arrange
			var fileHandler = new FileReader();
			var input = "SingleSentence.txt";
			var expectedOutput = "The quick brown fox";

			//Act
			var actualOutput = fileHandler.ReadTextFile(input);
			var actualOutputString = actualOutput.First();

			//Assert
			Assert.AreEqual(expectedOutput, actualOutputString);
		}

		[Test]
		public void ReadTextFile_ShouldReturnListOfLines_WhenProvidedLongerTextFile()
		{
			//Arrange
			var fileHandler = new FileReader();
			var input = "42LinesOfText.txt";
			var expectedOutput = new string[42]; 
			var firstLine = "Lorem ipsum dolor sit amet";
			var lastLine = "deserunt mollit anim id est laborum";

			//Act
			var actualOutput = fileHandler.ReadTextFile(input);

			//Assert
			Assert.AreEqual(expectedOutput.Length, actualOutput.Length);
			Assert.AreEqual(firstLine, actualOutput.First());
			Assert.AreEqual(lastLine, actualOutput.Last());
		}

		[Test]
		public void ReadTextFile_ShouldReturnCorrectArray_WhenProvidedVerySmallTextFile()
		{
			//Arrange
			const string fileName = "VerySmallBook.txt";
			const string firstLine = "hi";
			const string lastLine = "hi";
			var fileHandler = new FileReader();

			//Act
			var actualOutput = fileHandler.ReadTextFile(fileName);

			//Assert
			Assert.AreEqual(firstLine, actualOutput.First());
			Assert.AreEqual(lastLine, actualOutput.Last());
		}

		//WordCounter Tests
		[Test]
		public void CountWords_ShouldReturnDictionaryOfCorrectNumbers_WhenPassedStringArray()
		{
			//Arrange
			var wordCounter = new WordCounter();
			var input = new [] { "hello world world", "hello world world" };			
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
			var expectedOutput = new Dictionary<string, int>{
				{"hello", 2},
				{"world", 4}
			};
			var wordCounter = new WordCounter();
			var input = new [] { "Hello worlD & World 1", "hello WORLD, world 1" };

			//Act
			var actualOutput = wordCounter.Count(input);

			//Arrange
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}

		[Test]
		public void WordCounter_ShouldIgnorePunctuationAndNumbers_WhenTextInOtherLanguages()
		{
			//Arrange
			var wordCounter = new WordCounter();
			var input = new [] { "«здравствулте мир»", "「你好世界」。"}; //'Hello World' in Chinese and Russian, with native punctuation
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
			var wordCounter = new WordCounter();
			var input = new []{
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

		//Prime Number Calculator Tests
		[Test]
		public void GetDinstinctIntegers_ShouldReturnDistinctValues_WhenPassedDictionary()
		{
			//Arrange
			var primeNumberCalculator = new PrimeNumberCalculator();
			var input = new Dictionary<string, int>
			{
				{"foo", 1},
				{"bar", 1},
				{"meerkat", 2},
				{"ivan", 3},
				{"sergei", 3}
			};
			var expectedOutput = new List<int>{ 1,2,3 };		

			//Act
			var actualOutput = primeNumberCalculator.GetDistinctIntegers(input);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}

		public void GetDinstinctIntegers_ShouldReturnUniqueList_WhenPassedDuplicates()
		{
			//Arrange
			var primeNumberCalculator = new PrimeNumberCalculator();
			var input = new Dictionary<string, int> { 
				{"you", 22},
				{"to", 10},
				{"fro", 10},
				{"to", 10},
				{"be", 7},
				{"to", 7},
			};
			var expectedOutput = new List<int> { 12, 10, 7 };
			
			//Act
			var actualOutput = primeNumberCalculator.GetDistinctIntegers(input);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}

		[Test]
		public void GetListOfPrimes_ShouldReturnCorrectBooleans_WhenPassedListOfIntegers()
		{
			//Arrange
			var input = new List<int> { 1, 2, 97, 98 };
			var primeNumberCalculator = new PrimeNumberCalculator();			
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
		public void IsPrime_ShouldReturnTrue_WhenPassedKnownPrimes(
			[Values(2, 3, 5, 47, 409, 4003)] int input)
		{
			//Arrange
			var primeNumberCalculator = new PrimeNumberCalculator();
			var expectedOutput = true;

			//Act
			var actualOutput = primeNumberCalculator.IsPrime(input);

			//Assert
			Assert.AreEqual(expectedOutput, actualOutput);
		}

		[Test]
		public void IsPrime_ShouldReturnFalse_WhenPassedKnownNonPrimes(
			[Values(1, 4, 9, 16, 50, 1738, 4004)] int input)
		{
			//Arrange
			var primeNumberCalculator = new PrimeNumberCalculator();
			var expectedOutput = false;

			//Act
			var actualOutput = primeNumberCalculator.IsPrime(input);

			//Assert
			Assert.AreEqual(expectedOutput, actualOutput);
		}

		[Test]
		public void GetPrimes_ShouldReturnCorrectPrimes_WhenPassedValidWordCount()
		{
			//Arrange
			var primeNumberCalculator = new PrimeNumberCalculator();
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

		//Output Generator tests
		[Test]
		public void OutputGenerator_ShouldGenerateListOfStrings_WhenPassedWordCountAndPrimes()
		{
			//Arrange
			var outputGenerator = new OutputGenerator();
			var wordCountResults = new Dictionary<string, int> { 
				{"compare", 22},
				{"the", 13},
				{"market", 10},
				{"codeTest", 7}
			};
			var listOfPrimes = new Dictionary<int, bool> {
				{13, true},
				{7, true},
				{5, true}
			};
			var expectedOutput = new List<string>
			{
				"compare, 22, False", 
				"the, 13, True",
				"market, 10, False", 
				"codeTest, 7, True"
			};

			//Act
			var actualOutput = outputGenerator.GenerateOutput(wordCountResults, listOfPrimes);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}
	}
}
