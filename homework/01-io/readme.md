Homework, io
======
All parts of the exercise are done, in three different folders

NOTE I INTENTIONALLY, input a lot of faulty inputs, to test that the program can handles it without crashing

Part a reads from the standard input, part b reads from command-line arguments, part c reads an input and output file-path as a command-line argument.

OUTPUT: In either case, the input is cast to doubles, and they, their sines and cosines are written in a table either to the standard output (part a and b) or a file (part c) The comma separated table is written to Out[a,b,c].scv.

The makefile automatically uses the same input -- from the file input.txt.

the sample input.txt contains numbers separated by literal tabs, spaces, commas ',' and newlines , and the file contains both integers, decimal numbers, and some faulty inputs (the letter O, word, NaN, null) which the program should recognize without crashing. Note that part A only reads the first line, since the newline character terminates the standard input. THIS IS NOT A MISTAKE THAT IS JUST HOW THE STANDARD INPUT WORKS
