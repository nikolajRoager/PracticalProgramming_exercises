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

            doublelist Out = new doublelist(12);

            double m0=1;
            double m1=1;
            double m2=1;

            double G=1;



            //Upload velocity -> derivative of position at once
            Out[0]  = data[2];
            Out[1]  = data[3];

            Out[4]  = data[6];
            Out[5]  = data[7];

            Out[8]  = data[10];
            Out[9] = data[11];

            vec pos0 = new vec(data[0],data[1]);
            vec pos1 = new vec(data[4],data[5]);
            vec pos2 = new vec(data[8],data[9]);

            vec Acc0 = (G*m1/dot(pos0-pos1,pos0-pos1)*(pos1-pos0)/System.Math.Sqrt(dot(pos0-pos1,pos0-pos1))+G*m2/dot(pos0-pos2,pos0-pos2)*(pos2-pos0)/System.Math.Sqrt(dot(pos0-pos2,pos0-pos2)) );

            vec Acc1 = ( -G*m0/dot(pos0-pos1,pos0-pos1)*(pos1-pos0)/System.Math.Sqrt(dot(pos0-pos1,pos0-pos1))+G*m2/dot(pos1-pos2,pos1-pos2)*(pos2-pos1)/System.Math.Sqrt(dot(pos1-pos2,pos1-pos2)) );

            vec Acc2 = (G*m1/dot(pos2-pos1,pos2-pos1)*(pos1-pos2)/System.Math.Sqrt(dot(pos2-pos1,pos2-pos1))-G*m2/dot(pos0-pos2,pos0-pos2)*(pos2-pos0)/System.Math.Sqrt(dot(pos0-pos2,pos0-pos2)) );


            Out[2] = Acc0.x;
            Out[3] = Acc0.y;

            Out[6] = Acc1.x;
            Out[7]= Acc1.y;

            Out[10]= Acc2.x;
            Out[11]= Acc2.y;

            return Out;
        };





        doublelist planets0 = new doublelist(12);
        //Planet 0
        //position
        planets0[0] = 0.97000436;
        planets0[1] = -0.24308753;

        //velocity
        planets0[2] = 0.5*0.93240737;
        planets0[3] = 0.5*0.86473146;

        //Planet 1
        //position
        planets0[4] = -0.97000436;
        planets0[5] = 0.24308753;


        //velocity
        planets0[6] = 0.5*0.93240737;
        planets0[7] = 0.5*0.86473146;

        //Planet 2
        //position
        planets0[8] = 0;
        planets0[9] = 0;

        planets0[10] = -0.93240737;
        planets0[11] = -0.86473146;



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
