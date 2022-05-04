using System;
using static System.Console;
using static matrix;
using static System.Math;
using static fit;

public static class main
{
    public static int Main(string[] argv)
    {
        if (argv.Length!=1)
        {
            Error.WriteLine("Input not valid, need 1 argument: datafile.tsv");
            return 1;
        }

        int h=0;//As tall a matrix as we need to load

        //Pre declare so this exists outside the loading environment
        double[]  n_list;
        double[]  ts_list;
        double[]  dts_list;

        try
        {

            char[] deliminators = {' ','\t','\n',','};//\n is pointless in this context, as newline terminates ReadLine
            var options = StringSplitOptions.RemoveEmptyEntries;

            string  [] lines = System.IO.File.ReadAllLines(argv[0]);
            n_list=new double[lines.Length];
            ts_list=new double[lines.Length];
            dts_list=new double[lines.Length];


            foreach(string line in lines )
            {


                string[] words = line.Split(deliminators,options);

                if (words.Length!=3)
                {
                    Error.WriteLine("File should have 3 columns (x,y,δy)");
                    return 2;
                }

                double x=0;
                double y=0;
                double dy = 0;
                bool number = double.TryParse(words[0],out x) && double.TryParse(words[1],out y) && double.TryParse(words[2],out dy);

                if (number)
                {
                    if (double.IsNaN(x) || double.IsNaN(y) || double.IsNaN(dy))//NaN *is* technically a number so it passes the first test, but you might still want to catch it
                    {
                        Error.WriteLine(line+" NOT A NUMBER");
                        return 1;
                    }
                }
                else
                {
                    Error.WriteLine(line+" Double conversion failed");
                    return 1;

                }
                n_list[h]=x;
                ts_list[h]=y;
                dts_list[h]=dy/y;
                ++h;
            }
        }
        catch(System.Exception E)
        {
            Error.WriteLine("Error: "+E);
            return 2;
        }


        double[] Params;
        matrix Sigma;
        try
        {
            (Params,Sigma) = LSfit(n_list,ts_list,dts_list,
            //fit to a a+bx^3
            new Func<double,double>[] { z => 1.0, z => z*z*z }
            );
        }
        catch(System.Exception E)
        {
            Error.WriteLine("Error: "+E);
            return 2;
        }


        Error.WriteLine(Sigma.getString("(rounded to 3 digits) Covariance matrix = "));

        double Sig1 = Sqrt(Sigma[1,1]);
        double Sig0 = Sqrt(Sigma[0,0]);

        Error.WriteLine($"Estimated parameters a= {Params[0]} ± {Sig0}; b = {Params[1]} ± {Sig1} ");
        //Plot of fit with and without applying errors in either direction (Yes, this is part C and A together, sorry
        for (double  n = 0 ; n<n_list[n_list.Length-1]; n+=0.1)//bit weird having n be a floating point number, but oh well
        {
            WriteLine($"{n}\t{ Params[0]+Params[1]*n*n*n }\t{ Params[0]+Sig0+(Params[1]+Sig1)*n*n*n }\t{ Params[0]-Sig0+(Params[1]-Sig1)*n*n*n}");
        }

        return 0;
    }

}
