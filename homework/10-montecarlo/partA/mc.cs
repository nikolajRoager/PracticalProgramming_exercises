using System;
using static System.Math;
//using static System.Console;

public static class montecarlo
{
    public static (double,double) plain_box(Func <doublelist ,double> F, doublelist a, doublelist b, int N)
    {
        int dim=a.size;
        if (dim != b.size)
            throw new System.Exception("box corners did not have matching coordinate size");

        //Calculate the length/area/volumne/hypervolume or whatever
        double V=1;
        for(int i=0;i<dim;i++)V*=b[i]-a[i];

        //Get the integral of the function and its squares
        double sum=0,sum2=0;

        doublelist x=new doublelist(dim);


        var generator = new Random();
        int offset=0;//used for skipping bad points
        for(int i=0;i<N;i++)
        {
            //Get a truly random points
            for (int k=0;k<dim;k++)
                x[k]=a[k]+generator.NextDouble()*(b[k]-a[k]);

            double fx=F(x);
            if (double.IsNaN(fx) || double.IsInfinity(fx))
            {//This has happened once for me, it is not very likely, but it can happen, just skip i
                --i;
                ++offset;//Effectively skips i
            }
            else
            {
                sum+=fx;
                sum2+=fx*fx;
            }
        }

        //Calculate the mean and the variation, use this to get the estimate and the error;
        double mean=sum/N, sigma=Sqrt(sum2/N-mean*mean);
        return (mean*V,sigma*V/Sqrt(N));
    }

}
