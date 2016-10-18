using System.Collections.Generic;

namespace WordFrequencyCounter.Tests.Unit.Interfaces
{
	public interface IWordCounter
	{
		IDictionary<string, int> Count(string[] linesOfText);
	}
}
