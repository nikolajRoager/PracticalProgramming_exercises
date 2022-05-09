using System;

//A single hidden layer neural network
public class neural_network
{
	int n; // number of hidden neurons
	Func<double,double> activation; // activation function
	vector p; // network parameters
	public neural_network(int _n,Func<double,double> f,Func<double,double> trg_f)
    {
        n = _n;
        activation=f;
        p = new vector(3*n);//2 parameters and 1 weight per hidden neuron
        for (int i = 0; i < n; ++i)//Default, equal weights
        {
            //STUPIDLY use the function we want in the end as a starting guess
            p[i*3+0]=i*2.0/n-1.0;
            p[i*3+1]=0.1;//0.05;
            p[i*3+2]=trg_f(i*2.0/n-1.0); //p[i*3+2]
        }
    }
	public double response(double x)
    {
		//For a single hidden layer, this is really rather trivial
        double Out = 0;
        for (int i = 0; i < n; ++i)
            Out+=activation((x-p[i*3])/p[i*3+1])*p[3*i+2];
        return Out;
	}
	public double response(double x, vector P )
    {
		//For a single hidden layer, this is really rather trivial
        double Out = 0;
        for (int i = 0; i < n; ++i)
            Out+=activation((x-P[i*3])/P[i*3+1])*P[i*3+2];
        return Out;
	}
	public void train(vector x,vector y)
    {
		//train the network using our minimization algorithm

        int N = x.size;
        if (N != y.size)
            throw new ArgumentException("Training data should have same size");

        Func<vector,double> deviation= delegate(vector this_p)
        {
            double sum=0;
            for (int i = 0; i < N; ++i)
                sum+= Math.Pow(response(x[i],this_p)-y[i],2);
            return sum;
        };

        System.Console.Write($"Having had optimized such that deviation {deviation(p)} -> ");

        (vector p_new, int steps) = minimizer.downhill_simplex(deviation,p,1e-5);

        System.Console.WriteLine($" {deviation(p)} in {steps} steps");
        p = p_new;

	}
}
