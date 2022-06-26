Homework splines
===========
I have done all three parts of the exercise, using the same data set, here chosen by me to have some interesting spikes and bumbs, and straight sections.


YES, IT IS ALL IN THE SAME FOLDER, NO, IT IS NOT A MISTAKE, I WANT TO HAVE THEM TOGETHER SO THAT I CAN PLOT All.png

The calculations are done in 3 separate files. (Though they share binarySearch.dll)

In all cases, I use the object oriented approach, as I simply prefer the notation. In all cases I create an object with member functions. This means that the integral, and derivatives are seperate member functions. This was technically not asked for in part A, but I like to keep the notation the same so I did it anyway.

In all cases, I verify the integral and derivative by plotting them alongside a numerical approximation. In all cases the output are just figures

PART A
-------
Output: Linear.png, Linear\_integral.png and Linear\_derivative.png shows the interpolated function, derivative and integral alongside a numeric approximation. The splines are also shown in the All.png alongside all others.

Linear spline is implemented, including derivatives and integrals, which match well with the numeric result.
PART B
-------
Output: Qspline.png, Qspline\_integral.png and Qspline\_derivative.png shows the interpolated function, derivative and integral alongside a numeric approximation. The splines are also shown in the All.png alongside all others.

Quadratic spline is implemented, including derivatives and integrals, which match well with the numeric result


PART C
------
Output: Cspline.png, Cspline\_integral.png and Cspline\_derivative.png shows the interpolated function, derivative and integral alongside a numeric approximation. The splines are also shown in the All.png alongside all others. The image Cspline.png also includes the result from plotutils, which agrees very well with my result.

Cubic spline is implemented, and compared visually to plotutils (perfect match).
