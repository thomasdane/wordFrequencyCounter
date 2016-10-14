Notes

Optimizations and scaling

1. What if we wanted to process multiple books at once? Could use async or multithreading. 
2. Do we need to load entire book into memory? Could we parse it bit by bit? Discussion below. 

Discussion: how to quickly read a large text file 

One way to speed this up would be to NOT parse the book sequentially. What if instead, we load each line/page into an array and then process them in parallel. 
At the end of each processing, we have a total for the line. We can then add all the totals together. This idea came from http://cc.davelozinski.com/c-sharp/the-fastest-way-to-read-and-process-text-files
The article also shows that pre-allocating the size of the array has speed improvements. For simplicity now, I will not do that but can return to it later. It seemed to depend not on the size of the file, but on the length of the line. 
I do not expect many lines in a book to be 25 GUIDs long like the author of that article. 

Here is the code:

var allLines = new string[MAX]; //only allocate memory here
			allLines = File.ReadAllLines(fileName);
			Parallel.For(0, allLines.Length, x =>
			{
				TestReadingAndProcessingLinesFromFile_DoStuff(AllLines[x]);
			});

I will write just the minimum code now though to get my test to pass. 
