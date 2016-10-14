using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NaiveWordCounter
{
	class Program
	{
		static void Main(string[] args)
		{

			var file = "TestInput.txt";
			var fileHandler = new FileHandler();
			var content = fileHandler.ReadTextFile(file);
			//now i want to get a dictionary of words and frequencies
			//var result = getWordCount

		}
	}

	public interface IWordCounter
	{
		IDictionary<string, int> Count(string lineOfText);
	}

	public class TextProcessor
	{
		public void ParallelProcess(string[] content)
		{
			var wordCounter = new WordCounter();

			Parallel.For(0, content.Length, x =>
			{
				wordCounter.Count(content[x]);
			});
		}
	}

	//you have a dictionary
	// you loo

	public class WordCounter : IWordCounter
	{
		private static readonly char[] separators = { ' ' };

		public IDictionary<string, int> Count(string lineOfText)
		{
			
			var words = lineOfText.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			var wordCount = new Dictionary<string, int>();

			foreach (var word in words)
			{
				if (wordCount.ContainsKey(word))
				{
					wordCount[word] = wordCount[word] + 1;
				}
				else
				{
					wordCount.Add(word, 1);
				}
			}

			return wordCount;
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

