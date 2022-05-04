using System;
using static System.Console;
using static ode;

public static class main
{
    public static int Main()
    {
        //As I can not know how many parameters are needed in y, it is represented by a "doublelist", an arbitrary length function of doubles
        //looking for representing y'' = -y, need data=(y,y')
        System.Func<double,doublelist,doublelist> ODEsin = delegate(double x,doublelist data)
        {

            doublelist Out = new doublelist(2);
            Out[0] = data[1];//y' = y'
            Out[1] = -data[0];// y'' = -y
            return Out;
        };


        doublelist sin0 = new doublelist(2);
        sin0[0] = 0;//sin(0)
        sin0[1] = 1;//sin'(0)=cos(0)

        genlist<doublelist> sin_x = new genlist<doublelist>();
        doublelist x_list0 = new doublelist();
        (sin_x, x_list0) =ode.driver(
            ODEsin,
            0,
            System.Math.PI*4,
            sin0,
            0.01,  //h initial
            0.0001,//Absolute error
            0.0001,//Relative and absol
            0.2    //Max stepsize
        );

        for (int i = 0; i<sin_x.size; ++i)
        {
            WriteLine($"{x_list0[i]}\t{sin_x[i][0]}\t{sin_x[i][1]}\t{System.Math.Sin(x_list0[i])}");
        }


        //Here data=(theta,omega)
        System.Func<double,doublelist,doublelist> ODEpendulum = delegate(double x,doublelist data)
        {

            doublelist Out = new doublelist(2);
            Out[0] = data[1];
            Out[1] = -0.25*data[1]-5.0*System.Math.Sin(data[0]);
            return Out;
        };


        doublelist pend0 = new doublelist(2);
        pend0[0] = System.Math.PI - 0.1;
        pend0[1] = 0;

        genlist<doublelist> pend = new genlist<doublelist>();
        doublelist pend_t_list = new doublelist();
        (pend , pend_t_list ) =ode.driver(
            ODEpendulum,
            0,
            10,
            pend0,
            0.01,  //h initial
            0.0001,//Absolute error
            0.0001,//Relative and absol
            0.2    //Max stepsize
        );

        for (int i = 0; i<pend_t_list.size; ++i)
        {
            Error.WriteLine($"{pend_t_list [i]}\t{pend [i][0]}\t{pend [i][1]}");
        }

        return 0;
    }

}
