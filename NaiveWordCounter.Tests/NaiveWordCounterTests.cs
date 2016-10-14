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
			var input = "hello world world hello world world";

			//Act
			var actualOutput = wordCounter.Count(input);

			//Arrange
			CollectionAssert.AreEquivalent(expectedOutput, actualOutput);

		}
	}
}
