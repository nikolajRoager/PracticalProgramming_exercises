using System;
using static System.Console;
using static System.Math;
using static System.Math;
using System.Collections.Generic;//List

public static class main
{
    private class cspline
    {
        private double[] xdata;
        private double[] ydata;

        //Has s_i = a_i Dx^3+b_i Dx^2 + c Dx  y, where Dx = x-x0
        private double[] a;
        private double[] b;
        private double[] c;
        private double[] Integrals;//Pre computed integrals of the previous segment before this segment

        public cspline(double[] x, double[] y)
        {
/*
typedef struct {int n ; double ∗x , ∗y , ∗b , ∗c , ∗d ; } c u b i c s p l i n e ;
c u b i c s p l i n e ∗ c u b i c s p l i n e a l l o c ( int n , double ∗x , double ∗y )
{// b u i l d s natural cubic spline
c u b i c s p l i n e ∗ s = ( c u b i c s p l i n e ∗) m a l l o c ( sizeof ( c u b i c s p l i n e ) ) ;
s−>x = ( double∗) m a l l o c ( n∗sizeof ( double ) ) ;
s−>y = ( double∗) m a l l o c ( n∗sizeof ( double ) ) ;
s−>b = ( double∗) m a l l o c ( n∗sizeof ( double ) ) ;
s−>c = ( double∗) m a l l o c ( ( n−1)∗sizeof ( double ) ) ;
s−>d = ( double∗) m a l l o c ( ( n−1)∗sizeof ( double ) ) ;
s−>n = n ; for ( int i =0; i <n ; i ++){s−>x [ i ]= x [ i ] ; s−>y [ i ]= y [ i ] ; }
double h [ n −1 ] , p [ n −1 ] ; // VLA
for ( int i =0; i <n −1; i ++){h [ i ]= x [ i +1]−x [ i ] ; a s s e r t ( h [ i ] >0 ) ; }
for ( int i =0; i <n −1; i ++) p [ i ] = ( y [ i +1]−y [ i ] ) / h [ i ] ;
}
return s ;
}
double c u b i c s p l i n e e v a l ( c u b i c s p l i n e ∗s , double z ) {
a s s e r t ( z>=s−>x [ 0 ] && z<=s−>x [ s−>n −1 ] ) ;
int i =0 , j=s−>n −1; // binary search for the i n t e r v a l for z :
while( j −i >1){int m=( i+j ) / 2 ; i f ( z>s−>x [m] ) i=m; else j=m; }
double h=z−s−>x [ i ] ; // c a l c u l a t e the inerpolating spline :
return s−>y [ i ]+h ∗( s−>b [ i ]+h ∗( s−>c [ i ]+h∗s−>d [ i ] ) ) ;
}
*/


            xdata=x;
            int n=xdata.Length;
            ydata=y;

            //Prepare the segments
            a         = new double[n-1];
            b         = new double[n-1];
            c         = new double[n];
            Integrals = new double[n-1];

            Integrals[0]=0;



            //zeroth pass, get xi+1-xi and slopes, also check ascending order
            //Same notation as in the note, even though it is not super obvious in the naming convention
            double[] p = new double[n-1 ];
            double[] h = new double[n-1 ];
            for (int i =0; i <n-1; ++i)
            {
                h[i]  = x[i+1]-x[i];
                if (h[i]<0)
                    throw new System.Exception($"Invalid data, must be ordered");
                p[i] = (y[i+1]-y[i])/h[i];
            }


            //Diagonal and one of diagonal elements
            double[] D = new double[n];
            double[] Q = new double[n-1];
            double[] B = new double[n] ;
            //Set up from recursive formulas
            D[0] = 2;
            for ( int i =0; i <n -2; ++i)
            {
                D[i+1]=2*h[i]/h[i+1]+2;
                D[n-1]=2;
            }
            Q[0] = 1;
            for ( int i =0; i <n-2; ++i)
                Q[i+1]=h[i]/h[i+1];

            for (int i =0; i <n-2; ++i)
                B[i+1]=3*(p[i]+p[i+1]*h[i]/h[i+1]);
            B[0] = 3*p[0];
            B[n-1]=3*p[n-2];

            //Now run Guass elimination
            for ( int i =1; i <n ; ++i)
            {
                D[i]-=Q[i-1]/D[i-1] ;
                B[i]-=B[i-1]/D[i-1];
            }
            //Now read the data
            c[n-1]=B[n-1]/D[n-1];
            for ( int i=n-2; i >=0; --i)
                c[i]=(B[i]-Q[i]*c[i+1])/D[i] ;

            for (int i =0; i <n-1; ++i)
            {
                b[ i ]=(-2*c[ i ]- c[ i +1]+3*p [ i ])/h [ i ] ;
                a[ i ]=(   c[ i ]+ c[ i +1]-2*p[i] )  /(h[ i ]*h [ i ]);
            }


            for (int i = 0; i < n-2; ++i)
            {

                //See the integral function below for why this is defined as it is
                double tempD=ydata[i]+xdata[i]*(-c[i]+xdata[i]*(b[i]-xdata[i]*a[i]));
                double tempC=c[i]+xdata[i]*(-2*b[i]+3*xdata[i]*a[i]);
                double tempB=b[i]-3*xdata[i]*a[i];

                Integrals[i+1]=Integrals[i]+
                tempD*xdata[i+1]+xdata[i+1]*xdata[i+1]*tempC/2+xdata[i+1]*xdata[i+1]*xdata[i+1]*tempB/3+0.25*xdata[i+1]*xdata[i+1]*xdata[i+1]*xdata[i+1]*a[i]
                -(tempD*xdata[i]+xdata[i]*xdata[i]*tempC/2+xdata[i]*xdata[i]*xdata[i]*tempB/3+0.25*xdata[i]*xdata[i]*xdata[i]*xdata[i]*a[i]);

            }
        }

