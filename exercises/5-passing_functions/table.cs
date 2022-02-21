using static System.Console;//Writeline
using System;//Func

public static class table
{
    public static void table_header(string A, string B)
    {
        WriteLine(A+"    "+B);
	}

    public static void make_table(Func<double,double> f, double a, double b, double dx)
    {
        for(double x=a;x<=b;x+=dx)
            WriteLine($"{x}     {f(x)}");
	}
}
