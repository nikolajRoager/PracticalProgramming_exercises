using System;
using static System.Console;
using static System.Math;
using static montecarlo;

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
        WriteLine($"-------------------------------------------");
        WriteLine($"Demonstrating plain monte carlo integration");
        WriteLine($"-------------------------------------------");
        WriteLine($"NOTE, Monte Carlo is random, and give DIFFERENT results each run (different seeds). It will occasionally fail to get within error of te true result, this is to be expected, more often than not it should work");


        Func<doublelist,double> FF = (X)  => (X[0]*X[0]+X[1]*X[1] < 0.8*0.8 ? 1.0 : 0.0);
        //First test, integrate x*y from 0,0 to 1,1
        doublelist a=new doublelist(2);
        doublelist b=new doublelist(2);
        a[0]=0;
        a[1]=0;
        b[0]=1;
        b[1]=1;
        double res2;
        double err2;
        (res2,err2) = strat_box(FF,a,b,20000,64);
        WriteLine($"-----------------------------------------------------");
        WriteLine($"Stratified integral of f(x,y)=1 if x^2+y^2<0^2.8, otherwise 0  from 0,0 to 1,1 is {res2}±{err2} "+(approx(res2,0.5026548245743669   ,err2,err2) ? " PASS" : " FAIL"));
        WriteLine($"-----------------------------------------------------");

        return 0;
    }

}
