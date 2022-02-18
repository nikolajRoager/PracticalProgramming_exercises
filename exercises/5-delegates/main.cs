using System;
using static System.Console;

class main
{
    public static void Main()
    {
        WriteLine("A");
        genlist<int> list = new genlist<int>();
        WriteLine("B");

        list.push(0);
        list.push(1);
        list.push(2);
        list.push(3);
        WriteLine("C");
        for (int i = 0; i<list.data.Length; ++i)
            WriteLine($"data[{i}]={i}");

    }
}
