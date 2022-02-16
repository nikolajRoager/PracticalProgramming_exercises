using System.IO;
using static System.Console;
using System;

class main
{
    static public int Main(string[] argv)
    {
        foreach(string arg in argv)
        {
            try
            {
                Write(double.Parse(arg)+" ");
            }
            catch(System.Exception E)
            {

            }
        }
        WriteLine();
        return 0;
    }
}
