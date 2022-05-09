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


    public static (double,double) quasi_box(Func <doublelist ,double> F, doublelist a, doublelist b, int N)
    {
        int dim=a.size;
        if (dim != b.size)
            throw new System.Exception("box corners did not have matching coordinate size");

        //Calculate the length/area/volumne/hypervolume or whatever
        double V=1;
        for(int i=0;i<dim;i++)V*=b[i]-a[i];

        //Get the integral of the function
        double sum=0,sum1=0;//To get the error, I will use two different patterns, sum and sum1 ar ethe default and one using a different pattern

        doublelist x=new doublelist(dim);
        doublelist x1=new doublelist(dim);


        var generator = new Random();
        int offset=0;//used for skipping bad points
        for(int i=0;i<N;i++)
        {
            //Get a truly random points, get two different by using two different bases, the integer is translated into different primes
            doublelist Quasi = halton (i+offset,dim);
            doublelist Quasi1 = halton (i+offset,dim,8);//Use different primes to get different patterns
            for (int k=0;k<dim;k++)
            {
                x [k]=a[k]+Quasi [k]*(b[k]-a[k]);
                x1[k]=a[k]+Quasi1[k]*(b[k]-a[k]);
            }
            double fx =F(x );
            double fx1=F(x1);

            if (double.IsNaN(fx) || double.IsInfinity(fx) || double.IsNaN(fx1) || double.IsInfinity(fx1))
            {//This has happened once for me, it is not very likely, but it can happen, just skip i
                --i;
                ++offset;//Effectively skips i
            }
            else
            {
                sum+=fx;
                sum1+=fx1;
            }
        }

        //Calculate the means
        double mean=sum/N, mean1 = sum1/N;
        return (mean*V,Abs(mean*V-mean1*V));//Use the different methods to estimate error, as suggested
    }

    static double corput(int n , int Base )
    {
        double q=0 , bk= 1.0/Base;

        while ( n>0)
        {
            q += (n%Base)*bk ;
            n /= Base;
            bk /= Base;
        }

        return q;
    }

    //Picking a different "pattern" picks different prime bases, effectively giving different results
    static doublelist halton( int n , int d, int pattern=0)
    {
        int[] Bases = { 2 , 3 , 5 , 7 , 11 , 13 , 17 , 19 , 23 , 29 , 31 , 37 , 41 , 43 , 47 , 53 , 59 , 61 , 67 };
        if (Bases.Length <d)
            throw new System.Exception("Dimension too large for halton implementation");
        doublelist x = new doublelist(d);
        for(int i =0; i <d ; i++) x[i]= corput(n , Bases[ (i+pattern)%Bases.Length ] ) ;

        return x;
    }

}
