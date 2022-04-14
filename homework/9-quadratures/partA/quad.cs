using System;
using static System.Math;
//using static System.Console;

public static class quad
{
    public static double integrate(Func <double,double> F, double x_min, double x_max, double abs_acc=1e-9, double rel_acc=1e-9, double point_1=double.NaN, double point_2=double.NaN)
    {
        //Step size
        double h=x_max-x_min;

        //Is this first iteration, if so, get the middle points
        if(double.IsNaN(point_1))
            point_1=F(x_min+2*h/6);
        if(double.IsNaN(point_2))
            point_2=F(x_min+4*h/6);

        //Get the outer points
        double point_0=F(x_min+h/6);
        double point_3=F(x_min+5*h/6);

        double Q = (2*point_0+point_1+point_2+2*point_3)/6*(x_max-x_min);
        double q = (point_0+point_1+point_2+point_3)/4*(x_max-x_min);
        double err = Abs(Q-q);

        //If within error return
        if (err <= abs_acc+rel_acc*Abs(Q))
            return Q;

        //Otherwise integrate the two half-intervals, notably the old 1/6 and 2/6 way points are now the new 2/6 and 4/6 points for the first halves, and the old 4/6 and 5/6 points are now the new 2/6 and 4/6 points:

 //Old points  I   I        I   I
          //-I-I---I-I----I-I---I-I-
//New points I I   I I    I I   I I
        else return integrate(F,x_min,(x_max+x_min)/2,abs_acc/Sqrt(2),rel_acc,point_0,point_1)+
                    integrate(F,(x_min+x_max)/2,x_max,abs_acc/Sqrt(2),rel_acc,point_2,point_3);
        return 0;
    }

}
