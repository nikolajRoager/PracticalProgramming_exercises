Homework 12 , minimization
=========
Part A and B and C done. Though I am not sure if B counts as done.


Just for fun, part A and part C also prints a figure with all the steps taken. This is not part of the exercise, but I used it for debugging and decided to leave it in.

In part A I implement Quasi-Newton minimization using numerically calculated derivatives and.

I test it to find maximum of a 1 dimensional gaussian $exp(-(x-2.0)^2)$ (max 2.0), minimum of Rosenbrocks 2D valley function (with minimum set to 1,1), and the two-dimensional Himmelblau function (4 possible minima, I find the one I start closest to).

You will see in my out file where I start my guess, and what my precision is.


PART B
-----
I do not know if B is counts as done, because I do not get exactly the "true" result, my result is "close" (it is mass being 125.9 GeV/c^2 while it truly was 125.3 GeV/c^2)

If this is the true only data the true mass was calculated from, then I should get exactly 125.3 GeV/c^2 , but if it is not, then my result is not outright awful.

Though, Do note that the function to minimize had a lot of local minima around, so I had to do a start guess at m=121 GeV/c^2 to avoid them.


PART B
-----

In part C I implement the downhill simplex method, applied to. This particular implementation expects a 2 dimensional input (x and y). As the name suggests, it can be generalized to higher dimensions using higher dimension simplicies (A type of shape with n+1 verticies, where n is the dimension), though I have not done this.

I test it in the Rosenbrock and Himmelblau valley functions.



