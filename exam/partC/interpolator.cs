using System;
using static System.Console;
using static System.Math;

//A class, pre calculating the parameters
public class Nlinear
{
    private int N; //dimension
    private int pow2N;//I need this A LOT
    private int[] n; //size of each axis

    private vector[] axis_data;
    private double[] data;//The data of all the vertices, which we want to interpolate. Format, {data_000,data_001,data_002, data_010, data_011,... }

    //Inside each hyper-cube
    //S(p_x,p_y,p_z,...) = sum_{i=0}^1 sum_{j=0}^1 sum_{j=0}^1 ... a_{ijk...} p_x^ip_y^jp_z^2 ...
    //This gives us 2^N *(n[0]-1)\ldots elements, this is the vector I will store them in
    private vector a;//The elements are stored, as such that we first have cube (0,...,0,0) a_{0...00},a_{0...01},a_{0...10} and so on, then cube (0,...,0,1) etc.

    public Nlinear(vector[] _axis_data, double[] _data)

    {
        N = _axis_data.Length;
        pow2N=(int)Pow(2,N);
        n = new int[N];
        axis_data = new vector[N];

        //Get the size of the main coefficient vector, and verify the axes
        int A_size = 1;
        int data_size = 1;
        for (int i = 0; i < N; ++i)
        {
            n[i]=_axis_data[i].size;
            A_size *=2*(n[i]-1);
            data_size*=(n[i]);
            axis_data[i]=_axis_data[i].copy();

            //Verify that the data works
            double pdata = axis_data[i][0];
            for(int j = 1; j < n[i]; ++j)
            {
                if (pdata>axis_data[i][j])
                    throw new System.Exception($"Invalid data, x{i} must be ordered");
                pdata=axis_data[i][j];
            }

        }


        if (data_size != _data.Length )
            throw new System.Exception($"Invalid data, data size {_data.Length} did not match axes size {data_size }");

        a         = new vector(A_size);

        data = new double[data_size];
        for (int i = 0; i < data_size; ++i)
            data[i]=_data[i];

        //Loop through all the hypercubes
        int[] grid= new int[N];
        while(grid[N-1]!=n[N-1]-1)
        {

            //We have 2^N linear equations

            //If S(p_x,p_y,p_z,...) = sum_{i=0}^1 sum_{j=0}^1 sum_{j=0}^1 ... a_{ijk...} p_x^ip_y^jp_z^2 ...

            //S(x[i],y[j],...) = data[i,j,...] and so on with all the corners

            //Ok you know the drill,

            //This is the matrix we need to solve
            matrix A = new matrix(pow2N,pow2N);
            vector B = new vector(pow2N);

            //So we need to loop through this on a per- boundary condition basis
            //What we do-want is:
            /*
                    for (int i = 0; i <=1; ++i)
                        for (int j = 0; j <=1; ++j)
                            for (int k = 0; k <=1; ++k)
                                for (int l = 0; l <=1; ++l)
                                ...
                                {
                                    S(here)=data[here]
                                }

            Well, this is the same as do-loop through all binary numbers until 2^N, for instance for 3

            000 0
            001 1
            010 2
            011 3
            100 4
            101 5
            110 6
            111 7

            So we can just loop through these 2^N numbers

            */

            //Loop through the corners
            for (int I = 0; I < pow2N; ++I)
            {
                //alas, now we need a way to convert, for instance 3 = 011, i = 0, j=1, l=1 etc.
                int[] corner = new int[N];
                int dataID = 0;//It really does not matter how I read the data, it just need be consistent, my idea  is to first loop through x, then y then z and so on ... yes that is the ohther way around you normally do with matrices, but whatever
                int multiplier =1; // the data is stored as data[i*1+j*n[0]+k*n[1]*n[0]...]
                for (int i = 0; i < N; ++i)//shift the bits in I one to the right, and check if it is 1
                {
                    corner[i]=(I>>i)%2;
                    dataID += multiplier*(corner[i]+grid[i]);
                    multiplier*=n[i];
                }

                B[I]=data[dataID ];
                //Ok with that set, now loop through the elements of A corresponding to equation I
                for (int J = 0; J < pow2N; ++J)
                {
                    A[I,J]=1;
                    for (int i = 0; i < N; ++i)//shift the bits in I one to the right, and check if it is 1
                    {

                        if ((J>>i)%2==1)//This element includes the Jth element, which actually corresponds to axis[i] =x,y,z or whatever
                            A[I,J]*=  axis_data[i][grid[i]+corner[i]];
                    }
                }

            }



            (matrix Q,matrix R) = A.getQR();
            vector X = matrix.QRsolve(Q,R,B);

            //Calculate the offset due to whatever grid we are in
            int grid_index = 0;
            int fullgrid_offset=pow2N;//We need enough room for the indices each grid

            for (int j = 0; j< N; ++j)
            {

                grid_index += grid[j]*fullgrid_offset;
                fullgrid_offset*=n[j]-1;
            }


            //Now read of the coefficients, for this grid here
            for (int I = 0; I < pow2N; ++I)
            {
                a[I+grid_index]=X[I];
            }

            ++grid[0];//Increment the index
            //But keep overflowing to loop through all indices



            for (int i=0; i < N; ++i)
                if (grid[i]==n[i]-1)
                    if (i==N-1)
                        break;//Ok got them all
                    else
                    {
                        grid[i]=0;
                        ++grid[i+1];//Overflow
                    }

            if (grid[N-1]==n[N-1]-1)
                break;

        }

    }


