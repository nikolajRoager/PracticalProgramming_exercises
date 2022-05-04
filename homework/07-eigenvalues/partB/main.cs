using System;
using static System.Console;
using static matrix;
using static System.Math;
using static jacobi;

public static class main
{
    public static int Main(string[] argv)
    {
        double R_max =0;
        int N =0;
        if (argv.Length==2)
        {

            if (!double.TryParse(argv[0],out R_max))
            {
                Error.WriteLine("Input not valid, could not convert  "+argv[0]+" to double");
                return 1;
            }
            if (!int.TryParse(argv[1],out N))
            {
                Error.WriteLine("Input not valid, could not convert  "+argv[1]+" to int");
                return 1;
            }
        }
        else
        {
            Error.WriteLine("Input not valid, need 1 arguments, R_max and Number of points ");
            return 1;
        }


        matrix K=new matrix(N,N);
        matrix V=new matrix(N,N);
        double dr =R_max/(N+1);


        //I already multiply on the factor onto the diagonal and off diagonal elements
        double K_diag = 1/(dr*dr);
        double K_off  =  -1/(2*dr*dr);
        for (int i = 0; i < N; ++i)
        {
            K[i,i]=K_diag;

            if (i+1<N)
            {
                K[i+1,i]=K_off;
                K[i,i+1]=K_off;
            }

            V[i,i] = - 1/((i+1)*dr);

        }
/*
        //For debugging, print the matrices... don't uncomment if N is large
        Error.WriteLine(K.getString("K ="));
        Error.WriteLine(" ");
        Error.WriteLine(V.getString("V ="));
        Error.WriteLine(" ");
*/
        matrix H = K+V;





        //As far as I can tell, this always returns the eigenenergies in ascending order
        (matrix Energies, matrix Eigenstates) =jacobi.getDV(H);


        Energies *= 27.211386245988;//Convert to eV




        //Print the first 5 energies to the terminal
        if (N>=5)
            Error.WriteLine($"{N}\t{R_max}\t{Energies[0,0]}\t{Energies[1,1]}\t{Energies[2,2]}\t{Energies[3,3]}\t{Energies[4,4]}");


        //Plot only the lowest 5 energies and save their states
        for (int i = 0; i < Eigenstates.height; ++i)
        {
            //The eigenvectors are not normalized ... as vectors, but not as functions.
            //Apparently dividing by the squareroot of the stepsize solves this, I am honestly not sure why the squareroot. I just tried dividing by the stepsize, and then I guessed this and it works.

            WriteLine($" {dr*i}\t{Eigenstates[i,0]/Sqrt(dr)}\t{-Eigenstates[i,1]/Sqrt(dr)}\t{-Eigenstates[i,2]/Sqrt(dr)}\t{-Eigenstates[i,3]/Sqrt(dr)}");

        }


        return 0;
    }

}
