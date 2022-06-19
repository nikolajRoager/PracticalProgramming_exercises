using System;
using static System.Console;
using static System.Math;
using static Nlinear;

public static class main
{
    public static int Main(string[] argv)
    {
        Error.WriteLine("-----------------------------");
        //I use the "error" channel for normal logging stuff
        Error.WriteLine("Setting up data, build in checkerboard example");

        //3D cube example

        //Declare the lists before loading them, I want them to be seen outside the try environment
        vector xlist=new vector(4);
        vector ylist=new vector(3);
        vector zlist=new vector(2);
        double[] data = //Format, {data_000,data_001,data_002, ... data_010, data_011,... }
        {
            1, 0,1, 0,
            0, 1,0, 1,
            1, 0,1, 0,
            0, 1,0, 1,
            1, 0,1, 0,
            0, 1,0, 1
        }
        ;

        vector[] axes = new vector[3];

        xlist[0] = -2;
        xlist[1] = 0;
        xlist[2] = 1;
        xlist[3] = 2;
        ylist[0] = 0;
        ylist[1] = 1;
        ylist[2] = 2;
        zlist[0] = -1;
        zlist[1] = 1;

        axes[0]=xlist;
        axes[1]=ylist;
        axes[2]=zlist;


        Error.WriteLine("Setting up N-linear interpolation");
        Nlinear interpolator= new Nlinear(axes,data);
        Error.WriteLine("Verifying the solution for all boundary conditions ...");
        if (!interpolator.verify_all())
            return 1;
        else
            Error.WriteLine(" --- PASS: Solution matches all boundary conditions within relative and absolute error 10^-5");

        Error.WriteLine("Calculating and saving high resolution output");

        //Write the 3 dimensional output to a number of matrices after one-another

        vector xout = new vector(21);

        for (int i = 0; i < 21; ++i)
        {
            xout[i]=(xlist[xlist.size-1]-xlist[0])*i*1.0/(20)+xlist[0];
        }

        for (int k = 0; k < 5; ++k)
        {
            Write(21);
            for (int i = 0; i < 21; ++i)
                Write($" {xout[i]}");

            double z =(zlist[zlist.size-1]-zlist[0])*k*1.0/(4)+zlist[0];
            Write("\n");
            for (int i = 0; i < 16; ++i)
            {
                double y =(ylist[ylist.size-1]-ylist[0])*i*1.0/(15)+ylist[0];
                Write($" {y}");
                for (int j = 0; j < 21; ++j)
                {
                    double[] point = {xout[j],y,z};
                    Write($" {interpolator.interpolate(point)}");
                }
                Write("\n");
            }
            Write("\n");

        }


        Error.WriteLine("-----------------------------");
        return 0;
    }

}
