using System;
using static System.Console;
using static System.Math;
using static cmath;

class main
{
    public static double stirling(double x)
    {
        //Stirlings approximation only works for somewhat large and positive x, if x is negative or somewhat small, we need to find some relations to transform it in range, possibly recursively, we use some exact relations from https://en.wikipedia.org/wiki/Gamma_function#General

        //After this we have something positive
        if(x<0)return PI/Sin(PI*x)/stirling(1-x);

        //At most 9 recursions here
        if(x<9)return stirling(x+1)/x;

        //This is Stirlings actual relation, taken more or less directly from https://en.wikipedia.org/wiki/Stirling%27s_approximation#Versions_suitable_for_calculators

        double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;

        return Exp(lngamma);

    }

    public static complex stirling(complex z)
    {

        if (z.Re==0 && z.Im ==0)//NOT A BUG, I only get NaN if z is EXACTLY 0, even if it is 0.000001 it works fine, since the assymptote at 0 is 1
            return 1;
        //The approximation does NOT work for complex numbers Stirlings approximation do yes do work, but the greater than or less than operators, needed to bring this in range, do not
        //We still need to ... somehow ... remap this to be assymptotical, ut what does that mean? gues assymptotical in Re only

        //We do have do that Γ(x-i y)=Γ(x+i y)

        //After this we have something positive
        if(z.Re<0)return PI/sin(PI*z)/stirling(1-z);


        if(z.Re<9)return stirling(z+1)/z;

        complex lngamma=z*log(z+1/(12*z-1/z/10))-z+log(2*PI/z)/2;

        return exp(lngamma);
    }


    public static int Main()
    {
        double dx = 1.0/32.0;
        double lim = 4.0;
        int steps = (int) ((lim-(-lim))/dx);
        Write($"{steps}\t");
        for (int i = 0; i <= steps; ++i)
        {
            double x= -lim+i*dx;
            Write($"{x}\t");
        }
        WriteLine("");
        for (int j = 0; j <= steps; ++j)
        {
            double y= -lim+j*dx;
            Write($"{y}\t");
            for (int i = 0; i <= steps; ++i)
            {
                double x= -lim+i*dx;
                double val =complex.magnitude(stirling(x+I*y));
                if (val>8)//Truncate manually in C#, results in better display
                    val=8;
                Write($"{val}\t");
            }
            if (j!=steps)
                WriteLine("");

        }

        return 0;
    }

}
