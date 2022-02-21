using System.IO;
using static System.Console;
using System;
using static System.Math;

class main
{
    static public int Main()
    {
        Error.WriteLine("Reading from terminal, enter numbers");//Printed to log

        //Read and parse
        string input = In.ReadLine();
        char[] deliminators = {' ','\t','\n',','};//\n is pointless in this context, as newline terminates ReadLine
        var options = StringSplitOptions.RemoveEmptyEntries;
        string[] words = input.Split(deliminators,options);

        Out.WriteLine("input, x , sin(x), cos(x)");
        foreach(string word in words)
        {
            double x = 0;
            bool number = double.TryParse(word,out x);

            Out.Write("\""+word+"\"");
            if (number)
            {
                if (double.IsNaN(x))//NaN *is* technically a number so it passes the first test, but you might still want to catch it
                {
                    Error.WriteLine($"x={x} is ignored");

                    Out.WriteLine(", NaN, NaN, NaN ");

                }
                else
                {
                    //Write to document
                    Out.WriteLine($"x={x} , Sin(x)={Sin(x)}, cos(x)={Cos(x)}");
                }
            }
            else
            {
                Error.WriteLine(word+" is NOT a number");
                Out.WriteLine(", NaN, NaN, NaN ");

            }

        }

        return 0;
    }
}
