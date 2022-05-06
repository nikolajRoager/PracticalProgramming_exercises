using System;
using static System.Console;
using static System.Math;
using static optimizer;
using static vector;
using System.IO;
using System.Text;


public static class main
{


    //double precision approximation
    public static bool approx(double a,double b,double tau=1e-9,double eps=1e-9)
    {
        if (Abs(a-b)<tau)
            return true;
        if (Abs(a-b)/(Abs(a)+Abs(b))<eps)
            return true;
        return false;
    }

    public static int Main(string[] args)
    {
        WriteLine($"----------------------------------------------");
        WriteLine($"Quasi newton optimization on Higs data");
        WriteLine($"----------------------------------------------");

        bool verbose = false;

        string file="null";

        foreach(string s in args)
        {
            if (0==String.Compare(s,"-v"))
                verbose = true;
            else
                file = s;
        }

        if (String.Compare("null",file)==0)
        {
            WriteLine("File "+file+" not given");
            return 1;
        }

        double[] higgsdata;
        int higgsdata_length;
        try
        {


            //So read it again, this time explicitly marking , as a deliminator
            char[] deliminators = {' ','\t','\n',','};//\n is pointless in this context, as newline terminates ReadLine
            var options = StringSplitOptions.RemoveEmptyEntries;

            string[] lines = File.ReadAllLines(file);

            higgsdata_length = lines.Length;
            higgsdata = new double[3*higgsdata_length];


            for (int i = 0; i < lines.Length; ++i)
            {
                string[] words = lines[i].Split(deliminators,options);

                if (words.Length!=3)
                {
                    WriteLine(file+" did not have right number of elements");
                    return 1;
                }

                for (int j = 0; j<3 ; ++j)
                {

                    bool number = double.TryParse(words[j],out higgsdata[3*i+j]);

                    if (number)
                    {
                        if (double.IsNaN(higgsdata[3*i+j]) || double.IsInfinity(higgsdata[3*i+j]))//NaN *is* technically a number so it passes the first test, but you might still want to catch it
                        {
                            WriteLine($"Got evil number {higgsdata[3*i+j]}");
                            return 1;


                        }
                    }
                    else
                    {
                        WriteLine(words[j]+" is NOT a number");
                        return 1;

                    }


                }

            }
/*
            while((line = reader.ReadLine()) != null)
            {

                string[] words = line.Split(deliminators,options);
                double[] higgsdata = new double;

                foreach(string word in words)
                {
                    double x = 0;
                    bool number = double.TryParse(word,out x);

                    Out.Write("\""+word+"\"");
                    if (number)
                    {
                        if (double.IsNaN(x))//NaN *is* technically a number so it passes the first test, but you might still want to catch it
                        {
                            Error.WriteLine($"x={x} is ignored");

                            Out.WriteLine(", NaN, NaN, NaN ");

                        }
                        else
                        {
                            //Write to document
                            Out.WriteLine($"x={x} , Sin(x)={Sin(x)}, cos(x)={Cos(x)}");
                        }
                    }
                    else
                    {
                        Error.WriteLine(word+" is NOT a number");
                        Out.WriteLine(", NaN, NaN, NaN ");

                    }
                }
            }*/



        }
        catch(System.Exception E)
        {
            Error.WriteLine("Error: "+E);
            return 2;
        }



        WriteLine($"\nNow fitting A,Γ and m to deviation function");
        //Breit Wigner function
        Func<vector,double> D = delegate(vector X)
        {
            double Out=0;
            for (int i = 0; i < higgsdata_length; ++i)
            {
                double Ei    = higgsdata[i*3];//energy
                double Sigi  = higgsdata[i*3+1];//cross section
                double DSigi = higgsdata[i*3+2];//its error
                double FEiX = ( X[2]/((Ei-X[0])*(Ei-X[0])+X[1]*X[1]/4) );
                Out += Pow((FEiX-Sigi)/DSigi,2);
            }

            return Out;
        };
        WriteLine($"Starting at guess (m,Γ,A) = (121,10,10) with precision 10^-5.");
        WriteLine($"YES, I know that is crazy close, but there is a lot of local minima around. Now Running ...");
        (vector root1, int steps1) = qnewton(D,new vector(121,10,10),1e-5,verbose );


        WriteLine("");
        //As the Γ is squared the minimum might accidentally find it to be negative, just go with the positive solution
        WriteLine($"In {steps1} steps: Has m={root1[0]} GeV/c^2 Γ={Abs(root1[1])}  A={root1[2]}");

        WriteLine("");
        if (approx(125.3,root1[0],0.6,0.0))
        {
            WriteLine("PASS this is within the experimental error given of the true data");
        }
        else
        {
            WriteLine("FAIL this is not within the experimental error given of the true data");
        }

        WriteLine("I am not sure if I was meant to find the true answer exactly, or just there about, what I get is certainly somewhat close");


        return 0;
    }

}
