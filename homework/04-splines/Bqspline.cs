using System;
using static System.Console;
using static System.Math;
using static System.Math;
using System.Collections.Generic;//List

public static class main
{
    private class qspline
    {
        private double[] xdata;
        private double[] ydata;

        //Has s_i = a_i Dx^2+b Dx + y, where Dx = x-x0
        private double[] a;
        private double[] b;
        private double[] Integrals;//Pre computed integrals of the previous segment before this segment

        public qspline(double[] x, double[] y)
        {
/*return s ; }
double qsplineeval(qspline ∗s , double z ) { // evaluates s ( z )
assert ( z>=s−>x [ 0 ] && z<=s−>x [ s−>n −1 ] ) ;
int i =0 , j=s−>n −1; // binary search :
while( j −i >1){int m=( i+j ) / 2 ; i f ( z>s−>x [m] ) i=m; else j=m; }
*/


            //Check data is in ascending order
            xdata=x;
            int n=xdata.Length;
            double px = xdata[0];
            foreach (double X in xdata)
            {
                if (px>X)
                    throw new System.Exception($"Invalid data, must be ordered");
                px=X;
            }
            ydata=y;

            //Prepare the segments
            a         = new double[n-1];
            b         = new double[n-1];
            Integrals = new double[n-1];

            Integrals[0]=0;



            //zeroth pass, get xi+1-xi and slopes
            double[] deltaxy = new double[n-1 ];
            double[] deltax = new double[n-1 ];
            for (int i =0; i <n-1; ++i)
            {
                deltax[i]  = x[i+1]-x[i];
                deltaxy[i] = (y[i+1]-y[i])/deltax[i];
            }

            //First guess that the slope is equal to the slope component b[i] (as is done in the example)

            //Guess 1: a[0]=0, use recursion to see what we would get
            a[0] = 0 ; // recursion up :

            for (int i = 0; i< n-2; ++i) a[i+1]= (deltaxy[i+1]-deltaxy[i]-a[i]*deltax[i])/deltax[i+1];
            //for (int i =0;i< n-2; ++i)s−>c [ i +1]=(p [ i +1]−p [ i ]−s−>c [ i ] ∗h [ i ] ) / h [i + 1 ];
            a[n-2]/=2; //Guess a[max]=0 and take the aerage of what we had before
            for (int i=n-3; i <0; i--) a[i] = ( deltaxy[ i +1]-deltaxy[i]-a[ i +1]*deltax [ i + 1 ] ) / deltax[ i ] ;

            //Fix the slope
            for (int i =0; i<n-1; ++i) b[ i ]=deltaxy[i]-a[i]*deltax[i];


            for (int i = 0; i < n-1; ++i)
            {

                //See the integral function below for why this is defined as it is
                if (i<n-2)
                    Integrals[i+1]= Integrals[i]+
                 xdata[i+1]* ((ydata[i]-xdata[i]*b[i]+xdata[i]*xdata[i]*a[i]) +   xdata[i+1]*(0.5*b[i]-xdata[i]*a[i]  + xdata[i+1]*a[i]/3 ))
                -xdata[i]* ((ydata[i]-xdata[i]*b[i]+xdata[i]*xdata[i]*a[i]) +  xdata[i] *(0.5*b[i]-xdata[i]*a[i]  + xdata[i]*a[i]/3 ));
            }
        }

        public double spline(double x)
        {
            int i=binary_search.binsearch(x,xdata);

            //This is positive, as I checked on creation of the object
            //Quadratic interpolation, using offset x coordinates
            double deltax=x-xdata[i];
            return ydata[i]+deltax*(b[i]+deltax*a[i]);

        }
        public double integral(double x)
        {
            int i=binary_search.binsearch(x,xdata);
            //Integral from xdata[i] to x of  ydata[i]+(x-xdata[i])*b[i]+(x-xdata[i])^2*a[i]; plus the earlier integral
            //indef Integral x ydata[i]+(x-xdata[i])*b[i]+(x-xdata[i])^2*a[i]; plus the earlier integral
            //=Integral of ydata[i]-xdata[i]*b[i]+xdata[i]^2*a[i]+  x*b[i]-2*x*xdata[i]*a[i]  +x^2*a[i];
            //=x*(ydata[i]-xdata[i]*b[i]+xdata[i]^2*a[i]) +  0.5 x*x(*b[i]-2*xdata[i]*a[i])  + x^3*a[i]/3
            //=x* ((ydata[i]-xdata[i]*b[i]+xdata[i]^2*a[i]) +   x*(0.5*b[i]-xdata[i]*a[i]  + x*a[i]/3 ))
            return Integrals[i]+
             x* ((ydata[i]-xdata[i]*b[i]+xdata[i]*xdata[i]*a[i]) +   x*(0.5*b[i]-xdata[i]*a[i]  + x*a[i]/3 ))
            -xdata[i]* ((ydata[i]-xdata[i]*b[i]+xdata[i]*xdata[i]*a[i]) +  xdata[i] *(0.5*b[i]-xdata[i]*a[i]  + xdata[i]*a[i]/3 ));
        }
        public double derivative(double x)
        {
            int i=binary_search.binsearch(x,xdata);
            //Derivative w respect to x of
            //ydata[i]+(x-xdata[i])*b[i]+(x-xdata[i])^2*a[i];
            return b[i]+2*(x-xdata[i])*a[i];
        }

    }

    public static int Main(string[] argv)
    {
        if (argv.Length!=1)
        {
            Error.WriteLine("Input not valid, need 1 argument (input data)");
            return 1;
        }
        Error.WriteLine("Running quadratic interpolation, loading data");

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
            int i = 0;
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


        Error.WriteLine("Setting up qubic interpolation");
        //Don't need to precalc anything but I like to upload the data anyway
        qspline myQspline = new qspline(xlist,ylist);

        double a =xlist[0];
        double b =xlist[xlist.Length-1];
        int N = 256;
        double deltaX=(b-a)/N;

        //Do calculate a numerical integral, to compare the integral to
        double num_integral = 0;
        //Also calculate the numerical derivative
        double px=xlist[0];
        double py=ylist[0];
        double num_derivative=0;
        for (int i = 0; i <= N; ++i)
        {
            double x = i*deltaX+a;//will be cast to double on its own
            double y = myQspline.spline(x);

            num_integral+=deltaX*y;

            if (i!=0)
                num_derivative = (y-py)/(x-px);
            else
            {
                //Get the future data point instead
                double fx = (1+i)*deltaX+a;//will be cast to double on its own
                double fy = myQspline.spline(fx);
                num_derivative = (fy-y)/(fx-x);
            }

            WriteLine($"{x}\t{y}\t{myQspline.integral(x)}\t{num_integral}\t{myQspline.derivative(x)}\t{num_derivative}");

            py=y;
            px=x;
        }

        return 0;
    }

}
