using System;
using static System.Console;
using static System.Math;
using static System.Math;
using System.Collections.Generic;//List

public static class main
{
    //Calculate a class, storing all the parameters
    private class rect_bilerp
    {
        //Width and height of the grid
        private int n;
        private int m;
        private double x0,width;//As the x and y grid points are evenly spaced, no need to store them, this also makes it easier to see where we are
        private double y0,height;

        private double[][] z;//The data of all the vertices, which we want to interpolate


        //z(d_x,d_y) = a d_x+b d_y+c d_x d_y+d
        private double[][] a;
        private double[][] b;
        private double[][] c;
        private double[][] d;

        public rect_bilerp(double _x0, double _y0, double w, double h, double[][] zdata)
        {
            n = zdata.Length;
            m = zdata[0].Length;

            z = zdata;

            //Verify that the data
            foreach (double X in xdata)
            {
                if (px>X)
                    throw new System.Exception($"Invalid data, must be ordered");
                px=X;
            }
            ydata=y;

            a         = new double[x.Length-1];
            b         = new double[x.Length-1];
            Integrals = new double[x.Length-1];

            Integrals[0]=0;
            for (uint i = 0; i < x.Length-1; ++i)
            {

                double dx=xdata[i+1]-xdata[i];
                double dy=ydata[i+1]-ydata[i];

                a[i]=dy/dx;
                b[i]=ydata[i]-a[i]*xdata[i];

                if (i<x.Length-2)
                    Integrals[i+1]= Integrals[i]+xdata[i+1]*(0.5*xdata[i+1]*a[i]+b[i])-xdata[i]*(0.5*xdata[i]*a[i]+b[i]);
            }
        }

        public double spline(double x)
        {
            int i=binary_search.binsearch(x,xdata);

            //This is positive, as I checked on creation of the object
            //Linear interpolation is pretty simple
            return a[i]*x+b[i];

        }
        public double integral(double x)
        {
            int i=binary_search.binsearch(x,xdata);
            return Integrals[i]+x*(0.5*x*a[i]+b[i])-xdata[i]*(0.5*xdata[i]*a[i]+b[i]);
        }
        public double derivative(double x)
        {
            int i=binary_search.binsearch(x,xdata);
            return a[i];
        }

    }

    public static int Main(string[] argv)
    {
        if (argv.Length!=1)
        {
            Error.WriteLine("Input not valid, need 1 argument (input data)");
            return 1;
        }
        Error.WriteLine("Running linear interpolation, loading data");

        //Declare the lists before loading them, I want them to be seen outside the environment
        double[] xlist;
        double[] ylist;

        try
        {
            //Read data into arrays
            string[] lines = System.IO.File.ReadAllLines(argv[0]);
            char[] deliminators = {'\t',' ',','};
            var options = StringSplitOptions.RemoveEmptyEntries;
            xlist = new double[lines.Length];
            ylist = new double[lines.Length];
            uint i = 0;
            foreach(string line in lines )
            {

                string[] words = line.Split(deliminators,options);
                if (words.Length!=2)
                {

                    Error.WriteLine("Input not valid, 2 arguments in each line, seperate with tab, space or comma but got "+line);
                    return 1;
                }

                double x = 0;
                bool number = double.TryParse(words[0],out x);

                double y = 0;
                number = number && double.TryParse(words[1],out y);
                if (!number || double.IsNaN(x) ||double.IsNaN(y) )
                {

                    Error.WriteLine("Input not valid, could not convert to doubles: "+line);
                    return 1;
                }

                ylist[i]=y;
                xlist[i]=x;

                ++i;
            }
        }
        catch(Exception E)
        {
            Error.WriteLine("Could not load input, got error "+E);
            return 1;

        }


        Error.WriteLine("Setting up linear interpolation");
        //Don't need to precalc anything but I like to upload the data anyway
        lspline myLspline = new lspline(xlist,ylist);

        double a =xlist[0];
        double b =xlist[xlist.Length-1];
        uint N = 256;
        double deltaX=(b-a)/N;

        //Do calculate a numerical integral, to compare the integral to
        double num_integral = 0;
        //Also calculate the numerical derivative
        double px=xlist[0];
        double py=ylist[0];
        double num_derivative=0;
        for (uint i = 0; i < N; ++i)
        {
            double x = i*deltaX+a;//will be cast to double on its own
            double y = myLspline.spline(x);

            num_integral+=deltaX*y;

            if (i!=0)
                num_derivative = (y-py)/(x-px);
            else
            {
                //Get the future data point instead
                double fx = (1+i)*deltaX+a;//will be cast to double on its own
                double fy = myLspline.spline(fx);
                num_derivative = (fy-y)/(fx-x);
            }

            WriteLine($"{x}\t{y}\t{myLspline.integral(x)}\t{num_integral}\t{myLspline.derivative(x)}\t{num_derivative}");

            py=y;
            px=x;
        }

        return 0;
    }

}
