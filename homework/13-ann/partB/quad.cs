using System;
using static System.Math;
//using static System.Console;

public static class quad
{

    public static (double,double,int) integrateCC(Func <double,double> F, double x_min, double x_max, double abs_acc=1e-7, double rel_acc=1e-7, double point_1=double.NaN, double point_2=double.NaN)
    {
        Func <double,double> FF = theta => F((x_min+x_max)/2.0+(x_max-x_min)/2.0*Cos(theta) )*Sin(theta)*(x_max-x_min)/2.0;
        return integrate(FF,0,PI,abs_acc,rel_acc);
    }

    public static (double,double,int) integrate(Func <double,double> F, double x_min, double x_max, double abs_acc=1e-7, double rel_acc=1e-7, double point_1=double.NaN, double point_2=double.NaN)

    {
        if (double.IsInfinity(x_min) || double.IsInfinity(x_max))
        {

            if (double.IsNegativeInfinity(x_min) && double.IsPositiveInfinity(x_max))
            {
                Func <double,double> FF = t => (F((1-t)/t)+F(-(1-t)/t))/(t*t);

                return integrate(FF,0,1,abs_acc,rel_acc);
            }
            else if (double.IsPositiveInfinity(x_max))
            {

                Func <double,double> FF = t => F(x_min+(1-t)/t)/(t*t);

                return integrate(FF,0,1,abs_acc,rel_acc);
            }
            else if (double.IsNegativeInfinity(x_min))
            {
                Func <double,double> FF = t => F(x_max-(1-t)/t)/(t*t);

                return integrate(FF,0,1,abs_acc,rel_acc);
            }
            //If we got here something is wrong
            throw new Exception("Infinite limits not among allowed options");
        }
        else
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

            double Q = ((2*point_0+point_1+point_2+2*point_3)/6)*(x_max-x_min);
            double q = ((point_0+point_1+point_2+point_3)/4)*(x_max-x_min);
            double err = Abs(Q-q);



            //If within error return
            if (err <= Max(abs_acc,rel_acc*Abs(Q)))
            {

                return (Q,err,1);
            }
            //Otherwise integrate the two half-intervals, notably the old 1/6 and 2/6 way points are now the new 2/6 and 4/6 points for the first halves, and the old 4/6 and 5/6 points are now the new 2/6 and 4/6 points:

     //Old points  I   I        I   I
              //-I-I---I-I----I-I---I-I-
    //New points I I   I I    I I   I I
            else
            {
                double Q0, Q1;
                double err0, err1;
                int itr0,itr1;

    //            System.Console.WriteLine($" Reject");
    //            System.Console.WriteLine($"Between {x_min} and {x_max}");
    //            System.Console.WriteLine($"Points having {point_0} {point_1} {point_2} {point_3}");
    //            System.Console.WriteLine($"Got do {Q} and {q}, so error {err} versus tolerance {Max(abs_acc,rel_acc*Abs(Q))}");
                //Scaling absolute error by 1/sqrt(2) BREAKS EVERYTHING, the result is a truly infinite loop where the accuracy criterion races to 0 faster than the error, resulting in a stack-overflow
                (Q0,err0,itr0) = integrate(F,x_min,(x_max+x_min)/2,abs_acc/Sqrt(2),rel_acc,point_0,point_1);
                (Q1,err1,itr1) = integrate(F,(x_min+x_max)/2,x_max,abs_acc/Sqrt(2),rel_acc,point_2,point_3);
                //Assume the errors are unrelated
                return (Q0+Q1,Sqrt(err0*err0+err1*err1),itr0+itr1);
            }

        }
    }

}
