using System;
using static System.Console;
using static System.Math;

public static class main{




    //double precision approximation
    public static int Main(string[] args)
    {
        WriteLine($"--------------------------------------------------------");
        WriteLine($"Demonstrating Neural Network approximations of functiosn");
        WriteLine($"--------------------------------------------------------");

        //The (ordinary) differential equation we want to solve, I think a damped harmonic oscillator
        //I imagine this is a = ddyddt, v = dxdt and m a = -alpha v-k y*y, so
        Func<double,double,double,double,double> phi= (ddyddt,dydt,y,t)  => -ddtddt-dydt-y*y;
        //Gaussian activation function
        Func<double,double> activation = (X)  => Exp(-X*X);
        //Func<double,double> d_activation = (X)  => -2*X*Exp(-X*X);
        //Func<double,double> dd_activation = (X)  => 2*(2*X*X-1)*Exp(-X*X);

        neural_network trained = new neural_network(16,activation,0,8);

        vector training_x = new vector(16);
        vector training_y = new vector(16);
        for (int i=0; i<16; ++i)
        {
            double x = i/8.0-1.0;//Train from 0 to 4, that is where the interesting thigns happen
            training_x[i]=x;
        }

        trained.train(training_x,training_y);

        //Apparently this should allow pyxplot to put multiple data in the same file
        Error.WriteLine("");
        Error.WriteLine("");

        (double fstart, double dfstart, double Fstart)=trained.response(-1);
        for (double x = -1; x <= 1; x+=0.01)
        {
            (double f, double df, double F)=trained.response(x);

            Error.WriteLine($"{x}\t{f}");
        }
        return 0;
    }
}
