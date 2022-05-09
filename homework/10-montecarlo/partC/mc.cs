using System;
using static System.Math;
//using static System.Console;

public static class montecarlo
{

    public static (double,double) strat_box(Func <doublelist ,double> F, doublelist a, doublelist b, int N, int N_min = -1, int rec_count=0, int N_reuse=0, double sum_reuse=0, double sum2_reuse=0)
    {

        int dim=a.size;
        if (dim != b.size)
            throw new System.Exception("box corners did not have matching coordinate size");

        if(N_min == -1)
            N_min = Max(N/16,64);

        int MAX_REC =-1;//Set to -1 = no upper limit





        double V=1;
        for(int i=0;i<dim;i++)V*=b[i]-a[i];


        //I was supposed to measure the sub-variance ... without doing the full monte carlo on each half, but I am clearly too STUPID for that, I do not see any way I can do get the so called sub variance without being allowed to check multiple points, so what ISTUPIDLY does, is I divide N into 1/4 and 3/4, and use the first half points to do a normal plain monte carlo integral on the entire interval, calculating the variance of all possible half-intervals. Then, in the end, I use this variance to decide how to divide the remaining points between the two halves

        //Either this is the final sum, or just an estimate using a quarter
        int N_firstpass = (N <= N_min || rec_count == MAX_REC) ? N : Max(N/16,N_min);
        int N_secondpass = N-N_firstpass;

        //We are going to need to save how many points were already on each side, so we don't have to calculate them again
        int[] n_left = new int[dim];
        int[] n_right = new int[dim];

        doublelist x = new doublelist(dim);


        doublelist  sum_left = new doublelist( dim );
        doublelist  sum_right = new doublelist( dim );
        doublelist  sum2_left = new doublelist( dim );
        doublelist  sum2_right = new doublelist( dim );
        double sum=0;
        double sum2=0;


        var generator = new Random();

        for ( int i =0; i<N_firstpass ; i ++)
        {
            //Get a truly random points
            for (int k=0;k<dim;k++)
            {
                x[k]=a[k]+generator.NextDouble()*(b[k]-a[k]);
                System.Console.Error.Write($"{x[k]}\t");
            }
            System.Console.Error.Write($"\n");

            double fx=F(x);
            if (double.IsNaN(fx) || double.IsInfinity(fx))
            {//This has happened once for me, it is not very likely, but it can happen, just skip i
                --i;
            }
            else
            {
                sum+=fx;
                double fx2=fx*fx;
                sum2+=fx2;

            for ( int k =0; k<dim; k++)
            {
                if (x[k]<(a[k]+b[k])/2)
                {
                    n_left [k]++;
                    sum_left [k]+=fx ;
                    sum2_left [k]+=fx2 ;

                }
                else
                {
                    n_right [k]++;
                    sum_right [k]+=fx ;
                    sum2_right [k]+=fx2 ;
                }
            }


            }
        }

        //If too small, return this estimate here
        if (N<N_min || rec_count == MAX_REC )
        {
            //Unfortunately, I can only know which points from the run directly ahead of this are inside this half, i.e. I can not do know if a point on the full interval is within the first or second quarter.
            double mean=(sum+sum_reuse)/(N+N_reuse), sigma2=((sum2+sum2_reuse)/(N+N_reuse)-mean*mean)*V*V/(N+N_reuse);
            return (mean*V,rec_count ==0 ? Sqrt(sigma2): sigma2);
        }

        //Check which dimension has the greatest difference between left and right
        int kdiv =0; double maxvar=0;

        //And what is the estimated error made in each of the two intervals
        double sigma2_left=0;
        double sigma2_right=0;

        for(int k = 0; k<dim ; ++k)
        {
            //LAUGHABLY I can not do reuse the points from the call just before this, since I don't know if one previous point inside the half we are in now is in the first or second quarter, this is VERY BAD I know
            double mean_left = sum_left[k]/n_left[k];
            double mean_right = sum_right[k]/n_right[k];
            //Ok this sigma2_left/right still need to be multiplied by V/2, but that is constant so no need to actually do that
            double Var=Abs ( mean_right -mean_left) ;
            if ( Var>=maxvar)
            {
                maxvar=Var;kdiv=k;
                sigma2_left =((sum2_left[k])/(n_left[k])-mean_left*mean_left)/(n_left[k]);
                sigma2_right =((sum2_right[k])/(n_right[k])-mean_right*mean_right)/(n_right[k]);
            }

        }

        doublelist a2 = a.copy();
        doublelist b2 = b.copy();



        a2 [kdiv]=(a[kdiv]+b [kdiv]) / 2;
        b2 [kdiv]=(a[kdiv]+b [kdiv]) / 2 ;


        //Based on the 1/4 of the points we just waited, who do we think we need to add more detail to?
        double total_sig2 = (sigma2_left+sigma2_right);
        double leftness = ( (total_sig2==0) ? 0.5 :  sigma2_left/(total_sig2));




        //We need at least one point in each half
        int N_left = Max(Min( (int)(N_secondpass*leftness),N_secondpass-1) ,1);//There must be at least one point in each half
        (double integral_left,double sig2_left) =strat_box(F , a , b2 , N_left ,N_min ,rec_count+1,n_left[kdiv], sum_left[kdiv], sum2_left[kdiv]) ;


        (double integral_right,double sig2_right) =strat_box(F , a2 , b , N_secondpass-N_left,N_min ,rec_count+1,n_right[kdiv], sum_right[kdiv], sum2_right[kdiv]) ;

        return (integral_left+integral_right, rec_count ==0 ? Sqrt(sig2_left+sig2_right): sig2_left+sig2_right);//Return sigma^2 if this is NOT the final one
    }


}
