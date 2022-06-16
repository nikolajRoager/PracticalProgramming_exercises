Exam project Bi-linear interpolation on a rectilinear grid in two dimensions
=============
This document outlines what project I got, and what goals I choose for this project


NOTE This is a PANDOC FLAVOURED MARKDOWN document, made with the Pandoc notation in mind, that is, it can be converted to pdf using the pandoc utility (PDF version enclosed), this allows the use of Latex style mathematics.

Locating the project
--------

My Student ID is 201805275, with last two digits 75. There are 23 possible projects, and my project should be $75 mod 23$. I believe $75=23 \cdot 3+6$ so $75 \equiv 6 mod 23$.

That project is Bi-linear interpolation on a rectilinear grid in two dimensions.

The Data used to demonstrate this
============
To demonstrate that this is working, I should interpolate some 2D data. I choose to use publicly available terrain elevation data for various features in the continent Tharsis on Mars. The height data used is in Public Domain, (Publisher: GeoScience PDS Node, Author: MOLA Team, Published: 2014, link to data https://astrogeology.usgs.gov/search/map/Mars/GlobalSurveyor/MOLA/Mars\_MGS\_MOLA\_DEM\_mosaic\_global\_463m). The original data is not included anywhere in this repository, as it is more than 2 GB. Instead I have converted the data to a tab-separated list containing the data, which are included in this project.

The examples I use are:

The mountain Olympus Mons, a volcano which is the largest mountain in the solar system (Data from 220 deg East to 233 East, and 12 deg North to 25 deg North), here interpolating between 8 by 8 grid points.

The Valles Marineris, a system of canyons through the Tharsis continent on Mars. (Data from 70 deg East to 130 deg East and 20 Deg South to the Equator  NOTE this example is not square, but the output images display it as square, as it better fits inside the image that way, here interpolated between a 48 by 16 point grid).


The task
==============

Task A
---------
The given task was **Build an interpolating routine which takes as the input the vectors {xi} and {yj}, and the matrix {Fi,j} and returns the bi-linear interpolated value of the function at a given 2D-point p=(px,py).**

The output are 6 png images, with 3 images of 2 different examples: OlympusMons\_both.png shows both the original and interpolated data, OlympusMons\_int.png shows only the interpolated data as a solid mesh for the example with Olympus Mons.  VallesMarineris\_both.png and VallesMarineris\_int.png do the same for the other examples; in all cases the x and y axes are longitude and latitude in degrees (East and North). The txt file outA.txt contains a few tests verifying that the interpolation satisfies the conditions.

This could have been hard, if I did not already have a perfectly working linear equation solver from the homework, which I used to solve the boundary condition equations.

I went with an object oriented approach, where I set up the parameters of the linear interpolation once, and then just call it with my coordinates, that way I don't need to solve each of the n-1 by m-1 the linear equations more than once (where n by m is the grid resolution).

Task B
-------
**Implement bi-cubic interpolation**

WARNING SLOW TO RUN PROGRAM, as I need to solve a system of $16(n-1)(m-1)$ linear equations. This is done on startup, any later calls to the interpolator after that simply uses the coefficients calculated at startup.


I think it will be worth going through what I did.

Once again, we just need a linear set of equations, which my linear equation solver can solve. One way of defining the expression for the interpolated function value is:

\begin{align}
f^{k,l}(\Delta x,\Delta y) &= \sum_{i=0}^3 \sum_{j=0}^3  \Delta x^i \Delta y^j a_{ij}^{(k,l)}
\end{align}

Where $k,l$ denotes that  $x_k \leq x \leq x_{k+1}$ and $y_{l} \leq y \leq y_{l+1}$, for $k,l$ from 0 up to $m-1$ and $n-1$ respectively. It is considerably easier to work with if we write the interpolated functions in terms of $\Delta x$,$\Delta y$ instead of $x$ and $y$. We have 16 free parameters per the $(n-1)(m-1)$ rectangles, the first conditions to fulfill are, of course, the functions values at each corner which is:

\begin{align}
f^{k,l}(0,0)     &= a_{00}^{(k,l)} = z_{l,k},\\
f^{k,l}(w_k,0)   &= \sum_{i=0}^3  w_k^i a_{i0}^{(k,l)} = z_{l,k+1},\\
f^{k,l}(0,h_l)   &= \sum_{j=0}^3  h_l^j a_{0j}^{(k,l)} = z_{l+1,k},\\
f^{k,l}(w_k,h_l) &= \sum_{i=0}^3 \sum_{j=0}^3  w_k^i h_l^j a_{ij}^{(k,l)} = z_{l+1,k+1}.
\end{align}

Where $w_k$ and $h_l$ are the width and height of each rectangle,  this give us 4 linear equations per rectangle. Leaving us 12 conditions (or 3 conditions for each of the corners) short. A common choice seems to be using the derivatives with respect to $x$, $y$, $x$ and $y$ and $x$  which, in general, are:

\begin{align}
\frac{d}{dx} f^{k,l}(\Delta x,\Delta y) &= \sum_{i=1}^3 \sum_{j=0}^3  i \Delta x^{i-1} \Delta y^j a_{ij}^{(k,l)},\\
\frac{d}{dy} f^{k,l}(\Delta x,\Delta y) &= \sum_{i=0}^3 \sum_{j=1}^3  j \Delta x^i \Delta y^{j-1} a_{ij}^{(k,l)},\\
\frac{d^2}{dx dy} f^{k,l}(\Delta x,\Delta y) &= \sum_{i=1}^3 \sum_{j=1}^3  ij \Delta x^{i-1} \Delta y^{j-1} a_{ij}^{(k,l)},
\end{align}

For instance in the top-left corner.

\begin{align}
\frac{d}{dx} f^{k,l}(0,0)      &= a_{10}^{(k,l)},\\
\frac{d}{dy} f^{k,l}(0,0)      &= a_{01}^{(k,l)},\\
\frac{d^2}{dx dy} f^{k,l}(0,0) &= a_{11}^{(k,l)},
\end{align}

And similar in the other corners.

If we, for a moment, pretend that we have $d/dx z_{k,l}$,$d/dx z_{k,l}$ and $d^2/dxdy z_{k,l}$ given, we have exactly enough equations. We obviously don't have that. The easy thing to do is to numerically approximate these from the neighbouring points -- the approach suggested by the Wikipedia article on the matter -- or, as a logical extension of our approach in 1 dimension, we can simply demand continuity of these derivative.

I am going to stick with Wikipedias approach for one simple reason: this way the equations for all the rectangles don't couple to one another, and they can be solved individually. So instead of diagonalizing a $16(n-1)(m-1)$ by $16(n-1)(m-1)$ matrix -- which is sloooow --, I am simply diagonalizing $(n-1)(m-1)$ 16 by 16 matrices, which is far better.


Task C
-------
**Implement N-linear interpolation for a grid with arbitrary dimension N. Show that this is working by implementing the routine in 3 and 4 dimensions, and find a suitible way of visualizing this**


