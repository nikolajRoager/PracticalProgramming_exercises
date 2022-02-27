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
        x.a=1;
        x.b=N;

        Thread t1 = new Thread(harmonic);

        t1.Start(x);
        t1.Join();

        WriteLine($"harmonic sum from 1 to {N} is {x.sum+y.sum}");

    }

}
