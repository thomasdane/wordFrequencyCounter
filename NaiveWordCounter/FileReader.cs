using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NaiveWordCounter
{
	public class FileReader
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
