Readme


Requirements

1. Write an application that outputs the individual words that appear in a book, and how many times that word appears in the text file.

2. The second part is to also output whether the number of times each word appears is a prime number.

�

The following assumptions can be made:

-����������Ignore punctuation and capitalisation

-����������The code should compile and run on a machine with VS/Xamarin and NUnit


It would be beneficial to:

-come up with more than one solution and be able to talk about the pros and cons to each

-ensure the application scales and performs optimally

-Use TDD in the approach to writing the application

[DRAFT]

Optimizations and scaling


- Do we need to load entire book into memory? Could we parse it bit by bit? Discussion below. 
- What if the book is in another language? I added unicode support and tests for non-latin alphabets. 
- It is slower to output only the numbers in the list. Then you could use an Array or List where the index is the number and value is the boolean, like IsPrime = bool[]
- so IsPrime[6] would be true, isPrime[7] would be false. It's slightly confusing because of zero indexed arrays.
- what if someone enters a book that we have already computed? It would be good to save results to a database, 
and when someone enters a book name, we can first lookup the database to check if we have the results saved.  

Discussion: how to quickly read a large text file 

One way to speed this up would be to NOT parse the book sequentially. What if instead, we load each line/page into an array and then process them in parallel. 
At the end of each processing, we have a total for the line. We can then add all the totals together. This idea came from http://cc.davelozinski.com/c-sharp/the-fastest-way-to-read-and-process-text-files
The article also shows that pre-allocating the size of the array has speed improvements. For simplicity now, I will not do that but can return to it later. It seemed to depend not on the size of the file, but on the length of the line. 
I do not expect many lines in a book to be 25 GUIDs long like the author of that article. 

Parellism

- I have not done much multithreading or parallel stuff before, beyond a 'hello world' out of curiosity. I wanted to dive deeper so read http://www.i-programmer.info/programming/c/1420-the-perils-of-the-c-parallel-for-.html

PRIMES

I did a lot of research into the Sieve of Eratosthenes vs Sieve of Atkin, 
before I realised I did not have to GENERATE primes! Only determine if an integer IS prime. 

I wrote a very simple (slow) prime calculator. The worst part about it is it does not skip numbers
that are multiples of numbers already tested. For example, once we know that the integer is not divisible by 5
we do not need to check 10, 15, 20 and so on. There are lots of ways to find primes fast, as discussed here: http://stackoverflow.com/questions/15743192/check-if-number-is-prime-number

- My algorithm to find primes is O(n**2) which is really bad. It has a for loop inside a for loop. 

Another way might be to hardcode the list of primes and then check against them: 
https://www.dotnetperls.com/prime

The longest novel ever written (Zettels Traum) is estimated at 1,100,000 words (source https://en.wikipedia.org/wiki/List_of_longest_novels). Let's assume the wost case where we had a book
that was longer, and composed of only a single word. So 1,100,001 'meerkats' in a book. 

Notes

- there is a 2 GB limit on any object in .Net. I won't be able to handle lines longer than that. 
- Concurrent Dictionary is a thread-safe dictionary http://geekswithblogs.net/blackrabbitcoder/archive/2011/02/17/c.net-little-wonders-the-concurrentdictionary.aspx
- article about how string.split could create a large number of string objects if the line is long, and perhaps parsing by chars is faster http://stackoverflow.com/questions/8784517/counting-number-of-words-in-c-sharp
- i need to benchmark this solution and my next one and compare them
- using 
http://www.writewords.org.uk/word_count.asp my results were different from Railway Children. Without seeing their code, it's hard to judge why. 
However, both that site and myself get different results to http://www.textfixer.com/tools/online-word-counter.php#newText2. 
It seems there is some variation in counting words. 

Limitations and Extensions
- What if we wanted to process multiple books at once? Could use more multithreading?
- The brute force solution to that problem would be to spin this up on multiple instances and process 1 book per instance
- Could run the .exe of AWS Lambda and have it run every time a new book is uploaded to S3. 
- words with hyphens will have issues. they will be counted correctly, but display incorrectly. for example,
free-for-all will be displayed as freeforall. 
- I noticed online that some word frequency counters allow the user to exclude common words like 'the', 'to', 'and' etc. That would be a nice feature. 
If the business/customer wanted it in the case of this application, it would be easy to create a list of such common words and exclude them from the results. 
It may even speed up the app by excluding the most common results. 

Extensions

- testing for more edge cases and bad inputs. 

Long Books/Text Files

- I included War and Peace as an example, but it is not the longest novel ever written. https://en.wikipedia.org/wiki/List_of_longest_novels
So I copy and pasted the book twice into the one file, producing a book far longer than the longest ever book. 


Why do we only need to test up to the square root of a number to determine if it is prime? 

This confused me for a while, so I'll include the explanation that helped. 
http://stackoverflow.com/questions/5811151/why-do-we-check-upto-the-square-root-of-a-prime-number-to-determine-if-it-is-pri

My explanation: 

A prime number has to be divisible by something other than itself and 1. 

Let's say a x b = 100. This could be 10x10, or 50x2, or 5x20. 

If a == b, then they are equal, and are the square root of 100. a = 10, b = 10. 

If one of them is less than 10, the other HAS to be greater. For example, 5 x 20 == 100. And if one of them is bigger, 
the other must also get smaller to compensate. For example, 50 x 2. 

So imagine the number 101. The square root of 101 is about 10.04

Say we divide 101 by every integer up to 10.04. 101/2, 101/3, 101/4 ... 101/10. We find no cases where the remainder is 0. 

Now we also know that there is no number LARGER than 10 that divides into 101. Because any number larger than the square root
would have a partner/mirror number smaller than the square root. 

If the smaller factor doesn't exist, there is no matching larger factor. 

So we don't have to test past 10.04.