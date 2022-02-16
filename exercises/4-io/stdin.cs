using System.IO;
using static System.Console;
using System;

class main
{
    static public int Main()
    {
        Out.WriteLine("Lorem Ipsum");
        Error.WriteLine("Printed text");
        TextWriter cout = Out;
        TextReader cin = In;
        cout.WriteLine("Dolor Sit Amat");


        Console.Out.WriteLine("Write thing");
        string input = cin.ReadLine();
        char[] deliminators = {' ','\t','\n'};
        var options = StringSplitOptions.RemoveEmptyEntries;
        string[] words = input.Split(deliminators,options);
        foreach(string word in words)
        {
            double x = double.Parse(word);
            Console.Out.WriteLine($"x={x}");
        }
        return 0;
    }
}
