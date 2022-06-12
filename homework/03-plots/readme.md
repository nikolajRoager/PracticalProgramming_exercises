Homework plots
===========
Ihave done all three parts of the exercise.


NOTE in this homework I used gnuplot, rather than pyxplot, as it was is available as is on Ubuntu and Arch through the package managers, in the later homeworks I did manage to install pyxplot and used that

I also prefer to edit and save the gnuplot file seperately, in order to get the vim syntax highlight ing

I know the 3D plot in part C is not actually 3D, but you did not specify what you wanted, and I think  heatmaps are much more readable, note that the scale has been truncated at 8

PART A
------
Output: erf.png

In part A I plot the error function using the suggested approximation.



PART B
------
Output: gamma.png and lngamma.png, showing the gamma function and its log

In part B I plot the gamma function (an extension of the factorial) against known factorials. I also plot the natural log of the gamme function for somewhat larger numbers compared to $\ln(N!)=\sum_{n=1}^N \ln(n)$, note that the natural log of the function is undefined when the function is negative, this is not a mistake on my part, that is just how the log works, we were not specifically asked to plot $\log(|\gamma|)$

PART C
------
Output: gamma.png and gamma\_heatmap.png, showing the gamma function in 3D, and as a heatmap

In part C I extend the gamma function to complex numbers. I used a heatmap instead and a 3D plot, because I think heatmaps are better than regular 3D plots.

