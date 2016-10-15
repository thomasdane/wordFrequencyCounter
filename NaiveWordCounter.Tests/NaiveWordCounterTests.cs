﻿using System.Collections.Generic;
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
		public void GetResults_ShouldReturnDictionary_WhenPassedWarAndPeace()
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
		//Another integration test
		public void GetResults_ShouldReturnCorrectCount_WhenPassedThePlacesYoullGo()
		{
			//Arrange
			var expectedOutputTopTen = new Dictionary<string, int>() { 
				//I used http://www.writewords.org.uk/word_count.asp to verify the results
				{"youll", 15},
				{"go", 8},
				{"youre", 5},
				{"great", 5},
				{"dont", 4},
				{"know", 3},
				{"happen", 3},
				{"off", 3},
				{"down", 3},
				{"head", 3}
			};
			
			var runner = new Runner();
			var input = "ThePlacesYou'llGo.txt";

			//Act
			var actualOutput = runner.GetResults(input);
			var actualOutputTopTen = actualOutput.Take(10).OrderByDescending(v => v.Value);

			//Assert
			CollectionAssert.AreEquivalent(expectedOutputTopTen, actualOutputTopTen);

		}

		[Test]
		public void GetTotal_ShouldReturnSummedDictionary_WhenPassedRawResults()
		{
			//I realised I was not summing the dictionary.
			//The same word was counted twice if it was in different lines
			var expectedOutput = new Dictionary<string, int>(){
				{"hello", 3},
				{"world", 4},
				{"meerkat", 1}
			};

			var wordCounter = new WordCounter();
			var input = new string[3] { "hello world world", "hello hello world world world", "meerkat world" };

			//Act
			var actualOutput = wordCounter.GetTotal(input);

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
	}
}
