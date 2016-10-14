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
			var text = "the quick brown fox";

			//Act
			var result = ReadTextFile(pathToFile); //I may have to address porblems loading extremely long books into memory like this. 

			Assert.AreEqual(text, result);
		}
    }
}
