using System.Collections.Generic;

namespace NaiveWordCounter.Interfaces
{
	public interface IWordCounter
	{
		IDictionary<string, int> Count(string[] linesOfText);
	}
}
