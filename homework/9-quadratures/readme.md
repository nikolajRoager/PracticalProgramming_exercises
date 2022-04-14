Homework 9 or something like that, quadratures
=========
I do believe I have done all parts.

Part A is easy, I calculate the integrals asked, and check that they are within error of the true result.



In Part B, I do implement a function IntegralCC which uses Clenshaw–Curtis variable transformation variable transformation, I print how many calls to the functions are being made, and the "improved" version gets away with many fewer calls ... but there is a catch.

The variable transformation is extremely unstable, and liable to get stuck in an infinite loop of recursions (where the tolerance decreases faster than the error, eventually leading to rounding errors and then an actual stack-overflow). To be fair, this only happens if I use a very tight tolerance (relative and absolute accuracy = 1e-8 or better). The not-improved version do work at these precisions, without getting stuck, and for this reason, I honestly prefer the version I implemented in part A. Sure, the non-improved version might make a few thousand calls where the improved version makes a few dozen, but at least I have never seen eat the entire stack.

I did also compare against the scipy package quadrature, which made much fewer calls than the not variable-shifted version, but returned much worse results, I suspect the scipy version does not scale the absolute accuracy with 1/sqrt(2) as you suggested.

In part C I include the error estimate (assuming unrelated errors) and allow infinite limit, in the C# version I test 1/x^3, 1/x^4 integrated from 1 to infinity and the gaussian integral exp(-x^2) from - to + infinity, and ffrom -infinity to 0 just to test all the limits. The results are correct, notably 1/x^3 gives the exact result instantly, while the remaining examples require a couple hundred iterations (at this high precision at least)

I only test 1/x^4 from 0 to infinity and exp(-x^2) from - to + infinity, the result is, needless to say, correct. The number of iterations is much smaller, and the error is smaller as well. Oh well, the goal was not to beat scipy.


