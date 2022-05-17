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

        //The function we want to train the net to simulate
        Func<double,double> target = (x)  => Cos(5*x-1)*Exp(-x*x);
        Func<double,double> d_target = (x)  => -(x*Cos(5*x-1)+5*Sin(5*x-1) )*Exp(-x*x);
        //Gaussian activation function
        Func<double,double> activation = (X)  => X*Exp(-X*X);
        Func<double,double> d_activation = (X)  => (1-2*X*X)*Exp(-X*X);
        Func<double,double> antid_activation = (X)  => -0.5*Exp(-X*X);
        neural_network trained = new neural_network(8,activation,d_activation,antid_activation,-1,1);

        vector training_x = new vector(16);
        vector training_y = new vector(16);
        for (int i=0; i<16; ++i)
        {
            double x = i/8.0-1.0;//Train from 0 to 4, that is where the interesting thigns happen
            training_x[i]=x;
            training_y[i]=target(x);
            double true_dy =d_target(x);
            (double true_antid_yx ,double Err,int steps) = quad.integrate(target,-1,x);//I could not solve this analytically (or rather I could, but it involves the error function and I need to approximate that anyway
			Error.WriteLine($"{training_x[i]}\t{training_y[i]}\t{true_dy }\t{true_antid_yx}");
        }

        trained.train(training_x,training_y);

        //Apparently this should allow pyxplot to put multiple data in the same file
        Error.WriteLine("");
        Error.WriteLine("");

        (double fstart, double dfstart, double Fstart)=trained.response(-1);
        for (double x = -1; x <= 1; x+=0.01)
        {
            (double f, double df, double F)=trained.response(x);

            Error.WriteLine($"{x}\t{f}\t{df}\t{F-Fstart}");
        }
        return 0;
    }
}
