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

        Out.WriteLine("<table>");
        Out.WriteLine("\t<tr>");
        Out.WriteLine("\t\t<td> <b> input </b> </td>");
        Out.WriteLine("\t\t<td> <b> x </b> </td>");
        Out.WriteLine("\t\t<td> <b> Sin(x) </b> </td>");
        Out.WriteLine("\t\t<td> <b> cos(x) </b> </td>");
        Out.WriteLine("\t<tr>");
        foreach(string word in words)
        {
            Out.WriteLine("\t<tr>");
            double x = 0;
            bool number = double.TryParse(word,out x);

            Out.WriteLine("\t\t<td> <b> \""+word+"\" </b> </td>");
            if (number)
            {
                if (double.IsNaN(x))//NaN *is* technically a number so it passes the first test, but you might still want to catch it
                {
                    Error.WriteLine($"x={x} is ignored");

                    Out.WriteLine("\t\t<td> NaN </td>");
                    Out.WriteLine("\t\t<td> NaN </td>");
                    Out.WriteLine("\t\t<td> NaN </td>");

                }
                else
                {
                    //Write raw to log
                    Error.WriteLine($"x={x} , Sin(x)={Sin(x)}, cos(x)={Cos(x)}");

                    //Write html
                    Out.WriteLine($"\t\t<td> {x} </td>");
                    Out.WriteLine($"\t\t<td> {Sin(x)} </td>");
                    Out.WriteLine($"\t\t<td> {Cos(x)} </td>");
                }
            }
            else
            {
                Error.WriteLine(word+" is NOT a number");
                Out.WriteLine("\t\t<td> NaN </td>");
                Out.WriteLine("\t\t<td> NaN </td>");
                Out.WriteLine("\t\t<td> NaN </td>");

            }
            Out.WriteLine("\t</tr>");

        }
        Out.WriteLine("</table>");

        return 0;
    }
}
