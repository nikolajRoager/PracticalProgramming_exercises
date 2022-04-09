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
        matrix unity=new matrix(h,w);//reference unit matrix for later error checking

        A.randomize_symmetric();


        WriteLine(A.getString("A ="));

        (matrix D, matrix V) =jacobi.getDV(A);

        WriteLine(" ");
        WriteLine(D.getString("D ="));

        WriteLine(" ");

        matrix VTV = V.transpose()*V;
        matrix VVT = V*V.transpose();
        matrix VDVT = V*D*V.transpose();
        matrix VTAV = V.transpose()*A*V;

        bool pass0 = VTV.approx(unity);
        bool pass1 = VVT.approx(unity);
        bool pass2 = VDVT.approx(A);
        bool pass3 = VTAV.approx(D);
        WriteLine(VTV.getString( "V^T V   ="));
        WriteLine( "V^T V   = 1 ? " + (pass0  ? "PASS" : "FAIL") );
        WriteLine(VVT.getString( "V V^T   ="));
        WriteLine(  "V V^T   = 1 ? " + (pass1 ? "PASS" : "FAIL"));
        WriteLine(VDVT.getString("V D V^T ="));
        WriteLine( "V D V^T   = A ? " +  (pass2 ? "PASS" : "FAIL"));
        WriteLine(VTAV.getString("V^T A V ="));
        WriteLine( "V^T A V   = D ? " +  (pass3 ? "PASS" : "FAIL"));

        WriteLine( ( pass0 && pass1 && pass2 && pass3 ? "ALL PASSED" : "SOME FAILED"));
        return 0;
    }

}
