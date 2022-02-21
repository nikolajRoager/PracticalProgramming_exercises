using System;
using static System.Console;

class main
{
    public delegate double fun_of_3_doubles(double x, double y, double z);

    public static Func<double> make_fa()
    {
        double a=0;
        Func<double> fa = delegate(){a++;return a;};
        return fa;
    }

    public static void Main()
    {
        System.Func<double> fun = delegate(){return 7;};
        Func<double,double> square = delegate(double x){return x*x;};
        Action hello = delegate(){WriteLine("hello");};
        hello();

        fun_of_3_doubles f3 = delegate(double x, double y, double z){return 9;};
        fun_of_3_doubles f4 = delegate(double x, double y, double z){return 4;};

        double a=0;
        Action fa = delegate(){a++;};

        WriteLine($"fun = {fun()}");
        WriteLine($"square(2) = {square(2)}");
        WriteLine($"f3(1,2,3) = {f3(1,2,3)}");
        WriteLine($"f4(1,2,3) = {f3(1,2,3)}");
        fa();
        fa();
        fa();
        WriteLine($"a = {a}");


        Func<double> fb = make_fa();
        WriteLine($"fb() = {fb()}");
        WriteLine($"fb() = {fb()}");
        WriteLine($"fb() = {fb()}");
        Func<double> fc = make_fa();
        WriteLine($"fc() = {fc()}");
        WriteLine($"fc() = {fc()}");
    }


}
