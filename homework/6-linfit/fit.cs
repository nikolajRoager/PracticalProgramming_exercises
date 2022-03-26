using static matrix;
using System;
using static System.Console;

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


        Error.WriteLine(A.getString("Matrix to solve A="));
        Error.WriteLine(" ");

        (matrix Q,matrix R) = A.getQR();
        matrix C = QRsolve (Q, R, b);//This does not solve (QR)C=b, only RC = Q^T b, Q is not truely orthagonal, it only has orthagonal columns


        Error.WriteLine(Q.getString("Did get Q ="));
        Error.WriteLine(" ");

        Error.WriteLine(R.getString("Did get R ="));


        matrix PinvA = penrose_inverse(Q,R);

        Error.WriteLine(PinvA.getString("p-inverse"));

        matrix Sigma = PinvA*(PinvA.transpose());

        matrix Evil = (R.transpose())*R;
        (matrix EvilQ, matrix EvilR) = Evil.getQR();

        matrix EvilSigma = inverse(EvilQ,EvilR);

        Error.WriteLine(" ");

        Error.WriteLine((PinvA*A).getString("p-inverse * A="));
        Error.WriteLine(" ");

        Error.WriteLine((A*PinvA).getString("A * p-inverse="));
        Error.WriteLine(" ");
        Error.WriteLine(Sigma.getString("p-inverse * p-inverse^T"));

        return (C.get_data(),EvilSigma);
/*
        matrix QR = Q*R;

        Error.Write("   QR == A ? ... ");
        if (QR.approx(A))
            Error.WriteLine(" PASSED");
        else
            Error.WriteLine(" FAILED");
        matrix QtQ = Q.transpose()*Q;
        Error.Write("   Q^T Q == 1 ? ... ");
        if (QtQ.approx(new matrix(QtQ.height,QtQ.width)))
            Error.WriteLine(" PASSED");
        else
            Error.WriteLine(" FAILED");

        Error.WriteLine(A.getString("A = "));
        Error.WriteLine("\n");
        Error.WriteLine(Q.getString("Q = "));
        Error.WriteLine("\n");
        Error.WriteLine(R.getString("R = "));
        Error.WriteLine("\n");


        Error.WriteLine(QR.getString("QR = "));
        Error.WriteLine("\n");


        matrix RC = R*C;
        matrix QtB = (Q.transpose())*b;
        Error.Write("   R*C == Q^T b ? ... ");
        if (RC.approx(QtB))
            Error.WriteLine(" PASSED");
        else
            Error.WriteLine(" FAILED");


        Error.WriteLine(A.getString("A = "));
        Error.WriteLine("\n");
        Error.WriteLine(b.getString("b = "));
        Error.WriteLine("\n");
        Error.WriteLine(C.getString("C = "));
        Error.WriteLine("\n");
*/

    }
}

