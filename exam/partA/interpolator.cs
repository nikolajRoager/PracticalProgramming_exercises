using System;
using static System.Console;
using static System.Math;

//A class, pre calculating the parameters
public class bilenear
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
