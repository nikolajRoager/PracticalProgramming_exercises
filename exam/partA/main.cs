using System;
using static System.Console;
using static System.Math;

public static class main
{
    //A class, pre calculating the parameters
    private class bilenear
    {
        //Width and height of the grid
        private int n;
        private int m;

        private vector x;
        private vector y;
        private matrix z;//The data of all the vertices, which we want to interpolate


        //z(d_x,d_y) = a d_x+b d_y+c d_x d_y+d
        private matrix a;
        private matrix b;
        private matrix c;
        private matrix d;

        public bilenear(vector  xdata, vector   ydata,matrix zdata)
        {
            n = ydata.size;
            m = xdata.size;

            x = xdata.copy();
            y = ydata.copy();
            z = zdata.copy();


            //Verify that the data works
            double pdata = xdata[0];
            for(int i = 1; i < m; ++i)
            {
                if (pdata>xdata[i])
                    throw new System.Exception($"Invalid data, x must be ordered");
                pdata=xdata[i];
            }
            pdata = ydata[0];
            for(int i = 0; i < n; ++i)
            {
                if (pdata>ydata[i])
                    throw new System.Exception($"Invalid data, y must be ordered");
                pdata=ydata[i];
            }

            if (n != zdata.height || m != zdata.width)
                throw new System.Exception($"Invalid data, z data size did not match y,x data size");

            a         = new matrix(n-1,m-1);
            b         = new matrix(n-1,m-1);
            c         = new matrix(n-1,m-1);
            d         = new matrix(n-1,m-1);


            for (int i = 0; i < n-1; ++i)
                for (int j = 0; j < m-1; ++j)
                {

                    //We have 4 linear equations

                    //a[i,j]*x[j  ] + b[i,j]*y[i  ] + c[i,j]*x[j  ]*y[i  ] + d[i,j]= z[i  ,j  ];
                    //a[i,j]*x[j+1] + b[i,j]*y[i  ] + c[i,j]*x[j+1]*y[i  ] + d[i,j]= z[i  ,j+1];
                    //a[i,j]*x[j  ] + b[i,j]*y[i+1] + c[i,j]*x[j  ]*y[i+1] + d[i,j]= z[i+1,j  ];
                    //a[i,j]*x[j+1] + b[i,j]*y[i+1] + c[i,j]*x[j+1]*y[i+1] + d[i,j]= z[i+1,j+1];

                    //Now I can solve those analytically... or ... time to bust out that linear equation solver I have lying around in in my homework folder

                    //Juts to be clear, this equation is A \vec{X} =\vec{B}
                    //where
                    matrix A = new matrix(4,4);
                    A[0,0] = xdata[j  ]; A[0,1] = ydata[i  ]; A[0,2]= xdata[j  ]*ydata[i  ]; A[0,3]=1;
                    A[1,0] = xdata[j+1]; A[1,1] = ydata[i  ]; A[1,2]= xdata[j+1]*ydata[i  ]; A[1,3]=1;
                    A[2,0] = xdata[j  ]; A[2,1] = ydata[i+1]; A[2,2]= xdata[j  ]*ydata[i+1]; A[2,3]=1;
                    A[3,0] = xdata[j+1]; A[3,1] = ydata[i+1]; A[3,2]= xdata[j+1]*ydata[i+1]; A[3,3]=1;
                    //and
                    vector B = new vector(4);
                    B[0]=zdata[i  ,j  ];
                    B[1]=zdata[i  ,j+1];
                    B[2]=zdata[i+1,j  ];
                    B[3]=zdata[i+1,j+1];

                    (matrix Q,matrix R) = A.getQR();
                    vector X = matrix.QRsolve(Q,R,B);

                    //Now x is the vector of (a,b,c,d) so:
                    a[i,j]=X[0];
                    b[i,j]=X[1];
                    c[i,j]=X[2];
                    d[i,j]=X[3];
                }
        }

        public double interpolate(double px, double py)
        {
            //Both get the grid we are in, and clip to borders if we are outside
            int i = 0;
            int j = 0;

            (j,px)=binary_search.binsearch(px,x);
            (i,py)=binary_search.binsearch(py,y);

            return a[i,j]*px+b[i,j]*py+c[i,j]*px*py+d[i,j];
        }