        public double spline(double x)
        {
            int i=binary_search.binsearch(x,xdata);

            //This is positive, as I checked on creation of the object
            //Qubic interpolation, using offset x coordinates
            double deltax=x-xdata[i];
            return ydata[i]+deltax*(c[i]+deltax*(b[i]+deltax*a[i]));

        }
        public double integral(double x)
        {
            int i=binary_search.binsearch(x,xdata);
            //Integral from xdata[i] to x of  ydata[i]+(x-xdata[i])*b[i]+(x-xdata[i])^2*a[i]; plus the earlier integral
            //indef Integral ydata[i]+deltax*(c[i]+deltax*(b[i]+deltax*a[i]));

            //I am too stupid to actually calculate this without making mistakes, so the first step is to do write it as Ax^3+Bx^2+Cx+D
            double D=ydata[i]+xdata[i]*(-c[i]+xdata[i]*(b[i]-xdata[i]*a[i]));
            double C=c[i]+xdata[i]*(-2*b[i]+3*xdata[i]*a[i]);
            double B=b[i]-3*xdata[i]*a[i];

            return Integrals[i]+
            D*x+x*x*C/2+x*x*x*B/3+0.25*x*x*x*x*a[i]
            -(D*xdata[i]+xdata[i]*xdata[i]*C/2+xdata[i]*xdata[i]*xdata[i]*B/3+0.25*xdata[i]*xdata[i]*xdata[i]*xdata[i]*a[i]);

        }
        public double derivative(double x)
        {
            int i=binary_search.binsearch(x,xdata);
            double deltax = x-xdata[i];
            //Derivative w respect to x of
            //return ydata[i]+deltax*c[i]+deltax^2*b[i]+deltax^3*a[i];
            return  c[i]+deltax*(2*b[i]+3*deltax*a[i]);
        }

    }

    public static int Main(string[] argv)
    {
        if (argv.Length!=1)
        {
            Error.WriteLine("Input not valid, need 1 argument (input data)");
            return 1;
        }
        Error.WriteLine("Running qubic interpolation, loading data");

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


        Error.WriteLine("Setting up cubic interpolation");
        //Don't need to precalc anything but I like to upload the data anyway
        cspline myCspline = new cspline(xlist,ylist);

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
            double y = myCspline.spline(x);

            num_integral+=deltaX*y;

            if (i!=0)
                num_derivative = (y-py)/(x-px);
            else
            {
                //Get the future data point instead
                double fx = (1+i)*deltaX+a;//will be cast to double on its own
                double fy = myCspline.spline(fx);
                num_derivative = (fy-y)/(fx-x);
            }

            WriteLine($"{x}\t{y}\t{myCspline.integral(x)}\t{num_integral}\t{myCspline.derivative(x)}\t{num_derivative}");

            py=y;
            px=x;
        }

        return 0;
    }

}
