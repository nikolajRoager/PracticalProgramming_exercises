Homework 13 , Neural net
=========
Part A and B done. Sorry about not doing C.

Part A
-------
Output files: OutA.png shows the function estimate and the target, OutA.txt merely tells what the cost function was before and after optimization.

I use 8 neurons to estimate the target function, I show the result in a plot where I show both the training points and the estimate, it is certainly not perfect, but it is somewhat close.

Note, after some experimentation, I chose to use the quasi newtonian method, as the downhill simplex did not converge as well.

As other people suggested my starting guess is with the $a_i$ parameters spread evenly over the interval of the function while the other parameters are set to 1, this makes the convergence significantly faster.


Part B
-------
Output files: OutB.png shows the function estimate and the target, OutB.txt merely tells what the cost function was before and after optimization.

I interpret the exercise like this: instead of specifically training the function having 3 outputs, with 3 different weights (f(x), f'(x) and F(x)), I use three different activation functions: the gaussian wavelet, the derivative of the gaussian wavelet and its anti-derivative, the weights and parameters used in the sum is the same, but the different activation functions should give the derivative an anti-derivative.

I get reasonable agreement with the true derivatives and anti-derivatives, not perfect, but I think this should count as working.
