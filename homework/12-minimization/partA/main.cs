using System;
using static System.Console;
using static System.Math;
using static optimizer;
using static vector;

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

    public static int Main(string[] args)
    {
        WriteLine($"----------------------------------------------");
        WriteLine($"Demonstrating Quasi newton optimization method");
        WriteLine($"----------------------------------------------");

        bool verbose = false;

        foreach(string s in args)
        {
            if (0==String.Compare(s,"-v"))
                verbose = true;
        }

        WriteLine($"Demonstration one, find max of f(x)=exp(-(x-2)^2) (true solution x=2");
        Func<vector,double> f0 = (X)  => Exp( -(X[0]-2)*(X[0]-2));
        WriteLine($"Starting at x0 = (0.0) with precision 10^-5. Now Running ...");
        (vector root0, int steps0) = max_qnewton(f0,new vector(0.0),1e-5,verbose );

        WriteLine("");
        WriteLine(root0.getString($"In {steps0} steps: Got predicted max at x="));

        if (root0.approx(new vector(2.0),1e-5,1e-5))//vector double is understood  as a vector with this element only
        {
            WriteLine("PASS this is within 10^-5 of 2.0");
        }
        else
        {
            WriteLine("FAIL this is not within 10^-5 of 2.0");
        }


        WriteLine($"\nDemonstration Rosenbrock, find minimum of f(x,y)=((1-x)^2+100*(y-x^2)^2) (minimum is at 1,1)");
        Func<vector,double> f1 = (X)  => ((1-X[0])*(1-X[0])+100*(X[1]-X[0]*X[0])*(X[1]-X[0]*X[0]) ) ;
        WriteLine($"Starting at x0 = (0,0) with precision 10^-5. Now Running ...");
        (vector root1, int steps1) = qnewton(f1,new vector(0,0),1e-5,verbose );

        WriteLine("");
        WriteLine(root1.getString("Got predicted root x="));

        WriteLine("");
        WriteLine(root1.getString($"In {steps0} steps: Has f(x)="));
        WriteLine("");
        if (root1.approx(new vector(1.0,1.0),1e-5,1e-5))
        {
            WriteLine("PASS this is within 10^-5 of (1,1)");
        }
        else
        {
            WriteLine("FAIL this is not within 10^-5 of (1,1)");
        }
        return 0;
    }

}
