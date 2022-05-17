Homework 13 , Neural net
=========
Part A done, part B might be misunderstood, but I think it is done

Part A
-------
I use 8 neurons to estimate the target function, I show the result in a plot where I show both the training points and the estimate, it is certainly not perfect, but it is somewhat close.

Note, after some experimentation, I chose to use the quasi newtonian method, as the downhill simplex did not converge as well.

As other people suggested my starting guess is with the $a_i$ parameters spread evenly over the interval of the function while the other parameters are set to 1, this makes the convergence significantly faster.


Part B
-------
I interpret the exercise like this: instead of specifically training the function having 3 outputs, with 3 different weights (f(x), f'(x) and F(x)), I use three different activation functions: the gaussian wavelet, the derivative of the gaussian wavelet and its anti-derivative, the weights and parameters used in the sum is the same, but the different activation functions should give the derivative an anti-derivative.

Note that the anti-derivative can have a constant offset and still be the same, I chose my neural network to set the anti-derivative to be 0 at the start of the interval.

NOTE I chose *NOT* to use the derivative and anti-derivative in the training, Intentionally! My justification: I want my method to work, even in the case where we do not know those to begin with. I do include the derivative and anti-derivative in the final plot (analytical derivative, and anti-derivative from my quad exercise).

This might be a complete misunderstaning of how to do this, but it technically works.
