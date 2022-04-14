Homework 9 or something like that, quadratures
=========
I do believe I have done all parts.


NOTE the monte carlo method is random, and in part A I use the buil in RNG, which is different each run, during my tests I did get all the tests to corrcetly work, but some times some will randomly fail.

ALSO NOTE , I use fairly high number of points, this means my results are accuracte, but it might take half a minute to run


In part A I have implemented a plain, box shaped monte-carlo integration, which I test on the integral of xy from 0,0 to 1,1, the function f(x,y)=1 for (x^2+y^2<0.8^2) 0 otherwise; and the given target integral. In both cases I occasionally come within error of the true result. I do think these two tests are enough, to see that the integration is working.

In part B I implement the Halton sequence and try it with different prime basis, as suggested I use two different methods to estimate the error. In principle, the results are better, I get closer to the true value, and the error estimate is much much smaller.
