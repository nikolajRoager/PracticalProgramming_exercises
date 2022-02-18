using System.IO;
using static System.Console;
using System;
using static System.Math;

class main
{
    static public int Main(string[] argv)
    {

        Error.WriteLine("Reading from commandline arguments");//Printed to log


        Out.WriteLine("<table>");
        Out.WriteLine("\t<tr>");
        Out.WriteLine("\t\t<td> <b> input </b> </td>");
        Out.WriteLine("\t\t<td> <b> x </b> </td>");
        Out.WriteLine("\t\t<td> <b> Sin(x) </b> </td>");
        Out.WriteLine("\t\t<td> <b> cos(x) </b> </td>");
        Out.WriteLine("\t<tr>");
        foreach(string arg in argv)//Automatically disregards multiple blank spaces, but does not automatically disregard ','
        {
            //So read it again, this time explicitly marking , as a deliminator
            char[] deliminators = {' ','\t','\n',','};//\n is pointless in this context, as newline terminates ReadLine
            var options = StringSplitOptions.RemoveEmptyEntries;
            string[] words = arg.Split(deliminators,options);

            foreach (string word in words)
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

        }
        Out.WriteLine("</table>");

        return 0;
    }
}
