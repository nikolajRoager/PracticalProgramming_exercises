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
        //Gaussian activation function
        Func<double,double> activation = (X)  => X*Exp(-X*X);
        neural_network trained = new neural_network(8,activation,-1,1);

        vector training_x = new vector(64);
        vector training_y = new vector(64);
        for (int i=0; i<64; ++i)
        {
            double x = i/32.0-1.0;//Train from 0 to 4, that is where the interesting thigns happen
            training_x[i]=x;
            training_y[i]=target(x);


			Error.WriteLine($"{training_x[i]} {training_y[i]}");
        }

        trained.train(training_x,training_y);

        //Apparently this should allow pyxplot to put multiple data in the same file
        Error.WriteLine("");
        Error.WriteLine("");
        neural_network untrained = new neural_network(8,activation,-1,1);

        for (double x = -1; x <= 1; x+=0.01)
            Error.WriteLine($"{x}\t{trained.response(x)}\t{untrained.response(x)}");//We make sure to be offset from our training data, to make sure our network has not just learned to memorize the data
        return 0;
    }
}
