using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveWordCounter.Interfaces
{
	public interface IWordCounter
	{
		IDictionary<string, int> Count(string[] linesOfText);
	}
}
