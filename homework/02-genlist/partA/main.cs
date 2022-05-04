using System;
using static System.Console;

class main
{
    public static int Main(string[] argv)
    {
        if (argv.Length!=1)
        {
            Error.WriteLine("Need exactly 1 argument, name of file");
            return 1;
        }
        var reader = new System.IO.StreamReader(argv[0]);
        genlist<double[]> list = new genlist<double[]>();

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

        for(int i=0;i<list.size;i++)
        {
            var numbers = list.data[i];
            foreach(var number in numbers)
                Write($"{number:e} ");
                WriteLine();
        }

        return 0;
    }
}
