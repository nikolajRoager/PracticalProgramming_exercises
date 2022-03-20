using System;
using static System.Math;

public class matrix
{
    public readonly int height,width;
    private double[] data;

    //Generate rectangular matrix
    public matrix(int n, int m)
    {
		height=n;
        width=m;
		data = new double[height*width];

        //Default to identity
        for (int i = 0; i < height; ++i)
            for (int j = 0; j < width; ++j)
                this[i,j]= (i==j) ? 1 : 0;
    }

    public matrix(int n)
    {
		height=n;
        width=n;
		data = new double[height*width];

        //Default to identity
        for (int i = 0; i < height; ++i)
            for (int j = 0; j < width; ++j)
                this[i,j]= (i==j) ? 1 : 0;
    }

    //Create get and set functions
	public double this[int i,int j]
    {
		get => data[i+j*height];
		set => data[i+j*height]=value;
	}

    //Just flipping the arguments will also do
    public matrix transpose()
    {
        matrix Out = new matrix(width,height);

        for (int i = 0; i < height; ++i)
            for (int j = 0; j < width; ++j)
                Out[j,i]=this[i,j];
        return Out;
    }
    //Pretty display function
    public override string ToString()
    {
        string Out = "";
        for (int i = 0; i < height; ++i)
        {
            Out+= "  |";
            for (int j = 0; j < width; ++j)
            {
                Out+=((Abs(this[i,j])< 10) ? " " : "")+((this[i,j]>=0) ? " " : "")+string.Format(" {0:N3}",this[i,j]);
            }
            if (i<height-1)
                Out+=" |\n";
            else
                Out+= " |";
        }
        return Out;
    }

    //Pretty display function, with something in front, i.e. A = ...
    public string getString(string prefix)
    {
        string Out = "";
        for (int i = 0; i < height; ++i)
        {

            if (i != height/2)
                for (int j = 0; j<prefix.Length; ++j)
                    Out+= " ";
            else
                Out+=prefix;

            Out+= "|";
            for (int j = 0; j < width; ++j)
            {

                Out+=((Abs(this[i,j])< 10) ? " " : "")+((this[i,j]>=0) ? " " : "")+string.Format(" {0:N3}",this[i,j]);
            }
            if (i<height-1)
                Out+=" |\n";
            else
                Out+= " |";
        }
        return Out;
    }


    //Same as above, but returns a list of strings in order to display matrix multiplication pretty
    public string[] getStrings(string prefix)
    {
        string[] Out = new string[height];
        for (int i = 0; i < height; ++i)
        {
            Out[i]="";
            if (i != height/2)
                for (int j = 0; j<prefix.Length; ++j)
                    Out[i]+= " ";
            else
                Out[i]+=prefix;

            Out[i]+= "|";
            for (int j = 0; j < width; ++j)
            {

                Out[i]+=((Abs(this[i,j])< 10) ? " " : "")+((this[i,j]>=0) ? " " : "")+string.Format(" {0:N3}",this[i,j]);
            }
            Out[i]+=" |";
        }
        return Out;
    }


    //Python compatible display version, for testing
    public string ToPython()
    {
        //Python compatible display version, for testing
        string Out = "np.matrix([";
        for (int i = 0; i < height; ++i)
        {
            Out+= "[";
            for (int j = 0; j < width; ++j)
            {

                Out+=string.Format(" {0}",this[i,j]);
                if (j<width-1)
                    Out+=",";
            }
            if (i<height-1)
                Out+=" ],";
            else
                Out+= " ]])";
        }
        return Out;
    }

    //Pretty display of the linear equation
    public string getString(matrix b)
    {
        string Out = "";
        if (b.width!=1)
            throw new ArgumentException("b should be a single column");
        if (b.height!=width)
            throw new ArgumentException("b should have same height as matrix width");
        for (int i = 0; i < height; ++i)
        {


            Out+= "   ";
            for (int j = 0; j < width; ++j)
            {

                Out+=((this[i,j]>=0 && j>0) ? "+" : "")+string.Format(" {0:N3}",this[i,j])+$" * x[{j}] ";
            }
            if (i<height-1)
                Out+=$" = {b[i,0]} \n";
            else
                Out+= $" = {b[i,0]}";
        }
        return Out;
    }



    public void randomize()
    {
        var generator = new Random();

        for (int j = 0; j < width; ++j)
            for (int i = 0; i < height; ++i)
                this[i,j]=generator.NextDouble();
    }

    public double colNorm (int j)
    {
        double Norm2 =0;

        for (int i = 0; i < height; ++i)
            Norm2+= this[i,j]*this[i,j];
        return Sqrt(Norm2);
    }

    //Essentially the copy constructor which would be used by default in C++
    public matrix copy()
    {
        matrix Out = new matrix(height,width);
        for(int i=0;i<height;i++)
            for(int j=0;j<width;j++)
                Out[i,j]=this[i,j];
        return Out;
    }

    //Run Gram Schmidt on this matrix
    public (matrix,matrix) getQR()
    {

        //The algorithm assumes that the columns of A are linearly independent, lets just hope they are ...

        matrix Q= this.copy();
        matrix R= new matrix(width,width);

        if (height<width)
            throw new ArgumentException("matrix height must be greater or equal to width for this implementation to work");


        for (int i = 0; i < width; ++i)
        {
            R[i,i]=Q.colNorm(i);
            //Set column vector i in Q to be A normalized
            for (int k = 0; k < height; ++k)
                Q[k,i]=Q[k,i]/R[i,i];
            for (int j = i+1; j < width; ++j)
            {

                R[i,j]=0;
                for (int k = 0; k < height; ++k)
                {
                    R[i,j]+=Q[k,i]*Q[k,j];
                }
                for (int k = 0; k < height; ++k)
                {
                    Q[k,j]-=Q[k,i]*R[i,j];
                }
            }
        }

        return (Q,R);
    }

