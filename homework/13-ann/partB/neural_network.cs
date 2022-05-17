using System;
using static System.Math;
using static System.Console;


//A single hidden layer neural network
public class neural_network
{
	private int n; // number of hidden neurons
	private Func<double,double> activation; // activation function
	private Func<double,double> d_activation;
	private Func<double,double> antid_activation;
	private vector p; // network parameters

    double antid_offset;//I want the predicted anti-derivative to be 0 at the start of the interval
    double interval_start;

	public neural_network(int _n,Func<double,double> f,Func<double,double> df,Func<double,double> F, double a, double b)
    {
        n = _n;
        activation=f;
        d_activation=df;
        antid_activation=F;
        p = new vector(3*n);//2 parameters and 1 weight per hidden neuron


        for (int i = 0; i < n; ++i)//Default, equal weights
        {
            //STUPIDLY use the function we want in the end as a starting guess
            p[i*3+0]=i*(b-a)/(n-1)+a;
            p[i*3+1]=1.0;
            p[i*3+2]=1.0;//If this is NOT 0, then the optimizer will not know it is permitted to make it negative, and it must be so
        }
    }
	public (double,double,double) response(double x)
    {
		//For a single hidden layer, this is really rather trivial
        double Out = 0;
        double d_Out = 0;
        double antid_Out = 0;
        for (int i = 0; i < n; ++i)
        {
            Out+=activation((x-p[i*3])/p[i*3+1])*p[3*i+2];
            d_Out+=d_activation((x-p[i*3])/p[i*3+1])*p[3*i+2]/p[i*3+1];
            antid_Out+=antid_activation((x-p[i*3])/p[i*3+1])*p[3*i+2]*p[i*3+1];
        }
        return (Out,d_Out,antid_Out);
	}

	public void train(vector x,vector y)
    {
		//train the network using our minimization algorithm

        int N = x.size;
        if (N != y.size)
            throw new ArgumentException("training data should have same size");


		Func<vector, double> deviation = this_p => {
			this.p = this_p;
			double sum = 0;
			for (int k=0; k<x.size; k++)
            {
                (double Out,double d_Out,double antid_Out) =response(x[k]);
				sum += Pow(Out - y[k], 2);
			}
			return sum/x.size;
		};


        System.Console.Write($"Having optimized such that deviation {deviation(p)} -> ");

        (vector p_new, int steps) = minimizer.qnewton(deviation,p,1e-3);

        System.Console.WriteLine($" {deviation(p)} in {steps} steps");
        p = p_new;

	}
}
