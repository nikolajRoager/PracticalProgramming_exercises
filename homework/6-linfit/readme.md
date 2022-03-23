Homework linear equations
===========
I have done all three parts of the exercise, using randomly generated matrices.

NOTE C is done differently than suggested.

In part A I look at a 6 by 3 matrix and find the QR decomposition, I repeat this 1024 times. I also solve random linear equations on a 6 by 6 matrix, I also repeat this 1024 times, in all cases I check for any errors and find none.

In part B I invert

In part C, I measure the time of the QR decomposition at different N, note that I measure the time from inside the program, rather than outside it. This is so that I only measure the decomposition and skip the time taken to load and generate the matrices, I measurethe time to decompose a 2x2 to a 256x256 matrix.

I fit this to a a x^3+b function, this is done using my result from the next homework, which is included as fit.cs, I believe from that homework that this function is working. In this case I use 1 milli second as the uncertainty, as the timing function I used returned integer milli seconds.
