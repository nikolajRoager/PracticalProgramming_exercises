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

        WriteLine($"List before remove");
        for(int i=0;i<list.size;i++)
        {
            var numbers = list.data[i];
            foreach(var number in numbers)
                Write($"{number:e} ");
                WriteLine();
        }
        WriteLine($"Size: {list.size}, capacity {list.capacity}");

        WriteLine($"Removing element {list.size/2}");
        list.remove(list.size/2);
        WriteLine($"Removing element {list.size/2}");
        list.remove(list.size/2);
        for(int i=0;i<list.size;i++)
        {
            var numbers = list.data[i];
            foreach(var number in numbers)
                Write($"{number:e} ");
                WriteLine();
        }
        WriteLine($"Size: {list.size}, capacity {list.capacity}");

        return 0;
    }
}
