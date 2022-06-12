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
        double res1;
        double err1;
        int itr1=0;

        int itr0=0;
        double err0;
        double res0;
        Func<double,double> invSqrt = x  => 1.0/Sqrt(x);
        Func<double,double> F4 = x  => Log(x)/Sqrt(x);

       (res0,err0,itr0) = integrateCC(invSqrt,0,1);
        WriteLine($"{itr0} calls; integrateCC(1/Sqrt(x),0,1)={res0} ± {err0}");


        (res1,err1,itr1) = integrateCC(F4,0,1);
        WriteLine($"{itr1} calls; integrateCC(ln(x)/Sqrt(x),0,1)={res1} ±  {err1}");


        (res0,err0,itr0) = integrate(invSqrt,0,1);
        WriteLine($"{itr0} calls; integrate(1/Sqrt(x),0,1)={res0}±   {err0}");

        (res1,err1,itr1) = integrate(F4,0,1);
        WriteLine($"{itr1} calls; integrate(ln(x)/Sqrt(x),0,1)={res1}± {err1}");

        return 0;
    }

}
