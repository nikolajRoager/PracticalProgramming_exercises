Exam project Bi-linear interpolation on a rectilinear grid in two dimensions
=============
This document outlines what project I got, and what goals I choose for this project


Locating the project
--------

My Student ID is 201805275, with last two digits 75. There are 23 possible projects, and my project should be $75 mod 23$. I believe $75=23 \cdot 3+6$ so $75 \equiv 6 mod 23$.

That project is Bi-linear interpolation on a rectilinear grid in two dimensions.

The task
==============

Task A
---------
The given task was **Build an interpolating routine which takes as the input the vectors {xi} and {yj}, and the matrix {Fi,j} and returns the bi-linear interpolated value of the function at a given 2D-point p=(px,py).**

To demonstrate that this is working, I should interpolate some 2D data. I choose to use publicly available terrain elevation data for part of the mountain Olympus Mons and the Noctis Labyrinthus valleys in the continent Tharsis on Mars. The height data used is in Public Domain, (Publisher: GeoScience PDS Node, Author: MOLA Team, Published: 2014, link to data https://astrogeology.usgs.gov/search/map/Mars/GlobalSurveyor/MOLA/Mars\_MGS\_MOLA\_DEM\_mosaic\_global\_463m). The original data is not included anywhere in this repository, as it is more than 2 GB. Instead I have converted the data to a tab-separated list containing the data, and meta-data for, which are included in this project.

The examples I use in task A is the mountain Olympus Mons (Data from 220 deg East to 233 East, and 12 deg North to 25 deg North), here interpolating between 8 times 8 grid points.




Task B
-------
**Extend the routine to work in arbitrary dimensions. Show that this is working by implementing the routine in 3 dimensions, and find a suitible way of visualizing this**

Task C
-------
**Implement linear interpolation on  triangular grid in 2D**


