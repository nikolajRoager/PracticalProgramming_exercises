using System;
using static System.Console;
using static System.Math;

public static class main
{

    static double ex(double x)
    {
        if(x<0)          //Adjust the approximation to be positive
            return 1/ex(-x);
        if(x>1.0/8)
            return Pow(ex(x/2),2);//and "small", note I am using 2 and not 2.0
        //Now use the approximation
        return 1+x*(1+x/2*(1+x/3*(1+x/4*(1+x/5*(1+x/6*(1+x/7*(1+x/8*(1+x/9*(1+x/10)))))))));
    }

    //Same, but without range correction
    static double ex_simple(double x)
    {
        return 1+x*(1+(x/2)*(1+(x/3)*(1+(x/4)*(1+(x/5)*(1+(x/6)*(1+(x/7)*(1+(x/8)*(1+(x/9)*(1+(x/10))))))))));
    }


    public static int Main()
    {
        for (double x = -10; x<10; x+=0.1)
            WriteLine($"{x}\t{Exp(x)}\t{ex_simple(x)}\t{ex(x)}");
        return 0;
    }
}
