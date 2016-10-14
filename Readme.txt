Readme

Optimizations and scaling

1. What if we wanted to process multiple books at once? Could use async or multithreading. 
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
