using System;
using static System.Console;
using static System.Math;

class main
{
    static int Main()
    {
        //I don't want to check everything like this
        //while(i+1>i) {i++;} that would take several seconds
        //I know my computer stores numbers in binary so the max integer is all 1's, i.e. 2^n-1 for some n
        //So lets just check all the powers of two
        int pn=0b1;//previous n start at 000000....1 (0b is binary)
        int n=0b10;//start at 2^1
        while(n>pn )
        {   pn=n;
            n =n << 1;//Bitshift left
        }
        int max = n-1;//Now one before that should be our max int (if we are on a binary system, which we are)

        WriteLine($"my max int using continued bitshift method = {max}");
        WriteLine($"verify {max}+1 = {max+1}\n");//Intentional extra blank line

        WriteLine("The suggested while-loop method gives the same result, but has been commented out as it is much much slower\n");
        //WriteLine("Now trying the suggested while-loop method (much slower)");

        //n=1; while(n+1>n) {n++;}
        //WriteLine($"max int by checking all integers = {n}");


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
