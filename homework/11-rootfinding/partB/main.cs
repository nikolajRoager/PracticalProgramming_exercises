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

    //Get the f with this energy
    static (genlist<doublelist>,doublelist) getF(double energy,double absErr = 0.001,double relErr=0.001,double r_min = 0.01, double r_max =10)
    {


        System.Func<double,doublelist,doublelist> ODE= delegate(double r,doublelist data)
        {

            doublelist Out = new doublelist(2);
            Out[0] = data[1];//(f)' = f'
            Out[1] = -2*(energy+(1.0/r))*data[0];// (f')' = -2(εf+(1/r))f

            return Out;
        };

        doublelist f0 = new doublelist(2);
        f0[0] = r_min-r_min*r_min;//r-r*r
        f0[1] = 1-2*r_min;//d(r-r^2)/dr = 1-2r

        return ode.driver(
            ODE,
            r_min,
            r_max,
            f0,
            0.05,  //h initial
            absErr,//Absolute error
            relErr//Relative and absol
        );//I do NOT include the max step size parameter, as it otherwise would overrule the absolute and relative error
    }
    public static int Main(string[] args)
    {
        WriteLine($"------------------------------");
        WriteLine($"Demonstrating Shooting method\nODE based  boundary-problems");
        WriteLine($"----------------------------");

        bool verbose = false;

        foreach(string s in args)
        {
            if (0==String.Compare(s,"-v"))
                verbose = true;
        }



        genlist<doublelist> f_r= new genlist<doublelist>();
        doublelist r_list = new doublelist();



        double absErr = 0.01;
        double relErr=0.01;
        double r_min = 0.003;
        double r_max =10;

        //Sorry for all the 1-d vector nonsense, I am reusing an ODE clearly meant for multi dimensions
        System.Func<vector,vector> M= delegate(vector energy)
        {

            (f_r,r_list) = getF(energy[0],absErr,relErr,r_min,r_max);

            return new vector(f_r[f_r.size-1][0]);//Offset of f_r from 0, the first element is f(r), the second is f'(r) which I do not care about
        };


        WriteLine($"Just for fun, plotting M for E from -1.0 to 0.0");
        for (double E = -1.0; E<-0.01; E+=0.01)
        {
            Error.WriteLine($"{E}\t{M(new vector(E))[0]}");
        }
        WriteLine($"-----------------------------------------------");



        Error.WriteLine($"");
        Error.WriteLine($"");

        {
        WriteLine($"Starting at E = (-1.0) with precision 10^-6, absErr={absErr}, relErr={relErr}, r_min={r_min}, r_max={r_max} Now Running ...");
        (vector vec_E0,int N) = newton(M,new vector(-1.0),1e-6,verbose );
        double E0 = vec_E0[0];
        WriteLine("");
        WriteLine($"In {N} steps Got predicted root E0={E0}");

        if (approx(E0,-0.5,1e-6,1e-6))
        {
            WriteLine("PASS this is within 10^-6 of -0.5 (any remaining error is likely due to the rootfinder, not the ODE convergence)");
        }
        else
        {
            WriteLine("FAIL this is not within 10^-6 of the true solution -0.5");
        }


        (f_r,r_list) = getF(E0,absErr,relErr,r_min,r_max);
        //Print wavefunction
        for (int i = 0; i<f_r.size; ++i)
        {
            Error.WriteLine($"{r_list[i]}\t{f_r[i][0]}\t{f_r[i][1]}\t{r_list[i]*Math.Exp(-r_list[i])}");
        }

        }
        WriteLine($"-----------------------------------------------");
        WriteLine($"Testing convergence");
        //Convergence tests for r_min
        Error.WriteLine($"");
        Error.WriteLine($"");

        absErr = 0.01;
        relErr=0.01;
        r_min = 0.003;
        r_max =10;

        bool not_yet= true;
        for (r_min = 0.05; r_min> 0.001; r_min -=0.001)
        {
            (vector vec_E0,int N) = newton(M,new vector(-1.0),1e-6,verbose );
            double E0 = vec_E0[0];

            if (not_yet && approx(E0,-0.5,1e-6,1e-6))
            {
                WriteLine($"Only at r_min <= {r_min} do we get the right energy (With absErr={absErr}, relErr={relErr},  r_max={r_max}) ");
                not_yet = false;
            }


            Error.WriteLine($"{r_min}\t{E0}");
        }

        //Convergence tests for r_max
        Error.WriteLine($"");
        Error.WriteLine($"");

        absErr = 0.01;
        relErr=0.01;
        r_min = 0.003;
        r_max =10;

        not_yet= true;
        for (r_max = 5; r_max< 15; r_max +=1.0)
        {
            (vector vec_E0,int N) = newton(M,new vector(-1.0),1e-6,verbose );
            double E0 = vec_E0[0];

            if (not_yet && approx(E0,-0.5,1e-6,1e-6))
            {
                WriteLine($"Only at r_max >= {r_max} do we get the right energy (With absErr={absErr}, relErr={relErr}, r_min={r_min} ) ");
                not_yet = false;
            }


            Error.WriteLine($"{r_max}\t{E0}");
        }

        //Convergence tests for relErr
        Error.WriteLine($"");
        Error.WriteLine($"");

        absErr = 0.01;
        relErr=0.01;
        r_min = 0.003;
        r_max =10;

        not_yet= true;
        for (relErr = 0.1; relErr> 0.0; relErr -=0.01)
        {
            (vector vec_E0,int N) = newton(M,new vector(-1.0),1e-6,verbose );
            double E0 = vec_E0[0];

            if (not_yet && approx(E0,-0.5,1e-6,1e-6))
            {
                WriteLine($"Only at relErr <= {relErr} do we get the right energy (With absErr={absErr},  r_min={r_min}, r_max={r_max}) ");
                not_yet = false;
            }


            Error.WriteLine($"{absErr}\t{E0}");
        }

        //Convergence tests for absErr
        Error.WriteLine($"");
        Error.WriteLine($"");

        absErr = 0.01;
        relErr=0.01;
        r_min = 0.003;
        r_max =10;

        not_yet= true;
        for (absErr = 0.1; absErr> 0.0; absErr -=0.01)
        {
            (vector vec_E0,int N) = newton(M,new vector(-1.0),1e-6,verbose );
            double E0 = vec_E0[0];

            if (not_yet && approx(E0,-0.5,1e-6,1e-6))
            {
                WriteLine($"Only at absErr <= {absErr} do we get the right energy (With  relErr={relErr}, r_min={r_min}, r_max={r_max}) ");
                not_yet = false;
            }


            Error.WriteLine($"{absErr}\t{E0}");
        }

        return 0;
    }

}
