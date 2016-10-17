using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveWordCounter.Interfaces;

namespace NaiveWordCounter.SecondVersion
{
	public class WordCounterWithoutRegex : IWordCounter
	{
		public IDictionary<string, int> Count(string[] linesOfText)
		{
			var result = new ConcurrentDictionary<string, int>();
			//var regex = new Regex("[^\\p{L}]"); //matches all unicode letters (latin and non-latin)

			Parallel.For(0, linesOfText.Length, x =>
			{
				var line = linesOfText[x];
				var lineChunks = line.Split(' ');

				foreach (var chunk in lineChunks)
				{
					var stringBuilder = new StringBuilder();

					foreach (var item in chunk)
					{
						if (char.IsLetter(item))
						{
							stringBuilder.Append(item);
						}
					}

					var word = stringBuilder.ToString().ToLower();

					if (!string.IsNullOrEmpty(word.ToString()))
					{
						result.AddOrUpdate(word, 1, (key, value) => value + 1);
					}
				}
			});

			var orderedResult = result.OrderByDescending(x => x.Value).ThenBy(x => x.Key).ToDictionary(i => i.Key, i => i.Value);
			return orderedResult;
		}
	}
}
