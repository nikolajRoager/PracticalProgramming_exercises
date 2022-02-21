using System.IO;
using static System.Console;
using System;
using static cmath;
using static System.Math;

class main
{
    static public int Main()
    {
       // , √i, ei, eiπ, ii, ln(i), sin(iπ)
        WriteLine($"√-1={sqrt(-1+0*I)}");
        WriteLine($"exp(i)={exp(I)}");
        WriteLine($"exp(i*π)={exp(I*PI)}");
        WriteLine($"pow(i,i)={exp(log(I)*I)}");
        WriteLine($"ln(i)={log(I)}");
        WriteLine($"sin(πi)={sin(PI*I)}");
        return 0;
    }
}
