using static matrix;
using System;
//using static System.Console;

public static class fit
{
    public static (double[],matrix) LSfit(double[] x, double[] y, double[] dy, Func<double,double>[] F)
    {
        int n = x.Length;
        int m = F.Length;

        if (y.Length != n || dy.Length != n)
            throw new ArgumentException("x, y and dy should have same number of elements");

        if (m==0)
            throw new ArgumentException("Function list is empty");

        //I use Nx1 matrices as vector, such that I may use the same functions on them
        matrix b = vector(n);
        matrix A = new matrix(n,m);
        for (int i = 0; i < n; ++i)
        {

            for (int k = 0; k < m; ++k)
            {
                A[i,k]=F[k](x[i])/dy[i];
            }
            b[i,0]=y[i]/dy[i];
        }


        //Error.WriteLine(A.getString("Matrix to solve A="));
        //Error.WriteLine(" ");

        (matrix Q,matrix R) = A.getQR();
        matrix C = QRsolve (Q, R, b);//This does not solve (QR)C=b, only RC = Q^T b, Q is not truely orthagonal, it only has orthagonal columns


        //Error.WriteLine(Q.getString("Did get Q ="));
        //Error.WriteLine(" ");

        //Error.WriteLine(R.getString("Did get R ="));


        matrix PinvA = penrose_inverse(Q,R);

        //Error.WriteLine(PinvA.getString("p-inverse"));

        matrix Sigma = PinvA*(PinvA.transpose());

        /* Alternative EVIL method (find and use exact inverse), gives SAME result, so never use it
        matrix Evil = (R.transpose())*R;
        (matrix EvilQ, matrix EvilR) = Evil.getQR();

        matrix EvilSigma = inverse(EvilQ,EvilR);
        */
        return (C.get_data(),Sigma);
    }
}

