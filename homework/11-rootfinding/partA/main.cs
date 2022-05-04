using System;
using static System.Console;
using static System.Math;
using static rootfinder;
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
        WriteLine($"----------------------------");
        WriteLine($"Demonstrating newtons method");
        WriteLine($"----------------------------");

        bool verbose = false;

        foreach(string s in args)
        {
            if (0==String.Compare(s,"-v"))
                verbose = true;
        }

        WriteLine($"Demonstration one, find 0=f(x)=cos(x) (true solution, x=(n+0.5) pi)");
        Func<vector,vector> f0 = (X)  => new  vector(Cos(X[0]));
        WriteLine($"Starting at x0 = (0.2) with precision 10^-6. Now Running ...");
        vector root0 = newton(f0,new vector(0.2),1e-6,verbose );

        WriteLine("");
        WriteLine(root0.getString("Got predicted root x="));

        WriteLine("");
        WriteLine(f0(root0).getString("Has f(x)="));
        WriteLine("");
        if (f0(root0).approx(new vector(1),1e-6,1e-6))//vector (int) is understood  as a 0 vector of this size
        {
            WriteLine("PASS this is within 10^-6 of 0");
        }
        else
        {
            WriteLine("FAIL this is not within 10^-6 of 0");
        }

        WriteLine($"\nDemonstration two, find 0=f(x,y)=(Sin(x+y),Sin(x)) (true solution, x+y=n pi and x=n pi)");
        Func<vector,vector> f1 = (X)  => new  vector(Sin(X[0]+X[1]),Sin(X[1]));
        WriteLine($"Starting at x0 = (1.0,1.0) with precision 10^-6. Now Running ...");
        vector root1 = newton(f1,new vector(1.0,1.0),1e-6,verbose );

        WriteLine("");
        WriteLine(root1.getString("Got predicted root x="));

        WriteLine("");
        WriteLine(f1(root1).getString("Has f(x)="));
        WriteLine("");
        if (f1(root1).approx(new vector(2),1e-6,1e-6))//vector (int) is understood as a 0 vector of this size
        {
            WriteLine("PASS this is within 10^-6 of 0");
        }
        else
        {
            WriteLine("FAIL this is not within 10^-6 of 0");
        }

        WriteLine($"\nDemonstration three: f(x,y,z)=(Sin(x+y),Sin(y+z),Sin(x+z)) (Solution x,y,z any integer multiples of pi)");
        Func<vector,vector> f2 = (X)  => new  vector(Sin(X[0]+X[1]),(X[2]+X[1]),Sin(X[2]+X[0]));
        WriteLine($"Starting at x0 = (1.0,1.0,1.0) with precision 10^-6. Now Running ...");
        vector root2 = newton(f2,new vector(1.0,1.0,1.0),1e-6,verbose );

        WriteLine("");
        WriteLine(root2.getString("Got predicted root x="));

        WriteLine("");
        WriteLine(f2(root2).getString("Has f(x)="));
        WriteLine("");
        if (f2(root2).approx(new vector(3),1e-6,1e-6))//vector (int) is understood as a 0 vector of this size
        {
            WriteLine("PASS this is within 10^-6 of 0");
        }
        else
        {
            WriteLine("FAIL this is not within 10^-6 of 0");
        }


        WriteLine($"\nDemonstration Rosenbrock, find 0=f(x,y)=(-2+2x- 400xy+400x^3, 200(y-x^2)) (gradient of Rosenbrock function, i.e. an extremum for that function, only solution is x=y=1)");
        Func<vector,vector> f3 = (X)  => new  vector( -2+2*X[0]- 400*X[0]*X[1]+400*X[0]*X[0]*X[0], 200*(X[1]-X[0]*X[0]));
        WriteLine($"Starting at x0 = (-1,2) with precision 10^-6. Now Running ...");
        vector root3 = newton(f3,new vector(-1,2),1e-6,verbose );

        WriteLine("");
        WriteLine(root3.getString("Got predicted root x="));

        WriteLine("");
        WriteLine(f3(root3).getString("Has f(x)="));
        WriteLine("");
        if (f3(root3).approx(new vector(2),1e-6,1e-6))//vector (int) is understood as a 0 vector of this size
        {
            WriteLine("PASS this is within 10^-6 of 0");
        }
        else
        {
            WriteLine("FAIL this is not within 10^-6 of 0");
        }
        return 0;
    }

}
