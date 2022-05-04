using System;
using static System.Console;
using static System.Math;

public static class main
{
    public static int Main(string[] argv)
    {
        double R_max =0;
        int N =0;
        if (argv.Length==2)
        {

            if (!double.TryParse(argv[0],out R_max))
            {
                Error.WriteLine("Input not valid, could not convert  "+argv[0]+" to double");
                return 1;
            }
            if (!int.TryParse(argv[1],out N))
            {
                Error.WriteLine("Input not valid, could not convert  "+argv[1]+" to int");
                return 1;
            }
        }
        else
        {
            Error.WriteLine("Input not valid, need 1 arguments, R_max and Number of points ");
            return 1;
        }

        double dr =R_max/(N+1);

        for (int i = 0; i < N; ++i)
        {
            //has a = 1 and L=0
            //Taken from table in Griffith's introduction to QM
            double r = dr*i;
            double S0 = r*2*Exp(-r);//u = r R_{10}(r)
            double S1 = -r*((1/Sqrt(2)*(1-0.5*r))*Exp(-r*0.5));
            double S2 = r*((2/(3*Sqrt(3)))*(1-(2.0/3.0)*r+(2.0/27)*r*r)*Exp(-r/3.0));
            double S3 = -r*(0.24*(1-0.75*r+0.125*r*r-1.0/192 * r*r*r)*Exp(-0.25*r));

            WriteLine($" {r}\t{S0}\t{S1}\t{S2}\t{S3}");

        }


        return 0;
    }

}
