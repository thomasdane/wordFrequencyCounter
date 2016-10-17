#Readme

##Requirements

- Write an application that outputs the individual words that appear in a book, and how many times that word appears in the text file.

- The second part is to also output whether the number of times each word appears is a prime number.


The following assumptions can be made:

- Ignore punctuation and capitalisation

- The code should compile and run on a machine with VS/Xamarin and NUnit


It would be beneficial to:

- come up with more than one solution and be able to talk about the pros and cons to each

- ensure the application scales and performs optimally

- Use TDD in the approach to writing the application


[DRAFT]

##Approach

I have used TDD. 

I thought about how to parse a very large text file quickly, and my research led me to parallelism: http://cc.davelozinski.com/c-sharp/the-fastest-way-to-read-and-process-text-files

Rather than parse the book sequentially, the progream loads each line into an array and acts on them concurrently. 
A class called ConcurrentDictionary allows us to keep track of the totals: http://geekswithblogs.net/blackrabbitcoder/archive/2011/02/17/c.net-little-wonders-the-concurrentdictionary.aspx
- I have not done much multithreading or parallel stuff before, beyond a 'hello world' out of curiosity. 
I wanted to dive deeper so read http://www.i-programmer.info/programming/c/1420-the-perils-of-the-c-parallel-for-.html

I added unicode support and tests for non-latin alphabets. I included War and Peace as an example, but it is not the longest novel ever written.
https://en.wikipedia.org/wiki/List_of_longest_novels
So I coplied War And Peace twice into the one file, producing a book far longer than the longest ever book. 

##Other Solutions with Pros and Cons

In terms of a very different solution, it would be great to try this task with F# and a functional approach. Given time, I would love to try it. 
Nevertheless, along the way there were various places for mini optimizations.

The interfaces allowed me to define second versions of all the methods. 

I put these in a folder called SecondVersion. 

####1. File Reader

One way to speed this up would be to pre-allocate the size of the array. 

####2. Word Counter

String.split could create a large number of string objects if the line is long, and perhaps parsing by chars is faster http://stackoverflow.com/questions/8784517/counting-number-of-words-in-c-sharp

####3. Prime number calculator

I had never dealt with primes before, so started researching the Sieve of Eratosthenes vs Sieve of Atkin. 
Then I realised I did not have to GENERATE primes! Only determine if an integer IS prime. A lesson on reading requirements carefully. 

I initially wrote a very simple (slow) prime calculator. The worst part about it is it does not skip numbers
that are multiples of numbers already tested. For example, once we know that the integer is not divisible by 5
we do not need to check 10, 15, 20 and so on. It also checks all numbers, not stopping at the square root of the number [Discussion][1].
There are lots of ways to find primes fast, as discussed here: http://stackoverflow.com/questions/15743192/check-if-number-is-prime-number

- Here one approach I tried was hardcoding a list of smaller primes and comparing to that array before calculating the prime.  

![Screenshot](ImagesForReadme/HardCodedPrimes.png)


Another way might be to hardcode the list of primes and then check against them: 
https://www.dotnetperls.com/prime

##Benchmarks

Based on the results above, the fastest method of all was: 

##Optimizations and scaling

- What if someone enters a book that we have already computed? It would be good to save the results to a database.  
When someone enters a book name, we can first lookup the database to check if we have the results saved.  
- What if we had 1 million users who all wanted a book parsed every day? Could use more multithreading?
The brute force solution would be to spin this program up on multiple instances and process 1 book per instance. 
I have read about running .NET/C# on AWS Lambda by having node spawn the .NET process http://itmeze.com/2016/01/06/running-c-on-aws-lambda
So we could run program on Lambda, and have it run every time a new book is uploaded to S3. 

##Limitations
- words with hyphens will have issues. they will be counted correctly, but display incorrectly. for example,
free-for-all will be displayed as freeforall. 
- similarly, two contracted words will be displayed and counted as one word. For example, "You're" will be displayed and counted as "youre". 
This is almost a business/requirements decision - should this be counted as two words? 'You' and 'are' with a count for each? Or is the current behaviour better? 
One improvement I saw in other word frequency counters online was to only count root words. So "You'll" and "You're" would both display as "you" with a count of 2. 
This is possible with regex. 
- using http://www.writewords.org.uk/word_count.asp my results were different from Railway Children. Without seeing their code, it's hard to judge why. 
However, both that site and myself get different results to http://www.textfixer.com/tools/online-word-counter.php#newText2. 
It seems there is some variation in counting words.
- there is a 2 GB limit on any object in .Net. I won't be able to handle lines longer than that, even though it sounds improbablle to have a line that long. 
 
##Extensions

- testing for more edge cases and malformed inputs. 
- some kind of UI for people to upload books and see the output. Maybe a website.
- Some word frequency counters online allow the user to exclude common words like 'the', 'to', 'and' etc. That would be a nice feature. 
If the business/customer wanted it in the case of this application, it would be easy to create a list of such common words and exclude them from the results. 
It may even speed up the app by excluding the most common results.  

[1]: Learnings about Primes

Why do we only need to test up to the square root of a number to determine if it is prime? 

This confused me for a while, so I'll include the explanation that helped. 
http://stackoverflow.com/questions/5811151/why-do-we-check-upto-the-square-root-of-a-prime-number-to-determine-if-it-is-pri

Summary:

A prime number is divisible only by itself and 1. 

Conversley, a non-prime number will have two numbers, let's call them a and b, that multiply to create it.

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