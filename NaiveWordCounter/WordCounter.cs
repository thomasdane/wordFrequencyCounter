using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using NaiveWordCounter.Interfaces;

namespace NaiveWordCounter
{
	public class WordCounter : IWordCounter
	{
		public IDictionary<string, int> Count(string[] linesOfText)
		{
			var result = new ConcurrentDictionary<string, int>();
			var regex = new Regex("[^\\p{L}]"); //matches all unicode letters (latin and non-latin)

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
						//Add 'word' (first parameter) with a value of 1 (second parameter) if it does NOT exist
						//Otherwise, add it as a new key, and add 1 to its value (third param)
						//Explanation here https://www.dotnetperls.com/concurrentdictionary
					}
				}
			});

			var orderedResult = result.OrderByDescending(x => x.Value).ThenBy(x => x.Key).ToDictionary(i => i.Key, i => i.Value);
			return orderedResult;
		}
	}
}
