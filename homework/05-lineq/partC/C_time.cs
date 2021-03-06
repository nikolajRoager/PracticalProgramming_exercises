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


        int wh = 0;
        if (!int.TryParse(argv[0],out wh))
        {
            Error.WriteLine("Input not valid, could not convert  "+argv[0]+" to int");
            return 1;
        }

        Error.WriteLine($"Testing random matrices from 2x2 to {wh}x{wh} in steps of 1 (internal C# timer method)");

        for (int n = 10; n<wh;n+=10)
        {

            Error.WriteLine($"Internal timer matrix {n}x{n} (out of  {wh}x{wh})");
            DateTime start = DateTime.UtcNow;

            matrix A=new matrix(n,n);
            A.randomize();

            matrix I =new matrix(n,n);


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            (matrix Q,matrix R) = A.getQR();

            //Need to do something with these, otherwise they would get thrown away by the compiler optimizations
//            Error.WriteLine($"{n} "+(A.approx(Q*R) ? "PASS" : "FAIL"));

            stopwatch.Stop();
            TimeSpan stopwatchElapsed = stopwatch.Elapsed;

            if (n!=2)//The very first one seems to have some problems, must be a result of compiler optimization in all the later runs, this means I can't trust that point
                WriteLine($"{n}\t{Convert.ToInt32(stopwatchElapsed.TotalMilliseconds)}\t{1}");//Error of one milli second, as the time is returned as integers

        }

        return 0;
    }

}
