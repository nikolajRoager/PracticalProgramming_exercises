Homework, io
======
I have attempted to do all parts of all exercises, these are done in four different files.

Part a reads from the standard input, part b reads from command-line arguments, part c reads an input and output file-path as a command-line argument.

In either case, the input is cast to doubles, and they, their sines and cosines are written in a table either to the standard output (part a and b) or a file (part c) The comma separated table is written to table[a,b,c].scv. In any case the error log is piped to log/log[a,b,c].csv.

The makefile automatically uses the same input -- from the file input.txt -- in all parts and saves the output to the files out/out[a,b,c].html.

the sample input.txt contains numbers separated by literal tabs, spaces, commas ',' and newlines , and the file contains both integers, decimal numbers, and some faulty inputs (the letter O, word, NaN, null) which the program should recognize without crashing. Note that part A only reads the first line, since the newline character terminates the standard input.