    public double interpolate(double[] p, int[] Grid = null)
    {
        if (p.Length != N || p == null)
            throw new System.Exception($"Invalid point, size {p.Length} did not match dimensions {N}");

        //If a grid points was supplied, we are forced to use it (useful for testing boundary conditions), so clip to its border
        if (Grid == null || Grid.Length!= N)
        {
            Grid = new int[N];
            for (int i = 0; i < N; ++i)
            {


                (Grid[i],p[i])=binary_search.binsearch(p[i],axis_data[i]);
            }
        }
        else
        {//If a grid point was supplied, clip to border
            for (int i = 0; i < N; ++i)
            {
                if(axis_data[i][Grid[i]]>p[i])
                    p[i]=axis_data[i][Grid[i]];
                if (Grid[i]+1<n[i])
                    if(axis_data[i][Grid[i]+1]<p[i])
                        p[i]=axis_data[i][Grid[i]+1];
            }

        }
    double OUT = 0;


    //Inside each hyper-cube
    //z(p_x,p_y,p_z,...) = sum_{i=0}^1 sum_{j=0}^1 sum_{j=0}^1 ... a_{ijk...} p_x^ip_y^jp_z^2 ...

    //Calculate the offset due to whatever grid we are in
    int grid_index = 0;
    int fullgrid_offset=pow2N;//We need enough room for the indices each grid

    for (int j = 0; j< N; ++j)
    {

        grid_index += Grid[j]*fullgrid_offset;
        fullgrid_offset*=n[j]-1;
    }

//What we do-want is:
/*
        for (int i = 0; i <=1; ++i)
            for (int j = 0; j <=1; ++j)
                for (int k = 0; k <=1; ++k)
                    for (int l = 0; l <=1; ++l)
                    ...
                    {
                        OUT+=a[i+j*2+k*4+l*8+...+grid_index]*x[i]*y[j]*z[k]*w[l]...;
                    }

Well, this is the same as do-loop through all binary numbers until 2^N, for instance for 3

000 0
001 1
010 2
011 3
100 4
101 5
110 6
111 7

So we can just loop through these 2^N numbers

*/


    for (int I = 0; I < pow2N; ++I)
    {
        //alas, now we need a way to convert, for instance 3 = 011, into x^0y^1z^1
        double pProd = 1;
        for (int i = 0; i < N; ++i)//shift the bits in I one to the right, and check if it is 1
            if ((I>>i)%2==1)
                pProd*=p[i];
        OUT+=a[I+grid_index]*pProd;
    }
        return OUT;
    }