    public static matrix QRsolve (matrix Q, matrix R, matrix b)
    {
        if (b.width!=1)
            throw new ArgumentException("b should be a single column");
        if (b.height!=Q.width)
            throw new ArgumentException("b should have same height as matrix width");
        if (Q.width!=R.width)
            throw new ArgumentException("Q and R should have same width");
        //Already assumes that Q is orthagonal
        var c = Q.transpose()*b;
        //Now R x = c can be solved with back-substitution
        //As in the chapter, I use in-place back substitution

        for (int i = c.height-1; i>=0; --i)
        {
            double sum = 0;
            for (int k = i+1; k<c.height; ++k)
                sum+=R[i,k]*c[k,0];
            c[i,0]=(c[i,0]-sum)/R[i,i];
        }

        return c;
	}


    public static matrix inverse(matrix Q, matrix R)
    {
        if (Q.width!=R.width)
            throw new ArgumentException("Q and R should have same width");

        //We really want to solve $height equations QR x = [0,0,...1 ... 0]

        //We can write this as a matrix equation, which can be solved with back substitution
        matrix unit_vector = vector(Q.width);
        matrix unity = new matrix(Q.height,Q.width);
        //Already assumes that Q is orthagonal
        //Now R x = c can be solved with back-substitution on the entire matrix
        matrix Out = Q.transpose()*unity;
        for (int j = 0; j < Q.width; ++j)
        {
            for (int i = Out.height-1; i>=0; --i)
            {
                double sum = 0;
                for (int k = i+1; k<Out.height; ++k)
                    sum+=R[i,k]*Out[k,j];
                Out[i,j]=(Out[i,j]-sum)/R[i,i];
            }
        }
        return Out;
	}

    //Not as detailed operations as in the matlib class, but enough to get by:
    public static matrix operator+(matrix a, matrix b)
    {
        matrix Out = new matrix(a.height,a.width);
        if (a.height!=b.height || a.width!=b.width)
            throw new ArgumentException("matrix height width do not match for addition");
        for(int i=0;i<a.height;i++)
            for(int j=0;j<a.width;j++)
                Out[i,j]=a[i,j]+b[i,j];
        return Out;
	}

    public static matrix operator-(matrix a)
    {
        matrix Out = new matrix(a.height,a.width);

        for(int i=0;i<a.height;i++)
            for(int j=0;j<a.width;j++)
                Out[i,j]=-a[i,j];
        return Out;
	}

    public static matrix operator- (matrix a, matrix b)
    {
        matrix Out = new matrix(a.height,a.width);
        if (a.height!=b.height || a.width!=b.width)
            throw new ArgumentException("matrix height width do not match for subtraction");

        for(int i=0;i<a.height;i++)
            for(int j=0;j<a.width;j++)
                Out[i,j]=a[i,j]-b[i,j];
        return Out;
	}

    //Matrix scaling
    public static matrix operator*(matrix a, double x)
    {
        matrix Out = new matrix(a.height,a.width);
        for(int i=0;i<a.height;i++)
            for(int j=0;j<a.width;j++)
                Out[i,j]=a[i,j]*x;
        return Out;
    }

    //same idea as in the matrix library given, allow multiplication from both sides
    public static matrix operator*(double x, matrix a){ return a*x; }

    //Matrix Matrix multiplication, this is the important part
    public static matrix operator* (matrix a, matrix b)
    {
        //Need mathcing matrices
        if (a.width!=b.height )
            throw new ArgumentException($"matrices need to have matching width/height for multiplication to be defined, got {a.height} by {a.width} times {b.height} by {b.width}");


        matrix Out = new matrix(a.height,b.width);
        for (int i=0;i<a.height;i++)
            for (int j=0;j<b.width;j++)
            {
                Out[i,j]=0;
                for (int k=0;k<a.width;k++)
                    Out[i,j]+=a[i,k]*b[k,j];
            }

        return Out;
    }

    public static matrix vector(int n)
    {
        return new matrix(n,1);//Vectors, in this context, are column matrices with a width of 1
    }


    public static matrix vector(double[] data)
    {
        matrix Out = new matrix(data.Length,1);
        Out.data=data;

        return Out;
    }

    //double precision approximation
    public static bool approx(double a,double b,double tau=1e-9,double eps=1e-9)
    {
        if (Abs(a-b)<tau)
            return true;
        if (Abs(a-b)/(Abs(a)+Abs(b))<eps)
            return true;
        return false;
    }

    public bool approx(matrix that)
    {
        if (height != that.height || width != that.width)
            return false;
        for (int i = 0; i < height; ++i)
            for (int j = 0; j < width; ++j)
                if (!approx(that[i,j],this[i,j]))
                    return false;
        return true;
    }


    public bool approx(matrix A,matrix B)
    {
        if (A.height != B.height || B.width != A.width)
            return false;
        for (int i = 0; i < A.height; ++i)
            for (int j = 0; j < A.width; ++j)
                if (!approx(A[i,j],B[i,j]))
                    return false;
        return true;
    }


    public bool is_uptriangle()
    {
        for (int i = 0; i < height; ++i)
            for (int j = 0; j < i; ++j)
                if (!approx(this[i,j],0))
                    return false;

        return true;
    }


}
