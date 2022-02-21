using System.IO;
using static System.Console;
using System;

class main
{
    static public int Main(string[] argv)
    {
        /*
        if (argv.Length()!=1)
        {
            Out.WriteLine("Illegal input, enter filename");
            Error.WriteLine("Illegal input, enter filename");
            return 1;
        }*/

        var reader = new System.IO.StreamReader("In.txt");
        var writer = new System.IO.StreamWriter("Out.txt");




        writer.WriteLine("Write thing");
        string input = reader.ReadLine();

        writer.WriteLine(input);
        reader.Close();
        writer.Close();
        return 0;
    }
}
