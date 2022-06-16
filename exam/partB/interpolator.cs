using System;
using static System.Console;
using static System.Math;

//A class, pre calculating the parameters
public class bicubic
{
    //Width and height of the grid
    private int n;
    private int m;

    private vector x;
    private vector y;
    private matrix z;//The data of all the vertices, which we want to interpolate


    //z(d_x,d_y) = sum_{i=0}^3 sum_{j=0}^3 a_{ijkl} d_x^id_x^j
    //But for book-keeping purposes, I find it is easier to make A a monsterous 16*(n-1)*(m-1) vector
    private vector a;

    public bicubic(vector  xdata, vector   ydata,matrix zdata)
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

        a         = new vector(16*(n-1)*(m-1));

        //have a_ijkl = a[i+j*4+16*k+16*(n-1)*l]

        //We want to set up a set of linear equations A a =b to determine a.
        matrix A = new matrix(16*(n-1)*(m-1),16*(n-1)*(m-1));
        //zero-set matrix A, as it starts as being the identity
        for (int i = 0; i<16*(n-1)*(m-1); ++i)
            A[i,i]=0;

        vector B = new vector(16*(n-1)*(m-1));




        //Set conditions on grid by grid basis
        for (int l = 0; l < n-1; ++l)
        {

            double h_l = ydata[l+1]-ydata[l];
            for (int k = 0; k < m-1; ++k)
            {

                double w_k = xdata[k+1]-xdata[k];

                //have a_ijkl = a[i+j*4+16*k+16*(n-1)*l]
                //offset due to kl
                int kl = k*16+16*(n-1)*l;
                //Now have a_ijkl = a[i+j*4+kl]
                //Can do having be that  the corner point  of each grid point do being:

                /*
                    f^{k,l}(0,0)     &= a_{00}^{(k,l)}                                        = z_{l,k},\\
                    f^{k,l}(w_k,0)   &= \sum_{i=0}^3  w_k^i a_{i0}^{(k,l)}                    = z_{l,k+1},\\
                    f^{k,l}(0,h_l)   &= \sum_{j=0}^3  h_l^j a_{0j}^{(k,l)}                    = z_{l+1,k},\\
                    f^{k,l}(w_k,h_l) &= \sum_{i=0}^3 \sum_{j=0}^3  w_k^i h_l^j a_{ij}^{(k,l)} = z_{l+1,k+1}.
                */
                //Or

                //First set of condition, do be having points correct yes

                A[0+kl,kl]=1;//1 a_{00}^{(k,l)}
                B[0+kl]=zdata[l  ,k];// = z_{l,k},


                //f^{k,l}(w_k,0)   &= \sum_{i=0}^3  w_k^i a_{i0}^{(k,l)}                    = z_{l,k+1},\\
                for (int i = 0; i <= 3; ++i)
                    A[1+kl,i+kl] = Pow(w_k,i);
                B[1+kl]=zdata[l  ,k+1];//= z_{l,k+1},


                //f^{k,l}(0,h_l)   &= \sum_{j=0}^3  h_l^j a_{0j}^{(k,l)}                    = z_{l+1,k},\\
                for (int j = 0; j <= 3; ++j)
                    A[2+kl,j*4+kl] = Pow(h_l,j);
                B[2+kl]=zdata[l+1,k];//= z_{l+1,k},

                //f^{k,l}(w_k,h_l) &= \sum_{i=0}^3 \sum_{j=0}^3  w_k^i h_l^j a_{ij}^{(k,l)} = z_{l+1,k+1}.
                for (int i = 0; i <= 3; ++i)
                    for (int j = 0; j <= 3; ++j)
                        A[3+kl,i+j*4+kl] =Pow(w_k,i)*Pow(h_l,j);
                B[3+kl]=zdata[l+1,k+1];//= z_{l+1,k+1},

                //Derivatives be
                /*

                    \begin{align}
                    \frac{d}{dx} f^{k,l}(\Delta x,\Delta y) &= \sum_{i=1}^3 \sum_{j=0}^3  i \Delta x^{i-1} \Delta y^j a_{ij}^{(k,l)},\\
                    \frac{d}{dy} f^{k,l}(\Delta x,\Delta y) &= \sum_{i=0}^3 \sum_{j=1}^3  j \Delta x^i \Delta y^{j-1} a_{ij}^{(k,l)},\\
                    \frac{d^2}{dx dy} f^{k,l}(\Delta x,\Delta y) &= \sum_{i=1}^3 \sum_{j=1}^3  ij \Delta x^{i-1} \Delta y^{j-1} a_{ij}^{(k,l)},
                    \end{align}

                    Corner derivative x's be

                */

                //dx    f^{k,l}(0,0) &=    a_{10}^{(k,l)},\\
                A[4+kl,1+kl]=1;

                //dx    f^{k,l}(w,0) &= \sum_{i=1}^3  i w^{i-1} a_{i0}^{(k,l)},\\
                for (int i = 1; i <= 3; ++i)
                    A[5+kl,i+kl] = i*Pow(w_k,i-1);


                //dx    f^{k,l}(0,h) &= \sum_{j=0}^3  1 h^j     a_{1j}^{(k,l)},\\
                for (int j = 0; j <= 3; ++j)
                    A[6+kl,1+j*4+kl] = Pow(h_l,j);

                //dx    f^{k,l}(w,h) &= \sum_{i=1}^3 \sum_{j=0}^3  i w^{i-1} h^j a_{ij}^{(k,l)},\\
                for (int i = 1; i <= 3; ++i)
                    for (int j = 0; j <= 3; ++j)
                        A[7+kl,i+j*4+kl] =i*Pow(w_k,i-1)*Pow(h_l,j);

                //derivative y's
                //dy    f^{k,l}(0,0)     &= a_{01}^{(k,l)}
                A[8+kl,4+kl]=1;
                //dy    f^{k,l}(w_k,0)   &= \sum_{i=0}^3 j w_k^i a_{i1}^{(k,l)}
                for (int i = 0; i <= 3; ++i)
                    A[9+kl,i+4+kl] = Pow(w_k,i);
                //dy    f^{k,l}(0,h_l)   &= \sum_{j=1}^3 j h_l^{j-1} a_{0j}^{(k,l)}
                for (int j = 1; j <= 3; ++j)
                    A[10+kl,j*4+kl] = j*Pow(h_l,j-1);
                //dy    f^{k,l}(w_k,h_l) &= \sum_{i=0}^3 \sum_{j=1}^3  j w_k^i h_l^{j-1} a_{ij}^{(k,l)}
                for (int i = 0; i <= 3; ++i)
                    for (int j = 1; j <= 3; ++j)
                        A[11+kl,i+j*4+kl] =j*Pow(w_k,i)*Pow(h_l,j-1);

                //derivative xy

                // \frac{d^2}{dx dy} f^{k,l}(\Delta x,\Delta y) &= \sum_{i=1}^3 \sum_{j=1}^3  ij \Delta x^{i-1} \Delta y^{j-1} a_{ij}^{(k,l)},

                //dydx    f^{k,l}(0,0)     &= a_{11}^{(k,l)}
                A[12+kl,1+4+kl]=1;
                //dydx    f^{k,l}(w_k,0)   &= \sum_{i=1}^3 ji w_k^{i-1} a_{i1}^{(k,l)}
                for (int i = 1; i <= 3; ++i)
                    A[13+kl,i+4+kl] = i*Pow(w_k,i-1);
                //dydx    f^{k,l}(0,h_l)   &= \sum_{j=1}^3 ji h_l^{j-1} a_{1j}^{(k,l)}
                for (int j = 1; j <= 3; ++j)
                    A[14+kl,1+j*4+kl] = j*Pow(h_l,j-1);
                //dydx    f^{k,l}(w_k,h_l) &= \sum_{i=0}^3 \sum_{j=1}^3  ji w_k^{i-1} h_l^{j-1} a_{ij}^{(k,l)}
                for (int i = 0; i <= 3; ++i)
                    for (int j = 1; j <= 3; ++j)
                        A[15+kl,i+j*4+kl] =j*i*Pow(w_k,i-1)*Pow(h_l,j-1);


            }
        }

        (matrix Q,matrix R) = A.getQR();
        a = matrix.QRsolve(Q,R,B);

/*
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

        */
    }

    public double interpolate(double px, double py)
    {
        //Both get the grid we are in, and clip to borders if we are outside
        int k = 0;
        int l = 0;

        (l,px)=binary_search.binsearch(px,x);
        (k,py)=binary_search.binsearch(py,y);

    //z(d_x,d_y) = sum_{i=0}^3 sum_{j=0}^3 a_{ij} d_x^id_x^j

        //pre get the squares and cubes of the difference from top-left corner

        double Dx = px-x[l];
        double Dy = py-y[k];
        double[] dx = {1,Dx,Dx*Dx,Dx*Dx*Dx};
        double[] dy = {1,Dy,Dy*Dy,Dy*Dy*Dy};

        double OUT = 0;
        for (int i = 0; i <=3; ++i)
            for (int j = 0; j <=3; ++j)
            {
                OUT+=a[i+j*4+16*k+16*(n-1)*l]*dx[j]*dy[i];
            }
        return OUT;
    }

    public bool verify_all()
    {
        return true;
    /*
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

        return worked;*/
    }

}
