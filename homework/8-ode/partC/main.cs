using System;
using static System.Console;
using static ode;
using static vec;

public static class main
{
    public static int Main()
    {

        //Here data=(pos0.xyz,v0.xyz, pos1.xyz, v1.xyz, pos2.xyz, v2.xyz)
        System.Func<double,doublelist,doublelist> ODEtribody = delegate(double x,doublelist data)
        {

            doublelist Out = new doublelist(18);

            double m0=1;
            double m1=10;
            double m2=0;

            double G=1;



            //Upload velocity -> derivative of position at once
            Out[0]  = data[3];
            Out[1]  = data[4];
            Out[2]  = data[5];
            Out[6]  = data[9];
            Out[7]  = data[10];
            Out[8]  = data[11];
            Out[12] = data[15];
            Out[13] = data[16];
            Out[14] = data[17];

            vec pos0 = new vec(data[0],data[1],data[2]);
            vec pos1 = new vec(data[6],data[7],data[8]);
            vec pos2 = new vec(data[12],data[13],data[14]);

            vec Acc0 = (G*m1/dot(pos0-pos1,pos0-pos1)*(pos1-pos0)/System.Math.Sqrt(dot(pos0-pos1,pos0-pos1))+G*m2/dot(pos0-pos2,pos0-pos2)*(pos2-pos0)/System.Math.Sqrt(dot(pos0-pos2,pos0-pos2)) );

            Out[3] = Acc0.x;
            Out[4] = Acc0.y;
            Out[5] = Acc0.z;
            Out[9] =0;
            Out[10]=0;
            Out[11]=0;
            Out[15]=0;
            Out[16]=0;
            Out[17]=0;

            return Out;
        };


        doublelist planets0 = new doublelist(18);
        planets0[0] = -5;
        planets0[1] = 0;
        planets0[2] = 0;
        planets0[3] = 0;
        planets0[4] = 1.0;
        planets0[5] = 0;
        planets0[6] = 0;
        planets0[7] = 0;
        planets0[8] = 0;
        planets0[9] = 0;
        planets0[10] = 0;
        planets0[11] = 0;
        planets0[12] = 0;
        planets0[13] = 0;
        planets0[14] = 0;
        planets0[15] = 0;
        planets0[16] = 0;
        planets0[17] = 0;

        genlist<doublelist> planets = new genlist<doublelist>();
        doublelist t_list = new doublelist();
        (planets , t_list ) =ode.driver(
            ODEtribody,
            0,
            50,
            planets0,
            0.01,  //h initial
            0.0001,//Absolute error
            0.0001,//Relative and absol
            1.0
        );

        for (int i = 0; i<planets.size; ++i)
        {
            WriteLine($"{t_list[i]}\t{planets [i]}");
        }

        return 0;
    }

}
