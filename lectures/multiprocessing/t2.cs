using System;
using System.Threading;
using static System.Console;

class main
{
    public class data
    {
        public int a;
        public int b;
        public double sum;
    }

    //harmnoic sum
    public static void harmonic(object obj)
    {
        data d = (data)obj;
        d.sum=0;
        for (int i =d.a; i<d.b; ++i)
            d.sum+=1.0/i;

        WriteLine($"harmonic sum from {d.a} to {d.b-1} is {d.sum}");
    }

    public static void Main(string[] args)
    {
        int N = (int)1e8;
        if (args.Length>0)
            N = (int)double.Parse(args[0]);
        WriteLine($"N={(float)N}");
        data x = new data();
        data y = new data();
        x.a=1;
        x.b=N/2;
        y.a=x.b;
        y.b=N+1;

        Thread t1 = new Thread(harmonic);
        Thread t2 = new Thread(harmonic);

        t1.Start(x);
        t2.Start(y);
        t1.Join();
        t2.Join();

        WriteLine($"harmonic sum from 1 to {N} is {x.sum+y.sum}");

    }

}
