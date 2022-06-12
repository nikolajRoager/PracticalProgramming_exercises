Homework 11 , root finding
=========
Part A and B done

Part A
--------
The output is a text file: OutA.txt, where I show what functions I tested, and demonstrate that they are within the user defined error of the target.

In part A I implement Newtons using numerically calculated jacobians.

NOTE my algorithm sometimes prints WARNINGS, saying that something went wrong, this happens if you ask the function to start somewhere where the gradient of some component is 0, which in some cases makes the Jacobi impossible to decompose, I could perhabs have written some fixes for those points, but I just redo at a slightly different (RANDOM) starting location a little offset from the original guess, this usually fixes the problem, my algorithm is allowed to make ten such re-tries (actually this is how the system knows if a system is udner or overdetermined, if it can not get anything to work after ten re-rolls, the system likely can not be solved).

You may also see warnings of zero-divisions, this should only happen if the system you run this on has different machine epsilon, my algorithm can re-scale the stepsize to still work (I honestly don't know if this can ever happen).

In part A, I demonstrate that I can find roots for a 1d function with one output, a 2d function with 2 outputs, and a 3d function with 3 outputs. In all cases I use trigonometric functions to ensure that my functions are non-linear.

I also find the minimum of the gradient of the Rosenbrock valley function $f(x,y) = (1-x)^2+100(y-x^2)^2$, with:

$\nabla f(x,y) = {-2+2x- 400xy+400x^3, 200(y-x^2) }$

This is easy to find analytically, the global minimum is (a,a^2)), so here it is (1,1) and ... it is what we find , I even go as far as to starting in the point -1,2 but it still finds 1,1 (It is possible to make it fail, by setting it too far out, as I allow at most 10000 steps)

For part A, you can use the flag -v to run in verbose mode, to see each step, including all the matrices calculated.


Part B
--------
The output is a txt file, OutB.txt, which prints the energy found, and tests it against the true result; I also output five png files. wavefunction.png shows my best estimate for the wavefunction, alongside the analytical solution. The remaining images tests convergence with respect to one parameter, keeping $r_min=0.003$, $R_max=10$, and relative and absolute error $0.01$ otherwise. They are called ConvergenceRmax.png, etc.

Just out of curiousity, I also made the image "M\_E.png" which is M evalutated at some different E. I did hope to see more solutions but only E_0=-0.5 seems to work.

Note, what you call $Acc$ and $eps$ I call relative and absolute error. It means the same but does not get me confused.

Do also note, I use as starting guess $E_0=-1$, anything with closer to 0 energy has the root finder chassing $E_0->0$ as M assymptotically approach 0 that way

This was surprisingly easy, I had put this homework off until the very end, because other people told we it was very very hard.
