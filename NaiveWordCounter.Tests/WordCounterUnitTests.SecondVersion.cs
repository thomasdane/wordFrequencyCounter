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
	}
}
