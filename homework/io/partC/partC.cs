using System.IO;
using static System.Console;
using System;
using static System.Math;

class main
{
    static public int Main(string[] argv)
    {
        if (argv.Length!=2)
        {
            Error.WriteLine("Illegal input, need input_file output_file");
            return 1;
        }

        try
        {
            var reader = new System.IO.StreamReader(argv[0]);
            var writer = new System.IO.StreamWriter(argv[1]);

            writer.WriteLine("<table>");
            writer.WriteLine("\t<tr>");
            writer.WriteLine("\t\t<td> <b> input </b> </td>");
            writer.WriteLine("\t\t<td> <b> x </b> </td>");
            writer.WriteLine("\t\t<td> <b> Sin(x) </b> </td>");
            writer.WriteLine("\t\t<td> <b> cos(x) </b> </td>");
            writer.WriteLine("\t<tr>");

            //So read it again, this time explicitly marking , as a deliminator
            char[] deliminators = {' ','\t','\n',','};//\n is pointless in this context, as newline terminates ReadLine
            var options = StringSplitOptions.RemoveEmptyEntries;

            string  line;
            //an alternative is string[] lines = file.ReadAllLines(argv[0]);
            while((line = reader.ReadLine()) != null)
            {

                string[] words = line.Split(deliminators,options);

                foreach (string word in words)
                {

                    writer.WriteLine("\t<tr>");
                    double x = 0;
                    bool number = double.TryParse(word,out x);

                    writer.WriteLine("\t\t<td> <b> \""+word+"\" </b> </td>");
                    if (number)
                    {
                        if (double.IsNaN(x))//NaN *is* technically a number so it passes the first test, but you might still want to catch it
                        {
                            Error.WriteLine($"x={x} is ignored");

                            writer.WriteLine("\t\t<td> NaN </td>");
                            writer.WriteLine("\t\t<td> NaN </td>");
                            writer.WriteLine("\t\t<td> NaN </td>");

                        }
                        else
                        {
                            //Write raw to log
                            Error.WriteLine($"x={x} , Sin(x)={Sin(x)}, cos(x)={Cos(x)}");

                            //Write html
                            writer.WriteLine($"\t\t<td> {x} </td>");
                            writer.WriteLine($"\t\t<td> {Sin(x)} </td>");
                            writer.WriteLine($"\t\t<td> {Cos(x)} </td>");
                        }
                    }
                    else
                    {
                        Error.WriteLine(word+" is NOT a number");
                        writer.WriteLine("\t\t<td> NaN </td>");
                        writer.WriteLine("\t\t<td> NaN </td>");
                        writer.WriteLine("\t\t<td> NaN </td>");

                    }
                    writer.WriteLine("\t</tr>");
                }

            }

            writer.WriteLine("</table>");

            reader.Close();
            writer.Close();

        }
        catch(System.Exception E)
        {
            Error.WriteLine("Error: "+E);
            return 2;
        }

        return 0;

    }
}
