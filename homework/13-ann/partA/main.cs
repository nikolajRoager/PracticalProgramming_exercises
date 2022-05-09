using System;
using static System.Math;
using static System.Console;

public static class main
{


    //double precision approximation
    public static int Main(string[] args)
    {
        WriteLine($"--------------------------------------------------------");
        WriteLine($"Demonstrating Neural Network approximations of functiosn");
        WriteLine($"--------------------------------------------------------");

        //The function we want to train the net to simulate
        Func<double,double> target = (x)  => Cos(5*x-1)*Exp(-x*x);
        //Gaussian activation function
        Func<double,double> activation = (X)  => Exp(-X*X);
        neural_network trained = new neural_network(32,activation,target);

        vector training_x = new vector(200);
        vector training_y = new vector(200);
        for (int i=0; i<200; ++i)
        {
            double x = i/100-1.0;//Train from 0 to 4, that is where the interesting thigns happen
            training_x[i]=x;
            training_y[i]=target(x);
        }

        trained.train(training_x,training_y);


        neural_network untrained = new neural_network(32,activation,target);
        for (double x = -1; x <= 1; x+=0.01)
            Error.WriteLine($"{x}\t{trained.response(x)}\t{target(x)}\t{untrained.response (x)}");//We make sure to be offset from our training data, to make sure our network has not just learned to memorize the data
        return 0;
    }

}
