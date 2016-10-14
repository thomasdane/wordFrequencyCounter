Readme

Optimizations and scaling


2. Do we need to load entire book into memory? Could we parse it bit by bit? Discussion below. 

Discussion: how to quickly read a large text file 

One way to speed this up would be to NOT parse the book sequentially. What if instead, we load each line/page into an array and then process them in parallel. 
At the end of each processing, we have a total for the line. We can then add all the totals together. This idea came from http://cc.davelozinski.com/c-sharp/the-fastest-way-to-read-and-process-text-files
The article also shows that pre-allocating the size of the array has speed improvements. For simplicity now, I will not do that but can return to it later. It seemed to depend not on the size of the file, but on the length of the line. 
I do not expect many lines in a book to be 25 GUIDs long like the author of that article. 

Parellism

- I have not done much multithreading or parallel stuff before, beyong a 'hello world' out of curiosity. I wanted to dive deeper so read http://www.i-programmer.info/programming/c/1420-the-perils-of-the-c-parallel-for-.html
- I don't think I will have race conditions. 

Notes

- there is a 2 GB limit on any object in .Net. I won't be able to handle lines longer than that. 
- this post talks about a concurrent dictionary which i might explore http://codereview.stackexchange.com/questions/90925/word-frequency-in-a-large-text-file
- article about how string.split could create a large number of string objects if the line is long, and perhaps parsing by chars is faster http://stackoverflow.com/questions/8784517/counting-number-of-words-in-c-sharp
- i need to benchmark this solution and my next one and compare them

Limitations and Extensions
- What if we wanted to process multiple books at once? Could use more multithreading?
- The brute force solution to that problem would be to spin this up on multiple instances and process 1 book per instance