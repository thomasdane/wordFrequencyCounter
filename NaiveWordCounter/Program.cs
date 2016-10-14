﻿using System;
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
		
		}
	}

	public interface IWordCounter
	{
		int Count(string lineOfText);
	}

	public class WordCounter : IWordCounter
	{
		public Dictionary<string, int> result;
		
		public int Count(string lineOfText)
		{
			return 10;
		}
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

