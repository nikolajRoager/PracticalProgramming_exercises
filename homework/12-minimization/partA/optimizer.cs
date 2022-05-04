using System;
using static System.Console;
using static System.Math;
using static matrix;
using static vector;

public static class optimizer
{
    //Mirror function to maximize instead
    public static vector max_qnewton(Func<vector,double>f, vector x0, double acc=1e-2, bool verbose=false)
    {
        Func<vector,double> F = (X)  => -f(X);
        return  qnewton(F,x0,acc,verbose);
    }


    public static vector qnewton(Func<vector,double>f, vector x0, double acc=1e-2, bool verbose=false)
    {


        double little = Pow(2,-26);
        double alpha = 1e-4;//Used in acceptance condition for backtracking
        double epsilon = 1e-6;//Used to see if we should reset hessian

        vector x = x0.copy();//Better copy this, if the user wants to use the original input vector for something else.
        //Read the number of parameters the function outputs, and the number of parameters in the input

        int n = x.size;

        bool redo=true;//I have an emergency redo function, in case deltax is too small, in some rare cases, I have seen deltax be treated as a 0, breaking the algorithm uppon division, if that happen, we will retry here, with
        int recovery=0;//How many times have we had to do an emergency recovery? if we do this too often we abort
        int max_steps=10000;//should never need that much

        while (redo)
        {
            redo=false;//In 99% of cases, this will only need to be done once, but maybe we are on a computer where machine epsilon is smaller than expected.

            int step = -1;

            double fx=f(x);
            matrix B = new matrix(n,n);//Inverse hessian, start guess: identity
            do
            {//Each step
                ++step;

                //The idea is: Calculate the Gradient
                // ∇ f(x)
                // Let Delta x = - B ∇ f(x)
                // Let s = lambda Delta x
                // Perform backtracking lambda -> lambda/2 and update s until f(x+s)<f(x)+alpha dot(s,∇ f(x)) (or until lambda too small)
                // Let y = ∇f(x + s)-∇f(x) and u = s- By
                //
                // Symmetric Rank 1 update:
                // if dot(u,y)>epsilon
                //     let B = outer_product(u,u)/dot(u,y)
                // else
                //     let B = 1
                // endif


                //Get Jacobi matrix
                vector GradFx = new vector(n);


                for (int i = 0; i < n; ++i)
                {
                    //Get the input, offset on the relevant element
                    vector offsetx = x.copy();
                    double deltax = Max(Abs(x[i])*little,0.25*little);//The tiny step to use when calculating the Jacobi matrix, note I do not enjoy 0 divisions, and it is possible that the user guesses that x[k]=0, so I add a floor to deltax

                    offsetx[i]+=deltax;

                    double foffsetxk=f(offsetx);
                    offsetx[i]+=deltax;
                    GradFx[i] = (foffsetxk-fx)/deltax;


                    if(double.IsInfinity(GradFx[i]))//This is very very rare
                    {
                        //CRAP, deltax was too small, and we got a 0 division, we must retry with a larger little number... unless we have done that too many times already

                        if (little>0.25)
                            Error.WriteLine("ABORT: Got 0 division in Quasi Newton methods gradient too many times");

                        Error.WriteLine("WARNING: Got 0 division in Quasi Newton method gradient, this may be caused by your computer having less precision for double point numbers than expected. Will retry with a larger Delta x");
                        redo = true;
                        x = resize(x0.copy(),n);//Reset our guess, and try again with a larger little number
                        little*=2;


                        break;
                    }
                }

                if (!redo)//If finding the gradient worked
                {


                }


            if (GradFx.norm()<acc)
            {
                if (verbose)
                    WriteLine("BREAK DUE TO: gradient reached target");
                break;
            }

            if (step>=max_steps)//I am not sure how you would have > max steps ... ok actually you can, if we redo at the second to last step
            {
                if (verbose)
                    WriteLine("ALGORITHM FAILED: too many steps");
                break;
            }


            }//check if we should stop, from too many steps, or reaching the goal
            while (true );

        }

        return x;
    }
}
