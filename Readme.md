#Word Frequency Counter

This is a technical test to parse text files and count the frequency of words. 


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


##Installation


- git clone git@github.com:thomasdane/wordFrequencyCounter.git

- open the solution in Visual Studio

- Run the tests with TEST --> Run --> All Tests (Or Ctrl + R + A)

- Run the program with F5


##Approach


####TDD

I have never used TDD at work, but I thoroughly enjoyed using it here. 

I might even start using it in my side-project! I feel it reduced the stress of bugs. 

####Parsing text

Counting words in each line of text sequentially would have performance drawbacks for very large books. 

Recently I did some 'hello world' tutorials with multithreading https://github.com/thomasdane/multithreading

So what if rather than parsing the book sequentially, the program loads each line into an array and acts on them concurrently!?

My research led me to this awesome article detailing such an approach: http://cc.davelozinski.com/c-sharp/the-fastest-way-to-read-and-process-text-files

A class called ConcurrentDictionary allows us to keep track of the word frequency count for every line in the array: 

http://geekswithblogs.net/blackrabbitcoder/archive/2011/02/17/c.net-little-wonders-the-concurrentdictionary.aspx

I did some further reading about the limitations of ParallelFor (http://www.i-programmer.info/programming/c/1420-the-perils-of-the-c-parallel-for-.html) and it seemed that counting words would be fine: "it works if the for loop is essentially a set of independent operations"

####Other languages
There's lots of great literature written in languages other than English, so I added unicode support and tests for non-latin alphabets. 

####Book length

I included War and Peace as an example, but it is not the longest novel ever written.

https://en.wikipedia.org/wiki/List_of_longest_novels

So I coplied War And Peace twice into the one file, producing a book far longer than the longest ever book. It is still only 6MB though, so this program might have different concerns if we were processing GBs or TBs of text. 


##Other Solutions with Pros and Cons

In terms of a very different solution, it would be great to try this task with F# and a functional approach. Given time, I would love to try it. 

Nevertheless, along the way there were various places for mini optimizations.


###Benchmarking

I created some interfaces which allowed me to swap in different implementations of the methods. These alternative approaches are in a folder called 'SecondVersion'


####Prime numbers


**Original approach**

The prime number calculator is not very sophisticated. It does not skip numbers

that are multiples of numbers already tested. For example, once we know that the integer is not divisible by 5

we do not need to check 10, 15, 20 and so on. Initially it did not even stop at the square root of the target number, but I added that later.

There are lots of ways to find primes fast, as discussed here: http://stackoverflow.com/questions/15743192/check-if-number-is-prime-number

But for the purposes of this program, it performs fairly decently.

**Alternative**

I read that under the hood .NET hardcodes primes to speed up HashHelpers https://www.dotnetperls.com/prime

It made me think about hardcoding a list of smaller primes (less than the max count in War and Peace). The program could first check if the integer exists in the hardcoded array. If not, only then calculate the prime.  

Here is the result: 

![Screenshot](ImagesForReadme/HardCodedPrimes.png)


You can see that for RailwayChildren (a smaller result set) the hardcoded list was nearly twice.

For War and Peace however (larger result set), the results varied slightly but on average the hardcoded list was slower. Initially I used an Array of primes, so I refactored to use a HashSet, which has O(1) lookups: http://stackoverflow.com/questions/9812020/what-is-the-lookup-time-complexity-of-hashsettiequalitycomparert

The results were the same! This made me think the bottleneck is not at Calculating Primes. 

So I ran a benchmark against all the methods and discovered major bottleneck is in WordCounter: 


![Screenshot](ImagesForReadme/Bottleneck.png)

And it turns out that the WordCounter class is by far the slowest!


####Word Counter


**Original Approach**
My original code is below: 
![Screenshot](ImagesForReadme/WordCountOriginal.png)

One theory was that if the line is very long, String.Split creates a large number of string objects. This overhead might mean that parsing by chars is faster http://stackoverflow.com/questions/8784517/counting-number-of-words-in-c-sharp


However when I ran more targeted benchmarks, it seems String.Split is not the main problem:


![Screenshot](ImagesForReadme/WordCountBottleneck.png)


By far the most expensive code is the loop which replaces the punctuation and then adds/updates the dictionary:


![Screenshot](ImagesForReadme/ExpensiveCode.png)


Digging further into that code, the regex performs sometimes twice as slow as the add/update: 


![Screenshot](ImagesForReadme/Regex.png)


So let's refactor! 

**Alternative Approach**


I swapped out the regex for a comparison based on chars. I also used a StringBuilder to create the word

at the end without creating an intermediate string object on each step. 


![Screenshot](ImagesForReadme/CodeNoRegex.png)


And the results were much better. The smaller book was less than half the speed! The longer book about 30% faster. 


![Screenshot](ImagesForReadme/RegexResults.png)


Why is this? 


The performance for regex is really quite bad. 


http://stackoverflow.com/questions/12428776/why-are-c-sharp-compiled-regular-expressions-faster-than-equivalent-string-metho


https://blogs.msdn.microsoft.com/debuggingtoolbox/2008/04/02/comparing-regex-replace-string-replace-and-stringbuilder-replace-which-has-better-performance/


Based on some comments online, I tried using a Compiled option: Regex("[^\\p{L}]", RegexOptions.Compiled) but for some reason the results were even slower. 

####Pros and Cons

Some people may find the Regex.replace method more readable or intuitive. However it is slower than the method using char.IsLetter

Checkout the results when run against two copies of War and Peace:


![Screenshot](ImagesForReadme/TwoCopiesWarAndPeace.png)

It's more than twice as fast. So I think this would be a conversation you'd have to have with the team. Is it actually more readable and intuitive? How often are we going to deal with huge books? It's nice to have both options though and be able to bring data to a conversation with the team/customer/business. 


##Optimizations and scaling


- What if someone uploads a book that we have already computed? It would be good to save the results to a database. 

Then if someone uploads a book, we can first lookup the database and check if we have the results saved.  

- What if we had 1 million users who all wanted a book parsed? Could use more multithreading?

The brute force solution might be to spin up multiple instances and process 1 book per instance. 

I have read about running .NET on AWS Lambda by having node spawn the .NET process http://itmeze.com/2016/01/06/running-c-on-aws-lambda

So we could run program on Lambda, and have it run every time a new book is uploaded to S3. 

- Another mini optimisation would be to speed up the FileReader method by pre-allocate the size of the array  http://cc.davelozinski.com/c-sharp/the-fastest-way-to-read-and-process-text-files


##Limitations

- Words with hyphens will have issues. They will be counted correctly, but display incorrectly. For example,
"free-for-all" will be displayed as "freeforall". 

- Similarly, two contracted words will be displayed and counted as one word. For example, "You're" will be displayed and counted as "youre".  This is almost a business/requirements decision - should this be counted as two words? Or is the current behaviour desirable? 

- One improvement I saw in other word frequency counters online was to only count root words. So "You'll" and "You're" would both display as "you" with a count of 2. 

- Using http://www.writewords.org.uk/word_count.asp my results were different for Railway Children. Without seeing their code, it's hard to judge why. However, both that site and myself get different results to http://www.textfixer.com/tools/online-word-counter.php#newText2. It seems there is some variation in counting words.

- there is a 2 GB limit on any object in .Net. As rare as it might be, the program won't be able to handle lines larger than that. 

 
##Extensions

- testing for more edge cases and malformed inputs. 

- some kind of UI for people to upload books and see the output. 

- Some word frequency counters online allow the user to exclude common words like 'the', 'to', 'and' etc. That would be a nice feature. If the business/customer wanted it in the case of this application, it would be easy to create a list of such common words and exclude them from the results. It may even speed up the app by excluding the most common results.

##Thanks

Thank you to Murieann and Francois for giving me the opportunity to take this code test. I really enoyed it and learned so much! About TDD, parallel processing, benchmarking, primes, and that ironically "In Search of Lost Time" is the longest book ever written!