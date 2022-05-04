using System;
using static System.Console;
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


        int wh = 0;
        if (!int.TryParse(argv[0],out wh))
        {
            Error.WriteLine("Input not valid, could not convert  "+argv[0]+" to int");
            return 1;
        }

        matrix A=new matrix(wh,wh);

        A.randomize();
        WriteLine("Random matrix generated (display rounds to 3 digits)");

        WriteLine(A.getString(" A = "));
        matrix I =new matrix(wh,wh);

        //They will be overwritten anyway, but I get errors if I don't define them

        WriteLine("Q R Decomposition");

        (matrix Q,matrix R) = A.getQR();


        matrix invA = inverse(Q,R);
        matrix AinvA = A*invA;
        matrix invAA = invA*A;

        WriteLine(invA.getString(" A^-1 = ")+"\n");
        WriteLine(AinvA.getString(" A^-1A = ")+"\n");
        WriteLine(AinvA.getString(" AA^-1 = ")+"\n");


        Write("   A^-1 A= 1 ... ");
        if (AinvA.approx(I))
            WriteLine(" PASSED");
        else
            WriteLine(" FAILED");
        if (invAA.approx(I))
            WriteLine(" PASSED");
        else
            WriteLine(" FAILED");
        return 0;
    }

}
