using static table;//make_table and table_header
using System;//Func
using static System.Math;//Sin

class main
{
    static public int Main()
    {
        double k=1.0;
        Func<double,double> SinK  = delegate(double x){return Sin(k*x);};

        double[] ks = {1.0,2.0,3.0};
        foreach (double K in ks)
        {
            k=K;
            table_header("x ",$"sin({k} x)");
            make_table(SinK,0,2*PI,PI/9);
        }

        return 0;
    }

}
