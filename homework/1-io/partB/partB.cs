using System.IO;
using static System.Console;
using System;
using static System.Math;

class main
{
    static public int Main(string[] argv)
    {

        Error.WriteLine("Reading from commandline arguments");//Printed to log


        Out.WriteLine("input, x , sin(x), cos(x)");
        foreach(string arg in argv)//Automatically disregards multiple blank spaces, but does not automatically disregard ','
        {
            //So read it again, this time explicitly marking , as a deliminator
            char[] deliminators = {' ','\t','\n',','};//\n is pointless in this context, as newline terminates ReadLine
            var options = StringSplitOptions.RemoveEmptyEntries;
            string[] words = arg.Split(deliminators,options);

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

        }

        return 0;
    }
}
