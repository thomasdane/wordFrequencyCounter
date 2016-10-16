using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace NaiveWordCounter
{
	public class WordCounter
	{
		public IDictionary<string, int> Count(string[] linesOfText)
		{
			var result = new ConcurrentDictionary<string, int>();
			var regex = new Regex("[^\\p{L}]"); //regex to find unicode letters in latin and non-latin text

			Parallel.For(0, linesOfText.Length, x =>
			{
				var line = linesOfText[x];
				var lineChunks = line.Split(' ');

				foreach (var chunk in lineChunks)
				{
					var word = regex.Replace(chunk, "").ToLower();

					if (!string.IsNullOrEmpty(word))
					{
						result.AddOrUpdate(word, 1, (key, value) => value + 1);
						//AddOrUpdate: 
						//Add the word (first param) with a value of 1 (second param) if it does NOT exist
						//Otherwise, add it as a new key, and add 1 to its value (third param)
						//Good explanation here https://www.dotnetperls.com/concurrentdictionary
					}
				}
			});

			var orderedResult = result.OrderByDescending(x => x.Value).ToDictionary(i => i.Key, i => i.Value);
			return orderedResult;
		}
	}
}