        public bool verify_all()
        {
            //Verify that ALL grids fullfill the boundary conditions
            bool worked = true;
            for (int i = 0; (i < n-1) && worked; ++i)
                for (int j = 0; j < m-1; ++j)
                {
                    worked = worked && matrix.approx(a[i,j]*x[j]+b[i,j]*y[i]+c[i,j]*x[j]*y[i]+d[i,j],z[i,j]);
                    if (!worked)
                    {
                        Error.WriteLine($"FAILED to get boundary conditions i,j for rectangle {i},{j}");
                        break;
                    }
                    worked = worked && matrix.approx(a[i,j]*x[j+1]+b[i,j]*y[i]+c[i,j]*x[j+1]*y[i]+d[i,j],z[i,j+1]);
                    if (!worked)
                    {
                        Error.WriteLine($"FAILED to get boundary j+1 conditions for rectangle {i},{j}");
                        break;
                    }
                    worked = worked && matrix.approx(a[i,j]*x[j]+b[i,j]*y[i+1]+c[i,j]*x[j]*y[i+1]+d[i,j],z[i+1,j]);
                    if (!worked)
                    {
                        Error.WriteLine($"FAILED to get boundary i+1 conditions for rectangle {i},{j}");
                        break;
                    }
                    worked = worked && matrix.approx(a[i,j]*x[j+1]+b[i,j]*y[i+1]+c[i,j]*x[j+1]*y[i+1]+d[i,j],z[i+1,j+1]);
                    if (!worked)
                    {
                        Error.WriteLine($"FAILED to get boundary i+1,j+1 conditions for rectangle {i},{j}");
                        break;
                    }


                }

            return worked;
        }

    }

    public static int Main(string[] argv)
    {
        if (argv.Length!=1)
        {
            Error.WriteLine("Input not valid, need 1 argument (input data)");
            return 1;
        }
        //I use the "error" channel for normal logging stuff
        Error.WriteLine("Loading data");


        //Declare the lists before loading them, I want them to be seen outside the try environment
        vector xlist;
        vector ylist;
        matrix zdata;

        int n=0;
        int m=0;

        try
        {
            //Read data into vectors and matrices
            string[] lines = System.IO.File.ReadAllLines(argv[0]);
            char[] deliminators = {'\t',' ',','};
            var options = StringSplitOptions.RemoveEmptyEntries;

            n = lines.Length-1;
            ylist=new vector(n);
            //Line 0, the x vector
            {
                string[] words = lines[0].Split(deliminators,options);
                m = words.Length-1;//The first entry doesn't really matter
                xlist=new vector(m);
                zdata=new matrix(n,m);

                for (int i = 0; i < m; ++i)
                {
                    double X=0;//Yeah, C# does not let me set xlist[i] directly here
                    bool number = double.TryParse(words[i+1],out X );
                    xlist[i]=X;
                    if (!number)
                    {
                        Error.WriteLine("Not valid number in line 0 : "+words[i+1]);
                        return 1;
                    }
                }

            }

            if (m<=0 || n<=0)
            {
                Error.WriteLine("Input not valid, should have positive width and height");
                return 1;
            }




            for(int i =0; i < n; ++ i )
            {
                string[] words = lines[i+1].Split(deliminators,options);

                //The first entry is the y vector elements
                double Y=0;
                bool number = double.TryParse(words[0],out Y);
                ylist[i]=Y;

                if (!number)
                {
                    Error.WriteLine($"Not valid number in line {i+1} : "+words[0]);
                    return 1;
                }

                for (int j = 0; j < m; ++j)
                {
                    double Z=0;
                    number = double.TryParse(words[j+1],out Z);
                    zdata[i,j]=Z;

                    if (!number)
                    {
                        Error.WriteLine($"Not valid number in line {i+1} : "+words[j+1]);
                        return 1;
                    }
                }
            }
        }
        catch(Exception E)
        {
            Error.WriteLine("Could not load input, got error "+E);
            return 1;

        }




        Error.WriteLine("Setting up bilinear interpolation");
        bilenear interpolator= new bilenear(xlist,ylist,zdata);
        Error.WriteLine("Verifying the solution for all boundary conditions ...");
        if (!interpolator.verify_all())
            return 1;
        else
            Error.WriteLine(" --- PASS: Solution matches all boundary conditions within relative and absolute error 10^-5");

        Error.WriteLine("Calculating and saving high resolution output");

        vector xout = new vector(m*4);

        //Ok, now get the output and print to a list gnuplot can understand

        Write(m*8);
        for (int i = 0; i < m*4; ++i)
        {
            xout[i]=(xlist[m-1]-xlist[0])*i/(n*4-1)+xlist[0];
            Write($" {xout[i]}");
        }

        Write("\n");
        for (int i = 0; i < n*4; ++i)
        {
            double y =(ylist[m-1]-ylist[0])*i/(n*4-1)+ylist[0];
            Write($" {y}");
            for (int j = 0; j < m*4; ++j)
            {
                Write($" {interpolator.interpolate(xout[j],y)}");
            }
            Write("\n");
        }
        return 0;
    }

}
