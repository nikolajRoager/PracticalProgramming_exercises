using System;
using static System.Console;
using static matrix;

public static class main
{
    public static int Main(string[] argv)
    {
        if (argv.Length!=2)
        {
            Error.WriteLine("Input not valid, need 2 arguments width height");
            return 1;
        }


        int h = 0;
        if (!int.TryParse(argv[0],out h))
        {
            Error.WriteLine("Input not valid, could not convert  "+argv[0]+" to int");
            return 1;
        }

        int w = 0;
        if (!int.TryParse(argv[1],out w))
        {
            Error.WriteLine("Input not valid, could not convert  "+argv[1]+" to int");
            return 1;
        }


        matrix A=new matrix(h,w);

        A.randomize();
        WriteLine("Random matrix generated (display rounds to 3 digits)");

        WriteLine(A.getString(" A = "));
        matrix I =new matrix(w,w);

        //They will be overwritten anyway, but I get errors if I don't define them

        WriteLine("Q R Decomposition");

        (matrix Q,matrix R) = A.getQR();


        matrix QxR = Q*R;
        matrix QTQ = Q.transpose()*Q;

        WriteLine(Q.getString(" Q = ")+"\n");
        WriteLine(R.getString(" R = ")+"\n");

        WriteLine((QTQ).getString(" Q^TQ = "));

        //For display purposes, try to make a nicely formatted multiplication
        string[] Qstrings = Q.getStrings("   ");
        string[] Rstrings = R.getStrings(" * ");
        string[] QRstrings = (QxR).getStrings(" = ");
        string skip = "";
        for (; skip.Length<Rstrings[0].Length; skip+=" ") {}
        for (int i = 0; i<h; ++i)
        {
            if (i>=h/2-w/2 && i<h/2-w/2+w)
                WriteLine(Qstrings[i]+Rstrings[i-h/2+w/2]+QRstrings[i]);
            else
                WriteLine(Qstrings[i]+skip+QRstrings[i]);
        }

        Write("   Q^T * Q == 1 ?            ... ");
        if (QTQ.approx(I))
            WriteLine(" PASSED");
        else
            WriteLine(" FAILED");
        Write("   Q * R   == A ?            ... ");
        if (QxR.approx(A))
            WriteLine(" PASSED");
        else
            WriteLine(" FAILED");
        Write("   R is upper triangular   ? ... ");
        if (R.is_uptriangle())
            WriteLine(" PASSED");
        else
            WriteLine(" FAILED");

        int QtQfailed = 0;
        int QRfailed = 0;
        int RUTfailed = 0;

        int tests = 1024;
        WriteLine($"  Now running {tests} random matrices to test");
        for (int i = 0; i < tests; ++i)
        {
            A.randomize();
            (matrix myQ,matrix myR) = A.getQR();

            matrix myQxR = myQ*myR;
            matrix myQTQ = myQ.transpose()*myQ;


            if (!myQTQ.approx(I))
                ++QtQfailed;
            if (!myQxR.approx(A))
                ++QRfailed;
            if (!myR.is_uptriangle())
                ++QRfailed;
        }

        WriteLine($"  {tests-QtQfailed}/{tests} PASSED to have Q^T Q=1;\n  {tests-QRfailed}/{tests} PASSED to have QR=A;\n  {tests-RUTfailed}/{tests} PASSED to have R upper triangular");

        if (QtQfailed!=0 || QRfailed!=0 || RUTfailed!=0)
            WriteLine("QR decomposition FAILED");
        else
            WriteLine("QR decomposition TEST PASSED");

        WriteLine("Now trying to solve the following linear system Ax=b with");

        A= new matrix(h,h);
        var b = vector(h);//Not its own class, but actually just a single column matrix
        A.randomize();
        b.randomize();

        WriteLine(A.getString("A = "));
        WriteLine("\n");
        WriteLine(b.getString("b = "));
        (Q,R) = A.getQR();
        var x = QRsolve(Q,R,b);
        matrix Ax = A*x;
        WriteLine("\nEquation to solve");
        WriteLine(A.getString(b));
        WriteLine("\n");
        WriteLine(x.getString("x = "));
        WriteLine("\n");
        //For display purposes, try to make a nicely formatted multiplication
        string[] Astrings = A.getStrings("   ");
        string[] Xstrings = x.getStrings(" * ");
        string[] AXstrings = (Ax).getStrings(" = ");
        for (int i = 0; i<h; ++i)
        {
            WriteLine(Astrings[i]+Xstrings[i]+AXstrings[i]);
        }

        Write("   A*x == b ? ... ");
        if (Ax.approx(b))
            WriteLine(" PASSED");
        else
            WriteLine(" FAILED");

        WriteLine($"Repeat {tests} times with random linear equations");

        int LinEQfailed = 0;

        WriteLine($"  Now running {tests} random matrices to test");
        for (int i = 0; i < tests; ++i)
        {
            A.randomize();
            b.randomize();
            (matrix myQ,matrix myR) = A.getQR();


            var X = QRsolve(myQ,myR,b);
            matrix AX = A*X;

            if (!AX.approx(b))
                ++LinEQfailed ;
        }

        WriteLine($"  {tests-LinEQfailed}/{tests} PASSED to solve random linear equations;");

        if (QtQfailed!=0 || QRfailed!=0 || RUTfailed!=0)
            WriteLine("QR decomposition       FAILED");
        if (LinEQfailed != 0)
            WriteLine("Linear equation solver FAILED");


        if (!(LinEQfailed != 0 || QtQfailed!=0 || QRfailed!=0 || RUTfailed!=0))
            WriteLine("ALL TESTS PASSED");

        return 0;
    }

}
