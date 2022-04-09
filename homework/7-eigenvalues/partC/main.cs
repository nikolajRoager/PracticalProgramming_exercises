using System;
using static System.Console;
using static matrix;
using static System.Math;
using static jacobi;

public static class main
{
    public static int Main(string[] argv)
    {
        int w=0;
        int h=0;
        if (argv.Length==1)
        {

            if (!int.TryParse(argv[0],out h))
            {
                Error.WriteLine("Input not valid, could not convert  "+argv[0]+" to int");
                return 1;
            }
            w=h;
        }
        else
        {
            Error.WriteLine("Input not valid, need 1 arguments, height/width of matrix ");
            return 1;
        }
        matrix A=new matrix(h,w);

        A.randomize_symmetric();

        //The compiler might mess up and throw this away
        (matrix D, matrix V) =jacobi.getDV(A);

        return 0;
    }

}
