namespace WordFrequencyCounter.Tests.Unit.Interfaces
{
	public interface IFileReader
	{
		string[] ReadTextFile(string fileName);
	}
}
