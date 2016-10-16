using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NaiveWordCounter.Tests
{
	[TestFixture]
    public class NaiveWordCounterTests
    {
		//I am going to be committing more than usual here. I want to show thought process. In production or at work,
		//I would prefer to rebease all of this into a single clean commit before merging to master. 

		[Test]
		public void ReadTextFile_ShouldReturnText_WhenProvidedTxtFile()
		{
			//Arrange
			var expectedOutput = "The quick brown fox";
			var fileName = "SingleSentence.txt"; //TODO: rename this. 
			var fileHandler = new FileHandler();

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
			var fileHandler = new FileHandler();

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
			var fileHandler = new FileHandler();

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
		//this is more of an integration test, i will move to another project later.
		public void GetResults_ShouldReturnDictionary_WhenPassedSampleBook_RailwayChildren()
		{
			//Arrange
			var expectedOutput = new Dictionary<string, int>() { };
			var runner = new CompareTheWords();
			var input = "RailwayChildren.txt";

			//Act
			var actualOutput = runner.GetWordCount(input);

			//Assert
			CollectionAssert.IsNotEmpty(actualOutput);
			CollectionAssert.AllItemsAreNotNull(actualOutput);
		}

		[Test]
		public void GetResults_ShouldReturnDictionary_WhenPassedWarAndPeace()
		{
			//Arrange
			var expectedOutput = new Dictionary<string, int>() { };
			var runner = new CompareTheWords();
			var input = "WarAndPeace.txt";

			//Act
			var actualOutput = runner.GetWordCount(input);

			//Assert
			CollectionAssert.IsNotEmpty(actualOutput);
			CollectionAssert.AllItemsAreNotNull(actualOutput);
		}

		[Test]
		//Another integration test
		public void GetResults_ShouldReturnCorrectCount_WhenPassedThePlacesYoullGo()
		{
			//Arrange
			var expectedOutput = new Dictionary<string, int>() { 
				//I used http://www.writewords.org.uk/word_count.asp to verify the results
				{"you", 22},
				{"and", 16},
				{"youll", 15},
				{"your", 13},
				{"to", 10},
				{"the", 9},
				{"go", 8},
				{"be", 6},
				{"of", 6},
				{"great", 5}
			};
			
			var runner = new CompareTheWords();
			var input = "ThePlacesYou'llGo.txt";

			//Act
			var actualOutput = runner.GetWordCount(input);

			//sort first by value, then by key, then take top 10 results
			IDictionary<string, int> actualOutputSorted = actualOutput.OrderByDescending(x => x.Value).ThenBy(x => x.Key).Take(10).ToDictionary(i => i.Key, i => i.Value);
			IDictionary<string, int> expectedOutputSorted = expectedOutput.OrderByDescending(x => x.Value).ThenBy(x => x.Key).ToDictionary(i => i.Key, i => i.Value);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutputSorted, actualOutputSorted);
		}

		[Test]
		//Another integration test
		public void GetResults_ShouldReturnCorrectCount_WhenPassedSentencesWithFullStops()
		{
			//Arrange
			var expectedOutput = new Dictionary<string, int>() { 
				{"hello", 2},
				{"world", 2},
				{"im", 1},
				{"testing", 1}
			};

			var runner = new CompareTheWords();
			var input = "AFewSentences.txt";

			//Act
			var actualOutput = runner.GetWordCount(input);
			var actualOutputSorted = actualOutput.OrderByDescending(x => x.Value);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutputSorted);
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
		//Another integration test
		public void GetPrimes_ShouldReturnCorrectPrimes_WhenPassedThePlacesYoullGo()
		{
			//Arrange
			var input = new Dictionary<string, int>() { 
				{"you", 22},
				{"your", 13},
				{"to", 10},
				{"be", 7},
				{"of", 6},
				{"great", 5}
			};

			var expectedOutput = new Dictionary<int, bool>(){
				{13, true},
				{7, true},
				{5, true}
			};

			var runner = new CompareTheWords();

			//Act
			var actualOutput = runner.GetPrimes(input);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}

		public void GetDinstinctIntegers_ShouldReturnUniqueList_WhenPassedDuplicates()
		{
			//Arrange
			var input = new Dictionary<string, int>() { 
				{"you", 22},
				{"to", 10},
				{"fro", 10},
				{"to", 10},
				{"be", 7},
				{"to", 7},
			};
			var expectedOutput = new List<int>(){ 12, 10, 7};
			var runner = new CompareTheWords();

			//Act
			var actualOutput = runner.GetPrimes(input);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);
		}

		[Test]
		public void CompareTheTextFile_ShouldReturnWordFrequencyAndIsPrime_WhenPassedRailwayChildren()
		{
			var compareTheTextFile = new CompareTheWords();
			var input = "RailwayChildren.txt";
			var expectedOutput = new string[]
			{
				"foo, 10, false", 
				"meerkat, 7, true"
			};

			var actualOutput = compareTheTextFile.Compare(input);

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
