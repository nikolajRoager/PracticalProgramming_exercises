using System;
using static System.Console;
using static System.Math;
using static quad;

public static class main
{


    //double precision approximation
    public static bool approx(double a,double b,double tau=1e-7,double eps=1e-7)
    {
        if (Abs(a-b)<tau)
            return true;
        if (Abs(a-b)/(Abs(a)+Abs(b))<eps)
            return true;
        return false;
    }

    public static int Main()
    {

        double res0;
        double err0;
        int itr0;
        double p= 3.0;
        Func<double,double> invxp = x  => 1.0/Pow(x,p);

        (res0,err0,itr0) = integrate(invxp,1,double.PositiveInfinity);
        WriteLine($"integrate(1/x^{p},1,infinity)={res0}±{err0} at {itr0} calls; should be {1/(p-1)}");

        p= 4.0;
         invxp = x  => 1.0/Pow(x,p);

        (res0,err0,itr0) = integrate(invxp,1,double.PositiveInfinity);
        WriteLine($"integrate(1/x^{p},1,infinity)={res0}±{err0} at {itr0} calls; should be {1/(p-1)}");

        double res1;
        double err1;
        int itr1;
        Func<double,double> gauss = x  => Exp(-x*x);

        (res1,err1,itr1) = integrate(gauss,double.NegativeInfinity,double.PositiveInfinity);
        WriteLine($"integrate(exp(-x*x),-infinity,infinity)={res1}±{err1} at {itr1} calls; should be {Sqrt(PI)}");




        (res1,err1,itr1) = integrate(gauss,double.NegativeInfinity,0);
        WriteLine($"integrate(exp(-x*x),-infinity,0)={res1}±{err1} at {itr1} calls; should be {Sqrt(PI)/2}");


        return 0;
    }

}
