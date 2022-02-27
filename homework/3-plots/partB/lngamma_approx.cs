using System;
using static System.Console;
using static System.Math;

class main
{
    public static double lnstirling(double x)
    {
        //Stirlings approximation only works for somewhat large and positive x, if x is negative or somewhat small, we need to find some relations to transform it in range, possibly recursively, we use some exact relations from https://en.wikipedia.org/wiki/Gamma_function#General

        //After this we have something positive
        if(x<0)return Log(PI/Sin(PI*x))-lnstirling(1-x);

        //At most 9 recursions here
        if(x<9)return lnstirling(x+1)-Log(x);

        //This is Stirlings actual relation, taken more or less directly from https://en.wikipedia.org/wiki/Stirling%27s_approximation#Versions_suitable_for_calculators

        double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;

        return Exp(lngamma);

    }
    public static int Main()
    {
        double dx = 1.0/1024;
        int steps = (int) ((8.5-(-8.5))/dx);
        for (int i = 0; i <= steps; ++i)
        {
            double x= -8.5+i*dx;
            WriteLine($"{x} {lnstirling(x)}");
        }

        return 0;
    }

}
