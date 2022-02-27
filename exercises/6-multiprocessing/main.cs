using System;
using System.Threading;
using static System.Console;

class main
{
    public class data
    {
        public ulong a;
        public ulong b;
        public double sum;
    }

    //harmnoic sum
    public static void harmonic(object obj)
    {
        data d = (data)obj;
        d.sum=0;
        for (ulong i =d.a; i<d.b; ++i)
            d.sum+=1.0/i;

        WriteLine($"harmonic sum from {d.a} to {d.b-1} is {d.sum}");
    }

    public static void Main(string[] args)
    {
        ulong N = (ulong)1e8;
        if (args.Length>0)
            N = (ulong)double.Parse(args[0]);
        WriteLine($"N={(float)N}");
        data x = new data();
        data y = new data();
        data v = new data();
        data w = new data();
        x.a=1;
        x.b=N/4;
        y.a=x.b;
        y.b=2*N/4;
        v.a=y.b;
        v.b=3*N/4;
        w.a=v.b;
        w.b=N+1;

        Thread t1 = new Thread(harmonic);
        Thread t2 = new Thread(harmonic);
        Thread t3 = new Thread(harmonic);
        Thread t4 = new Thread(harmonic);

        t1.Start(x);
        t2.Start(y);
        t3.Start(v);
        t4.Start(w);
        t1.Join();
        t2.Join();
        t3.Join();
        t4.Join();

        WriteLine($"harmonic sum from 1 to {N} is {x.sum+y.sum+v.sum+w.sum}");

    }

}