    public bool verify_all()
    {

        bool worked = true;

        //Loop through all the hypercubes
        int[] grid= new int[N];
        while(worked)
        {

            //Now check all the (2^N) corners of this hypercube
            for (int I = 0; I < pow2N; ++I)
            {
                double[] corner = new double[N];//Where exactly is this corner, used for calling the interpolator
                int[] corner_offset = new int[N];//What corner are we in (0,1), useful for printing where stuff went wrong
                int dataID = 0;//It really does not matter how I read the data, it just need be consistent, my idea  is to first loop through x, then y then z and so on ... yes that is the ohther way around you normally do with matrices, but whatever
                int multiplier =1; // the data is stored as data[i*1+j*n[0]+k*n[1]*n[0]...]
                for (int i = 0; i < N; ++i)//shift the bits in I one to the right, and check if it is 1
                {
                    corner_offset[i]=(I>>i)%2;
                    corner[i]=axis_data[i][grid[i]+corner_offset[i]];
                    dataID += multiplier*(grid[i]+corner_offset[i]);
                    multiplier*=n[i];
                }
                double got = interpolate(corner,grid);
                double shouldget = data[dataID];
                worked = worked && matrix.approx(got,shouldget);
                if (!worked)
                {
                    Error.Write($"FAILED to get expected data, at corner [ ");
                    foreach (int j in corner_offset)
                        Error.Write($"{j} ");
                    Error.Write("] for rectangle [ ");
                    foreach (int j in grid)
                        Error.Write($"{j} ");
                    Error.WriteLine($"], got {got} should get {shouldget}");
                    break;
                }

            }

            ++grid[0];

            //advance to next cube
            for (int i=0; i < N; ++i)
                if (grid[i]==n[i]-1)
                    if (i==N-1)
                        break;//Ok got them all
                    else
                    {
                        grid[i]=0;
                        ++grid[i+1];//Overflow
                    }

            if (grid[N-1]==n[N-1]-1)
                break;
        }

        /*
        for (int l = 0; (l < n-1) && worked; ++l)
            for (int k = 0; k < m-1; ++k)
            {
                //For each grid, verify that the functions values are found correctly
                worked = worked && matrix.approx(interpolate(xdata[k],ydata[l], l,k),zdata[l,k]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected z, at corner 0,0 for rectangle {l},{k}");
                    break;
                }
                worked = worked && matrix.approx(interpolate_dx(xdata[k],ydata[l], l,k),dxzdata[l,k]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected dz/dx, at corner 0,0 for rectangle {l},{k}");
                    break;
                }
                worked = worked && matrix.approx(interpolate_dy(xdata[k],ydata[l], l,k),dyzdata[l,k]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected dz/dy, at corner 0,0 for rectangle {l},{k}");
                    break;
                }
                worked = worked && matrix.approx(interpolate_dxdy(xdata[k],ydata[l], l,k),dxdyzdata[l,k]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected d^2z/dxdy, at corner 0,0 for rectangle {l},{k}");
                    break;
                }
                //For each grid, verify that the functions values are found correctly
                worked = worked && matrix.approx(interpolate(xdata[k],ydata[l+1], l,k),zdata[l+1,k]);//DO NOT shift the l,k in the interpolate argument, that forces the interpolator to use THIS rectangle, rather than the next one over, it should be the same in the corner points, but that is what we want to check
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected z, at corner 1,0 for rectangle {l},{k}");
                    break;
                }
                worked = worked && matrix.approx(interpolate_dx(xdata[k],ydata[l+1], l,k),dxzdata[l+1,k]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected dz/dx, at corner 1,0 for rectangle {l},{k}");
                    break;
                }
                worked = worked && matrix.approx(interpolate_dy(xdata[k],ydata[l+1], l,k),dyzdata[l+1,k]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected dz/dy, at corner 1,0 for rectangle {l},{k}");
                    break;
                }
                worked = worked && matrix.approx(interpolate_dxdy(xdata[k],ydata[l+1], l,k),dxdyzdata[l+1,k]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected d^2z/dxdy, at corner 1,0 for rectangle {l},{k}");
                    break;
                }
                //For each grid, verify that the functions values are found correctly
                worked = worked && matrix.approx(interpolate(xdata[k+1],ydata[l], l,k),zdata[l,k+1]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected z, at corner 0,1 for rectangle {l},{k}");
                    break;
                }
                worked = worked && matrix.approx(interpolate_dx(xdata[k+1],ydata[l], l,k),dxzdata[l,k+1]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected dz/dx, at corner 0,1 for rectangle {l},{k}");
                    break;
                }
                worked = worked && matrix.approx(interpolate_dy(xdata[k+1],ydata[l], l,k),dyzdata[l,k+1]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected dz/dy, at corner 0,1 for rectangle {l},{k}");
                    break;
                }
                worked = worked && matrix.approx(interpolate_dxdy(xdata[k+1],ydata[l], l,k),dxdyzdata[l,k+1]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected d^2z/dxdy, at corner 0,1 for rectangle {l},{k}");
                    break;
                }
                //For each grid, verify that the functions values are found correctly
                worked = worked && matrix.approx(interpolate(xdata[k+1],ydata[l+1], l,k),zdata[l+1,k+1]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected z, at corner 1,1 for rectangle {l},{k}");
                    break;
                }
                worked = worked && matrix.approx(interpolate_dx(xdata[k+1],ydata[l+1], l,k),dxzdata[l+1,k+1]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected dz/dx, at corner 1,1 for rectangle {l},{k}");
                    break;
                }
                worked = worked && matrix.approx(interpolate_dy(xdata[k+1],ydata[l+1], l,k),dyzdata[l+1,k+1]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected dz/dy, at corner 1,1 for rectangle {l},{k}");
                    break;
                }
                worked = worked && matrix.approx(interpolate_dxdy(xdata[k+1],ydata[l+1], l,k),dxdyzdata[l+1,k+1]);
                if (!worked)
                {
                    Error.WriteLine($"FAILED to get expected d^2z/dxdy, at corner 1,1 for rectangle {l},{k}");
                    break;
                }

            }
        */
        return worked;
    }

}
