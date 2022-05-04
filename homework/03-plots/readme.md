Homework plots
===========
I have done all three parts of the exercise, in part A I plot the error function using the suggested approximation.

In part B I plot the gamma function (an extension of the factorial) against known factorials. I also plot the natural log of the gamme function for somewhat larger numbers compared to $\ln(N!)=\sum_{n=1}^N \ln(n)$, note that the natural log of the function is undefined when hte function is negative, this is not a mistake on my part, that is just how the log works, we were not specifically asked to plot $\log(|\gamma|)$

In part C I extend the gamma function to complex numbers. Though I used a heatmap instead of a 3D plot, because I did not like the way the 3D plot looked

I use gnuplot, rather than pyxplot, as it is available as is on Ubuntu and Arch through the package managers (I could also just have downloaded the source code for Pyxplot on Arch, but installing Gnuplot was easier)

I also prefer to edit and save the gnuplot file seperately, in order to get the vim syntax highlight ing

I know the 3D plot in part C is not actually 3D, but you did not specify what you wanted, and I think  heatmaps are much more readable, note that the scale has been truncated at 8
