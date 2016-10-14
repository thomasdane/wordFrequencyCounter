using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NaiveWordCounter.Tests
{
	[TestFixture]
    public class NaiveWordCounterTests
    {
		[Test]
		public void HelloNunit()
		{
			Assert.Pass();
		}

		//I am going to be committing more than usual here. I want to show thought process. In production or at work,
		//I would prefer to rebease all of this into a single clean commit before merging to master. 

		[Test]
		public void ReadTextFile_ShouldReturnText_WhenProvidedTxtFile()
		{
			//Arrange
			var expectedOutput = "The quick brown fox";
			var fileName = "TestInput.txt"; //TODO: rename this. 
			var fileHandler = new FileHandler();

			//Act
			var actualOutput = fileHandler.ReadTextFile(fileName); //I may have to address porblems loading extremely long books into memory like this. 

			//Assert
			Assert.AreEqual(expectedOutput, actualOutput);
		}

		[Test]
		public void ReadTextFile_ShouldReturnArrayOfLines_WhenProvidedLongerTextFile()
		{
			//Arrange
			var expectedOutput = new string[42];
			var fileName = "42LinesOfText.txt";
			var fileHandler = new FileHandler();

			//Act
			var actualOutput = fileHandler.ReadTextFile(fileName);

			//Assert
			Assert.AreEqual(expectedOutput.Length, actualOutput.Length);
		}
    }
}
