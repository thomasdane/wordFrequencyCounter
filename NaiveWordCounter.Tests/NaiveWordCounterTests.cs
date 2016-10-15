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
			var runner = new Runner();
			var input = "RailwayChildren.txt";

			//Act
			var actualOutput = runner.GetResults(input);

			//Assert
			CollectionAssert.IsNotEmpty(actualOutput);
			CollectionAssert.AllItemsAreNotNull(actualOutput);
		}

		[Test]
		public void GetResults_ShouldReturnDictionary_WhenPassedSampleBook_WarAndPeace()
		{
			//Arrange
			var expectedOutput = new Dictionary<string, int>() { };
			var runner = new Runner();
			var input = "WarAndPeace.txt";

			//Act
			var actualOutput = runner.GetResults(input);

			//Assert
			CollectionAssert.IsNotEmpty(actualOutput);
			CollectionAssert.AllItemsAreNotNull(actualOutput);
		}

		[Test]
		public void WordCounter_ShouldIgnorePunctuationAndNumbers_WhenPassedText()
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

	}
}
