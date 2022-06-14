Exam project Bi-linear interpolation on a rectilinear grid in two dimensions
=============
This document outlines what project I got, and what goals I choose for this project


Locating the project
--------

My Student ID is 201805275, with last two digits 75. There are 23 possible projects, and my project should be $75 mod 23$. I believe $75=23 \cdot 3+6$ so $75 \equiv 6 mod 23$.

That project is Bi-linear interpolation on a rectilinear grid in two dimensions.

The Data used to demonstrate this
============
To demonstrate that this is working, I should interpolate some 2D data. I choose to use publicly available terrain elevation data for various features in the continent Tharsis on Mars. The height data used is in Public Domain, (Publisher: GeoScience PDS Node, Author: MOLA Team, Published: 2014, link to data https://astrogeology.usgs.gov/search/map/Mars/GlobalSurveyor/MOLA/Mars\_MGS\_MOLA\_DEM\_mosaic\_global\_463m). The original data is not included anywhere in this repository, as it is more than 2 GB. Instead I have converted the data to a tab-separated list containing the data, which are included in this project.

The examples I use are:

The mountain Olympus Mons, a volcano which is the largest mountain in the solar system (Data from 220 deg East to 233 East, and 12 deg North to 25 deg North), here interpolating between 8 by 8 grid points.

The Valles Marineris, a system of canyons through the Tharsis continent on Mars.

I did consider the Noctis Labyrinthus system of valleys, but ultimately the valleys are too steep and narrow that I would need too many grid points, and at that point it really would not be a lot of room to interpolate.

Ultimately, I only use square segments of the surface as example, my code can handle actual rectangle, but gnuplot, which I use to plot the result, does not handle that as well.


The task
==============

Task A
---------
The given task was **Build an interpolating routine which takes as the input the vectors {xi} and {yj}, and the matrix {Fi,j} and returns the bi-linear interpolated value of the function at a given 2D-point p=(px,py).**

The output are 6 png images, with 3 images of 2 different examples: OlympusMons\_both.png shows both the original and interpolated data, OlympusMons\_int.png shows only the interpolated data as a solid mesh for the example with Olympus Mons.  TharsisMontes\_both.png and TharsisMontes\_int.png does the same for the other examples; in all cases the x and y axes are longitude and lattitude in degrees (East and North). The txt file outA.txt contains a few tests verifying that the interpolation sattisfies the conditions.

This could have been hard, if I did not already have a perfectly working linear equation solver from the homework, which I used to solve the boundary condition equations.

I went with an object oriented approach, where I set up the parameters of the linear interpolation once, and then just call it with my coordinates, that way I don't need to solve each of the n-1 by m-1 the linear equations more than once (where n by m is the grid resolution).

Task B
-------
**Implement bi-cubic interpolation; boundery condition: vanishing gradients at all borders**

Task C
-------
**Implement N-linear interpolation for arbitrary dimensional  grids N. Show that this is working by implementing the routine in 3 and 4 dimensions, and find a suitible way of visualizing this**


