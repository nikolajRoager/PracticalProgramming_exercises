using System;
using static System.Console;
using static System.Math;


class main
{


    public static int Main(string[] argv)
    {
        if (argv.Length!=2)
        {
            Error.WriteLine("Illegal input, need output_erf output_bessel");
            return 1;
        }
        Func<double,double> LogPerSqrt = delegate(double t){return Log(t)/Sqrt(t);};

        uint steps =0;
        Func<double,double> inv_expX2 = delegate(double x){++steps;return Exp(-x*x);};


        WriteLine($"integral of Log(x)/sqrt(x) from 0 to 1= {integrate.quad(f:LogPerSqrt ,a:0,b:1,acc:1e-6,eps:0)}");


        var writer1 = new System.IO.StreamWriter(argv[0]);
        var writer2 = new System.IO.StreamWriter(argv[1]);

        for(double z=-3;z<=3;z+=1.0/8)//The limits from the lecture will suffice
        {



            Func<double,double> bessel1_integrand = delegate(double t){return Cos(t-z*Sin(t));};

            double bessel1 = integrate.quad(f:bessel1_integrand ,a:0,b:PI,acc:1e-6,eps:0)/PI;
            writer2.WriteLine($"{z} {bessel1}");

            steps =0;
            double erf = 2/Sqrt(PI)* integrate.quad(f:inv_expX2,a:0,b:z,acc:1e-6,eps:0);

            uint steps1 = steps;
            steps =0;

            double erf2 = 2/Sqrt(PI)*integrate.quad(f:inv_expX2,a:z,b:double.PositiveInfinity,acc:1e-6,eps:0);
            writer1.WriteLine($"{z} {erf} {steps1} {erf2} {steps}");
        }

        writer2.Close();
        writer1.Close();
        return 0;
    }
}
