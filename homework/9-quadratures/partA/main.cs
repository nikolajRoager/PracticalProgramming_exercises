using System;
using static System.Console;
using static System.Math;
using static quad;

public static class main
{


    //double precision approximation
    public static bool approx(double a,double b,double tau=1e-9,double eps=1e-9)
    {
        if (Abs(a-b)<tau)
            return true;
        if (Abs(a-b)/(Abs(a)+Abs(b))<eps)
            return true;
        return false;
    }

    public static int Main()
    {

        double res0 = integrate(Sqrt,0,1);
        WriteLine($"integrate(Sqrt,0,1)={res0}"+(approx(res0,2.0/3.0) ? " PASS" : " FAIL"));//Just use the already existing squareroot function

        Func<double,double> invSqrt = x  => 1.0/Sqrt(x);
        double res1 = integrate(invSqrt,0,1);
        WriteLine($"integrate(1/Sqrt,0,1)={res1}"+(approx(res1,2.0) ? " PASS" : " FAIL"));

        Func<double,double> F3 = x  => 4*Sqrt(1-x*x);
        double res2 = integrate(F3,0,1);
        WriteLine($"integrate(4 Sqrt(1-x^2),0,1)={res2}"+(approx(res2,PI) ? " PASS" : " FAIL"));

        Func<double,double> F4 = x  => Log(x)/Sqrt(x);
        double res3 = integrate(F4,0,1);
        WriteLine($"integrate(ln(x)/Sqrt,0,1)={res3}"+(approx(res3,-4.0) ? " PASS" : " FAIL"));

        //Error function estimate

        Func<double,double> F5 = x  => 2*Exp(-x*x)/Sqrt(PI);
        for (double x = 0 ; x < 3.5; x+=0.05)
        {
            Error.WriteLine($"{x}\t{integrate(F5,0,x)}");
        }

        return 0;
    }

}
