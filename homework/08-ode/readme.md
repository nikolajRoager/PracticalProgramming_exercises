Homework 8, ordinary differential equaitons
===========


Part C IS NOT DONE YET, MISSING INITIAL CONDITION NOT FOUND

I use my generic list class from one of the previous homeworks to store the output of every substep.


In part A I (1) implemented the Dormand Prince 5-4 embedded stepper, and (2) an adaptive stepsize driver.

As an example, I have solved (3) a simple problem, in this case y''=-y (should be sin(x)) (4) the requested problem, pendulum with friction.

I did, originally, attempt to implement a generic ode stepper and driver, but C# --unlike C++ -- can not accept statements such as `Data* 0.5` where `Data` is a generic type, likewise, C# does not allow addition of generic types.

For this reason, my ode uses a custom variable length list of doubles class, which combines features from the vec and genlist exercises.

I was supposed to first implement the output of the entire path in part B, but I really don't see how I could reproduce the plot from Scipy without doing that in A already.

Do note that I implement a max step size, my stepper is good enough to get away with very few steps, to get a readable graph out I must force it to use more steps. This can be turned off.

In part B I store all the points each step (actually, I did that in A because I needed to, but I do it in B as well) and I check the error on a per-point basis (Actually I originally made it that way, but I turned it off in A)
