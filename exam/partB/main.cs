using System;
using static System.Console;
using static System.Math;
using static bicubic;

public static class main
{
    public static int Main(string[] argv)
    {
        Error.WriteLine("-----------------------------");
        if (argv.Length!=1)
        {
            Error.WriteLine("Input not valid, need 1 argument (input data)");
            return 1;
        }
        //I use the "error" channel for normal logging stuff
        Error.WriteLine($"Loading data: "+argv[0]);


        //Declare the lists before loading them, I want them to be seen outside the try environment
        vector xlist;
        vector ylist;
        matrix zdata;

        int n=0;
        int m=0;

        try
        {
            //Read data into vectors and matrices
            string[] lines = System.IO.File.ReadAllLines(argv[0]);
            char[] deliminators = {'\t',' ',','};
            var options = StringSplitOptions.RemoveEmptyEntries;

            n = lines.Length-1;
            ylist=new vector(n);
            //Line 0, the x vector
            {
                string[] words = lines[0].Split(deliminators,options);
                m = words.Length-1;//The first entry doesn't really matter
                xlist=new vector(m);
                zdata=new matrix(n,m);

                for (int i = 0; i < m; ++i)
                {
                    double X=0;//Yeah, C# does not let me set xlist[i] directly here
                    bool number = double.TryParse(words[i+1],out X );
                    xlist[i]=X;
                    if (!number)
                    {
                        Error.WriteLine("Not valid number in line 0 : "+words[i+1]);
                        return 1;
                    }
                }

            }

            if (m<=0 || n<=0)
            {
                Error.WriteLine("Input not valid, should have positive width and height");
                return 1;
            }




            for(int i =0; i < n; ++ i )
            {
                string[] words = lines[i+1].Split(deliminators,options);

                //The first entry is the y vector elements
                double Y=0;
                bool number = double.TryParse(words[0],out Y);
                ylist[i]=Y;

                if (!number)
                {
                    Error.WriteLine($"Not valid number in line {i+1} : "+words[0]);
                    return 1;
                }

                for (int j = 0; j < m; ++j)
                {
                    double Z=0;
                    number = double.TryParse(words[j+1],out Z);
                    zdata[i,j]=Z;

                    if (!number)
                    {
                        Error.WriteLine($"Not valid number in line {i+1} : "+words[j+1]);
                        return 1;
                    }
                }
            }
        }
        catch(Exception E)
        {
            Error.WriteLine("Could not load input, got error "+E);
            return 1;

        }




        Error.WriteLine("Setting up bilinear interpolation");
        bicubic interpolator= new bicubic(xlist,ylist,zdata);
        Error.WriteLine("Verifying the solution for all boundary conditions ...");
        if (!interpolator.verify_all())
            return 1;
        else
            Error.WriteLine(" --- PASS: Solution matches all boundary conditions within relative and absolute error 10^-5");

        Error.WriteLine("Calculating and saving high resolution output");

        vector xout = new vector(m*4);

        //Ok, now get the output and print to a list gnuplot can understand

        Write(m*4);
        for (int i = 0; i < m*4; ++i)
        {
            xout[i]=(xlist[m-1]-xlist[0])*i/(m*4-1)+xlist[0];
            Write($" {xout[i]}");
        }

        Write("\n");
        for (int i = 0; i < n*4; ++i)
        {
            double y =(ylist[n-1]-ylist[0])*i/(n*4-1)+ylist[0];
            Write($" {y}");
            for (int j = 0; j < m*4; ++j)
            {
                Write($" {interpolator.interpolate(xout[j],y)}");
            }
            Write("\n");
        }
        Error.WriteLine("-----------------------------");
        return 0;
    }

}
