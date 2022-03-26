using System;
using static System.Console;
using System.Diagnostics;
using static matrix;

public static class main
{
    public static int Main(string[] argv)
    {
        if (argv.Length!=1)
        {
            Error.WriteLine("Input not valid, need 1 argument: square matrix size");
            return 1;
        }


        int n = 0;
        if (!int.TryParse(argv[0],out n))
        {
            Error.WriteLine("Input not valid, could not convert  "+argv[0]+" to int");
            return 1;
        }




        matrix A=new matrix(n,n);
        A.randomize();


        //Hope the compiler does not optimize this away
        (matrix Q,matrix R) = A.getQR();


        return 0;
    }

}
