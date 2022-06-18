Exam project Bi-linear interpolation on a rectilinear grid in two dimensions
=============
This document outlines what project I got, and what goals I choose for this project


NOTE This is a PANDOC FLAVOURED MARKDOWN document, made with the Pandoc notation in mind, that is, it can be converted to pdf using the pandoc utility (PDF version enclosed, do check that out if you prefer), this allows the use of Latex style mathematics.

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

For part C, I wanted some 3-dimensional data, I choose (real part of) the lowest 3 wavefunctions of hydrogenlowest 3:

\begin{algin}
\end{align}


The task
==============

Task A
---------
The given task was **Build an interpolating routine which takes as the input the vectors {xi} and {yj}, and the matrix {Fi,j} and returns the bi-linear interpolated value of the function at a given 2D-point p=(px,py).**

The output are 6 png images, with 3 images of 2 different examples: OlympusMons\_both.png shows both the original and interpolated data, OlympusMons\_int.png shows only the interpolated data as a solid mesh, and OlympusMons\_above.png shows the interpolated data from above, with a heatmap and contours marked for the example with Olympus Mons.  The three remaining files do the same for the other examples; in all cases the x and y axes are longitude and latitude in degrees (East and North). The txt file outA.txt contains a few tests verifying that the interpolation satisfies the conditions.

This could have been hard, if I did not already have a perfectly working linear equation solver from the homework, which I used to solve the boundary condition equations.

I went with an object oriented approach, where I set up the parameters of the linear interpolation once, and then just call it with my coordinates, that way I don't need to solve each of the n-1 by m-1 the linear equations more than once (where n by m is the grid resolution).

Task B
-------
**Implement bi-cubic interpolation**


The output are the exact same 6 png images as in A, and the exact same file, verifying that everything worked.

NOTE, bi-cubic interpolations seems to be defined differently in different sources. I do this in a way slightly different from how cubic interpolation was defined in the book, not because I can not implement it, but because it is simply too slow to run.

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

We can calculate the derivatives in all the corners. Similarly, we can find the higher order derivatives in all the corners.


If we, for a moment, pretend that we have $d/dx z_{k,l}$,$d/dx z_{k,l}$ and $d^2/dxdy z_{k,l}$ given, we have exactly enough equations. We obviously don't have that. In our book, we used the condition that the derivatives should be continuous in the internal grid points, and second derivatives 0 at the borders. This would also give us enough equations to solve the system... but it would be absurdly slow, as our equations would couple all the coefficients of all the rectangles to one another, we would need to diagonalize a $16(n-1)(m-1)$ matrix, which my code actually can do ... but it quickly becomes far too slow (It can be done with the 8 by 8 example, but the 16 by 48 example is not happening this century).

Therefore, I do an evil trick: I will first calculate the derivatives in all the corner points numerically, from the neighbouring points, and then use these derivatives as conditions (except for the edge points, where I use the second derivative being 0 as condition instead). Then I have $(n-1)(m-1)$ independent systems of 16 by 16 matrices to diagonalize, which is far far faster.

This is not ideal, I know, and you may consider removing some or all points for this part. Even so, the result looks considerably better than the bi-linear interpolation.

Task C
-------
**Implement N-linear interpolation for a grid with arbitrary dimension N. Show that this is working by implementing the routine in 3 and 4 dimensions, and find a suitible way of visualizing this**

This exercise is easy, visualizing the result, not so much.
