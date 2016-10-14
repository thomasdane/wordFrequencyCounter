using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace NaiveWordCounter
{
	class Program
	{
		static void Main(string[] args)
		{

		}
	}

	public class Runner
	{
		public IDictionary<string, int> GetResults(string fileName)
		{
			var fileHandler = new FileHandler();
			var content = fileHandler.ReadTextFile(fileName);

			var wordCounter = new WordCounter();
			var result = wordCounter.Count(content);

			return result;
		}
	}

	public interface IWordCounter
	{
		IDictionary<string, int> Count(string[] linesOfText);
	}

	public class WordCounter : IWordCounter
	{
		public IDictionary<string, int> Count(string[] linesOfText)
		{
			var result = new ConcurrentDictionary<string, int>();

			Parallel.For(0, linesOfText.Length, x =>
			{
				var line = linesOfText[x];
				var words = line.Split(' ');

				foreach (var word in words)
				{
					//Add the word with a value of 1 if it does NOT exist
					//Otherwise, add it as a new key, and add 1 to its value
					//Good explanation here https://www.dotnetperls.com/concurrentdictionary
					result.AddOrUpdate(word, 1, (key, value) => value + 1);
				}
			});

			return result;
		}
	}

	public class FileHandler
	{
		public string[] ReadTextFile(string fileName)
		{
			var bookFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Books"));
			var filePath = Path.Combine(bookFolder, fileName);
			var content = File.ReadAllLines(filePath);
			return content;
		}
	}
}

