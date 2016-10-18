using System.Collections.Generic;

namespace WordFrequencyCounter.Interfaces
{
	public interface IWordCounter
	{
		IDictionary<string, int> Count(string[] linesOfText);
	}
}
