using System;
using static System.Console;

class main
{
    public static int Main(string[] argv)
    {
        //Read the same data as in the other parts
        if (argv.Length!=1)
        {
            Error.WriteLine("Need exactly 1 argument, name of file");
            return 1;
        }
        var reader = new System.IO.StreamReader(argv[0]);
        linkedList<double[]> list = new linkedList<double[]>();//I want to use the same data

        var options = StringSplitOptions.RemoveEmptyEntries;
        char[] deliminators = {' ','\t'};
        for(string line = reader.ReadLine(); line!=null; line = reader.ReadLine())
        {
            var words = line.Split(deliminators,options);
            int n = words.Length;
            var numbers = new double[n];
            for(int i=0;i<n;i++) numbers[i] = double.Parse(words[i]);
            list.push(numbers);
        }

        //This is the method suggested by the exercis
        WriteLine("Using .next() to advance trhough list");
        for( list.start(); list.current != null; list.next())
        {
            foreach(var number in list.current.item)
                Write($"{number:e} ");
            WriteLine();
        }

        //This is the way I prefer to interact with a list
        WriteLine("Using .get(i) function to loop as a traditional array");
        for(int i=0;i<list.size;i++)
        {
            var numbers = list.get(i);
            foreach(var number in numbers)
                Write($"{number:e} ");
                WriteLine();
        }

        return 0;
    }
}
