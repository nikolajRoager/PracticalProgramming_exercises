using System;
using static System.Console;
using static System.Math;

class main
{
    static int Main()
    {
        //Demonstrate arrays
        int n= 16;
        double[] F = new double[n];

        //Just something to test the for loops, lets do the Fibonnaci numbers
        F[0]=1;
        F[1]=1;
        for (int i = 2; i<n; ++i)
            F[i]=F[i-1]+F[i-2];
        for (int i = 0; i<n; ++i)
            WriteLine($"F[{i}]={F[i]}");

        //Demonstration of reference assignment

        double[] G = F;
        G[0]=0;//Sabotage teh sequence

        foreach (double f in F)
            WriteLine($"{f}");


        return 0;
    }
}
