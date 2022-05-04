Homework 11 , root finding
=========
CURRENTLY DONE PART A, WILL DO NEXT PARTS IN READING PERIOD

In part A I implement Newtons using numerically calculated jacobians.

NOTE MY APROACH CAN NOT HANDLE OVER OR UNDERDETERMINED EQUATIONS! I think this is what you want, since,  in the chapter, you only seem to use functions with the same number of inputs as outputs (i.e. 2D function with a 2 vector as output). The same restrictions happen for me due to using Gram Schmidt QR decomposition (Which fails for underdetermined systems, even systems which have the same number of outputs as inputs, but dublicate inputs, such as f(x,y)= {sin(xy),0}, f(x,y)= {sin(xy),sin(xy)}, this results in a Q matrix with NaN or infinite elements), If you have an underdetermined problem, you can always add in more different constraints). My algorithm can quickly recognize that a system is over or underdetermined and simply aborts.

I know this is bad, I should really just solve an underdetermined set of equations.

Also NOTE my algorithm sometimes prints WARNINGS, saying that something went wrong, this happens if you ask the function to start somewhere where the gradient of some component is 0, which in some cases makes the Jacobi impossible to decompose, I could perhabs have written some fixes for those points, but I just redo at a slightly different (RANDOM) starting location a little offset from the original guess, this usually fixes the problem, my algorithm is allowed to make ten such re-tries (actually this is how the system knows if a system is udner or overdetermined, if it can not get anything to work after ten re-rolls, the system likely can not be solved).

You may also see warnings of zero-divisions, this should only happen if the system you run this on has different machine epsilon, my algorithm can re-scale the stepsize to still work (I honestly don't know if this can ever happen).

In part A, I demonstrate that I can find roots for a 1d function with one output, a 2d function with 2 outputs, and a 3d function with 3 outputs. In all cases I use trigonometric functions to ensure that my functions are non-linear.

I also find the minimum of the gradient of the Rosenbrock valley function f(x,y) = (1-x)^2+100(y-x^2)^2, with:

grad f(x,y) = {-2+2x- 400xy+400x^3, 200(y-x^2) }

This is easy to find analytically, for the y component can only be 0 if y=x^2, and IF y=x^2 the x component is only zero when 2x-2=0, i.e. x=1 (actually in general the global minimum is (a,a^2)), so we would expect the algorithm to find the point (1,1) and ... it does, I even go as far as to starting in the point -1,2 but it still finds 1,1 (It is possible to make it fail, by setting it too far out, as I allow at most 10000 steps)

For part A, you can use the flag -v to run in verbose mode, to see each step, including all the matrices calculated.
