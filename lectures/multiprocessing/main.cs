using System;
using System.Threading;
using static System.Console;
using System.Threading.Tasks;

class main
{

    public static void Main(string[] args)
    {
        int N = (int)1e8;
        if (args.Length>0)
            N = (int)double.Parse(args[0]);
        double[] a = new double[N];
        WriteLine($"N={(float)N}");
        double sum = 0;
        Parallel.For(0,(int) (N), i=> a[i]=Math.Sin(i)*Math.Cos(i) );
//        for (int i = 0; i < N; ++i)
//            a[i]=Math.Sin(i)*Math.Cos(i);


        //WriteLine($"Sum = {sum}");

    }

}
