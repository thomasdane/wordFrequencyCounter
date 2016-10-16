using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NaiveWordCounter.Tests
{
	[TestFixture]
    public class NaiveWordCounterTests
    {
		//I am going to be committing more than usual here. I want to show thought process. At work,
		//I would prefer to rebase all of this into a single clean commit before merging to master. 

		[Test]
		public void ReadTextFile_ShouldReturnText_WhenProvidedTxtFile()
		{
			//Arrange
			var expectedOutput = "The quick brown fox";
			var fileName = "SingleSentence.txt"; //TODO: rename this. 
			var fileHandler = new FileReader();

			//Act
			var actualOutput = fileHandler.ReadTextFile(fileName);  

			//Assert
			Assert.AreEqual(expectedOutput, actualOutput.First().ToString()); //as this text file has only one line, this will suffice for now
		}

		[Test]
		public void ReadTextFile_ShouldReturnListOfLines_WhenProvidedLongerTextFile()
		{
			//Arrange
			var fileName = "42LinesOfText.txt";
			var expectedOutput = new string[42]; //I will have to develop a solution for very large books. 
			var firstLine = "Lorem ipsum dolor sit amet";
			var lastLine = "deserunt mollit anim id est laborum";
			var fileHandler = new FileReader();

			//Act
			var actualOutput = fileHandler.ReadTextFile(fileName);

			//Assert
			Assert.AreEqual(expectedOutput.Length, actualOutput.Length);
			Assert.AreEqual(firstLine, actualOutput.First().ToString());
			Assert.AreEqual(lastLine, actualOutput.Last().ToString());
		}

		[Test]
		public void ReadTextFile_ShouldReturnCorrectArray_WhenProvidedVerySmallTextFile()
		{
			//Arrange
			var fileName = "VerySmallBook.txt";
			var expectedOutput = new string[1]; //I will have to develop a solution for very large books. 
			var firstLine = "hi";
			var lastLine = "hi";
			var fileHandler = new FileReader();

			//Act
			var actualOutput = fileHandler.ReadTextFile(fileName);

			//Assert
			Assert.AreEqual(expectedOutput.Length, actualOutput.Length);
			Assert.AreEqual(firstLine, actualOutput.First().ToString());
			Assert.AreEqual(lastLine, actualOutput.Last().ToString());
		}

		[Test]
		public void CountWords_ShouldReturnDictionaryOfCorrectNumbers_WhenPassedString()
		{
			//Arrange
			var expectedOutput = new Dictionary<string, int>(){
				{"hello", 2},
				{"world", 4}
			};
			var wordCounter = new WordCounter();
			var input = new string[2] { "hello world world", "hello world world" };

			//Act
			var actualOutput = wordCounter.Count(input);

			//Arrange
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}

		[Test]
		public void WordCounter_ShouldIgnorePunctuationAndNumbers_WhenPassedEnglishText()
		{
			//Arrange
			var expectedOutput = new Dictionary<string, int>(){
				{"hello", 2},
				{"world", 4}
			};
			var wordCounter = new WordCounter();
			var input = new string[2] { "Hello worlD & World 1", "hello WORLD, world 1" };

			//Act
			var actualOutput = wordCounter.Count(input);

			//Arrange
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}

		[Test]
		public void WordCounter_ShouldIgnorePunctuationAndNumbers_WhenTextInOtherLanguages()
		{
			//Arrange
			var expectedOutput = new Dictionary<string, int>(){
				{"你好世界", 1},
				{"здравствулте", 1},
				{"мир", 1}
			};
			var wordCounter = new WordCounter();
			var input = new string[2] { "「 你好世界 」。", "«здравствулте мир»" }; //Chinese and Russian with punctuation

			//Act
			var actualOutput = wordCounter.Count(input);

			//Dictionaries are unordered. This can be solved by using a SortedDictionary
			//My tests above rely on order, so might have problems. Jon Skeet discusses ordering here http://stackoverflow.com/questions/6384710/why-is-a-dictionary-not-ordered
			var actualOutputSorted = new SortedDictionary<string, int>(actualOutput);
			var expectedOutputSorted = new SortedDictionary<string, int>(expectedOutput);
			
			//Cannot simply compare two unicode strings. They need to be escaped. 
			//More information: http://stackoverflow.com/questions/9461971/nunit-how-to-compare-strings-containing-composite-unicode-characters
			var ExpectedChineseHelloWorld = System.Uri.UnescapeDataString(expectedOutputSorted.Keys.Last().ToString());
			var ActualChineseHelloWorld = System.Uri.UnescapeDataString(actualOutputSorted.Keys.Last().ToString());
			var ExpectedRussianHelloWorld = System.Uri.UnescapeDataString(expectedOutputSorted.Keys.First().ToString());
			var ActualRussianHelloWorld = System.Uri.UnescapeDataString(actualOutputSorted.Keys.First().ToString());
			
			//Assert
			Assert.AreEqual(ExpectedChineseHelloWorld, ActualChineseHelloWorld);
			Assert.AreEqual(ExpectedRussianHelloWorld, ActualRussianHelloWorld);
		}

		[Test]
		public void GetDistinctValues_ShouldReturnDistinctValues_WhenPassedDictionary()
		{
			var expectedOutput = new List<int>(){ 1,2,3 };
			//when to use IDictionary vs Dictionary
			var input = new Dictionary<string, int>()
			{
				{"foo", 1},
				{"bar", 1},
				{"meerkat", 2},
				{"ivan", 3},
				{"sergei", 3}
			};
			var primeNumberCalculator = new PrimeNumberCalculator();

			//Act
			var actualOutput = primeNumberCalculator.GetDistinctIntegers(input);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}

		[Test]
		public void GetListOfPrimes_ShouldReturnBoolean_WhenPassedListOfIntegers()
		{
			//Arrange
			var input = new List<int>() { 1, 2, 97, 98 };
			var primeNumberCalculator = new PrimeNumberCalculator();			
			var expectedOutput = new Dictionary<int, bool>()
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
		public void OutputGenerator_ShouldGenerateListOfStrings_WhenPassedWordCountResultsAndPrimes()
		{
			var wordCountResults = new Dictionary<string, int>() { 
				{"compare", 22},
				{"the", 13},
				{"market", 10},
				{"codeTest", 7},
			};

			var listOfPrimes = new Dictionary<int, bool>(){
				{13, true},
				{7, true},
				{5, true}
			};

			var expectedOutput = new List<string>
			{
				"compare, 22, False", 
				"the, 13, True",
				"market, 10, False", 
				"codeTest, 7, True",
			};

			var outputGenerator = new OutputGenerator();
			var actualOutput = outputGenerator.GenerateOutput(wordCountResults, listOfPrimes);

			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}
	}
}
