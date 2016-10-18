using System;
using System.IO;
using WordFrequencyCounter.Tests.Unit.Interfaces;

namespace WordFrequencyCounter.Tests.Unit
{
	public class FileReader : IFileReader
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
