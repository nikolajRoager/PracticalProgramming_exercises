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
        double[]  t_list;
        double[]  a_list;
        double[] da_list;

        try
        {

            char[] deliminators = {' ','\t','\n',','};//\n is pointless in this context, as newline terminates ReadLine
            var options = StringSplitOptions.RemoveEmptyEntries;

            string  [] lines = System.IO.File.ReadAllLines(argv[0]);
            t_list=new double[lines.Length];
            a_list=new double[lines.Length];
            da_list=new double[lines.Length];


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
                t_list[h]=x;
                a_list[h]=Log(y);
                da_list[h]=dy/y;
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
            (Params,Sigma) = LSfit(t_list,a_list,da_list,
            //Linear fit, ln(a) is one parameter, λ another
            new Func<double,double>[] { z => 1.0, z => -z }
            );
        }
        catch(System.Exception E)
        {
            Error.WriteLine("Error: "+E);
            return 2;
        }

        Error.WriteLine("----PART A----");
        Error.WriteLine($"Fit done with parameters: ln(a)= {Params[0]}, λ= {Params[1]} ");

        Error.WriteLine(Sigma.getString("(rounded to 3 digits) Covariance matrix = "));

        double Sig1 = Sqrt(Sigma[1,1]);
        double Sig0 = Sqrt(Sigma[0,0]);
        //Plot of fit with and without applying errors in either direction (Yes, this is part C and A together, sorry
        for (double this_t = t_list[0]; this_t<t_list[t_list.Length-1]; this_t+=0.1)
        {
            WriteLine($"{this_t}\t{ Exp(Params[0]-Params[1]*this_t) }\t{ Exp(Params[0]+Sig0-(Params[1]-Sig1)*this_t) }\t{ Exp(Params[0]-Sig0-(Params[1]+Sig1)*this_t) }\t{ Exp(Params[0]+Sig0-(Params[1]+Sig1)*this_t) }\t{ Exp(Params[0]-Sig0-(Params[1]-Sig1)*this_t) }");
        }

        Error.WriteLine($"Estimated parameters ln(a)= {Params[0]}  λ = {Params[1]}  halflife { (Log(2)/Params[1])}  days, compared to modern estimate (wikipedia) 3.6319 d");


        Error.WriteLine("----PART B----");
        Error.WriteLine($"Estimated parameters ln(a)= {Params[0]} ± {Sig0} λ = {Params[1]} ± {Sig1} halflife {Log(2)/Params[1]} ± {(Log(2)/(Params[1]*Params[1])) *Sqrt(Sigma[1,1])} days, compared to modern estimate (wikipedia) 3.6319 days (error presumably on scale of the final digit)");

        if ((Log(2)/(Params[1]*Params[1])*Sqrt(Sigma[1,1]))>Abs(Log(2)/Params[1]-3.6319))
            Error.WriteLine("Inside error of modern estimate");
        else
            Error.WriteLine("NOT Inside error of modern estimate");

        return 0;
    }

}
