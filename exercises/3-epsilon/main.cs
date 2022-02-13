using System;
using static System.Console;
using static System.Math;

class main
{
    static int Main()
    {
        //I don't want to check everything like this
        //while(i+1>i) {i++;} that would take several seconds, I don't have time for that
        //I know that max int is 2^n-1 so lets just check all the powers of two
        int pn=1;//previous n start at 2^0
        int n=2;//start at 2^1
        while(n>pn) {pn=n;n*=2;}
        int max = n-1;

        WriteLine($"my max int = {max}");
        WriteLine($"verify {max}+1 = {max+1}\n");

        //Sadly, I could not find an optimal way of finding epsilon
        double x=1; while(1+x!=1){x/=2;} x*=2;//Just divide by 2 until adding this to 1 does nothing
        float y=1F; while((float)(1F+y) != 1F){y/=2F;} y*=2F;

        WriteLine($"double epsilon = {x}, compare to 2^-52={System.Math.Pow(2,-52)}");
        WriteLine($"float epsilon = {y}, compare to 2^-23={System.Math.Pow(2,-23)}");

        //Having verrified thism lets do the large sum
        int N=(int)1e6;
        double epsilon=Pow(2,-52);
        double tiny=epsilon/2;
        double sumA=0,sumB=0;

        sumA+=1; for(int i=0;i<N;i++){sumA+=tiny;}
        WriteLine($"sumA-1 = {sumA-1:e} should be {n*tiny:e}");

        for(int i=0;i<N;i++){sumB+=tiny;} sumB+=1;
        WriteLine($"sumB-1 = {sumB-1:e} should be {n*tiny:e}");
        //As we see, sum A looses information to a larger degree than sum B

        if ( Approx.approx(1.0,1.0))
            WriteLine("1.0 approx 1.0");
        else
            WriteLine("1.0 is not 1.0");

        if ( Approx.approx(1.1,1.0))
            WriteLine("1.1 approx 1.0");
        else
            WriteLine("1.1 is not 1.0");


        if ( Approx.approx(1.0+tiny,1.0))
            WriteLine("1.0+tiny approx 1.0");
        else
            WriteLine("1.1 is not 1.0");


        return 0;
    }
}
