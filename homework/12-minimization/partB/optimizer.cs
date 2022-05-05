using System;
using static System.Console;
using static System.Math;
using static matrix;
using static vector;

public static class optimizer
{
    //Mirror function to maximize instead
    public static (vector,int) max_qnewton(Func<vector,double>f, vector x0, double acc=1e-2, bool verbose=false)
    {
        Func<vector,double> F = (X)  => -f(X);
        return  qnewton(F,x0,acc,verbose);
    }


    public static (vector,int) qnewton(Func<vector,double>f, vector x0, double acc=1e-2, bool verbose=false)
    {


        double little = Pow(2,-26);
        double alpha = 1e-4;//Used in acceptance condition for backtracking
        double epsilon = 1e-9;//Used to see if we should reset hessian

        vector x = x0.copy();//Better copy this, if the user wants to use the original input vector for something else.
        //Read the number of parameters the function outputs, and the number of parameters in the input

        int n = x.size;

        bool redo=true;//I have an emergency redo function, in case deltax is too small, in some rare cases, I have seen deltax be treated as a 0, breaking the algorithm uppon division, if that happen, we will retry here, with
        int max_steps=1000;//should never need that much

        int step = -1;//Start at -1 so we really start at 0
        while (redo)
        {
            redo=false;//In 99% of cases, this will only need to be done once, but maybe we are on a computer where machine epsilon is smaller than expected.


            step = -1;
            double fx=f(x);
            matrix B = new matrix(n,n);//Inverse hessian, start guess: identity

            //We will save the gradient between steps, as we need the gradient both at the start and at the end
            vector GradFx = new vector(n);



            do
            {//Each step
                ++step;

                //PART 1: Calculate the Gradient
                // ∇ f(x)
                //
                // PART 2: backtracking:
                // Let Delta x = - B ∇ f(x)
                // Let s = lambda Delta x
                // Perform backtracking lambda -> lambda/2 and update s until f(x+s)<f(x)+alpha dot(s,∇ f(x)) (or until lambda too small)
                //
                // Symmetric Rank 1 update:
                //
                // CALCULATE ∇f(x + s) again !!?
                //
                // Let y = ∇f(x + s)-∇f(x) and u = s- By
                //
                // if dot(u,y)>epsilon
                //     let B = outer_product(u,u)/dot(u,y)
                // else
                //     let B = 1
                // endif


                //If this is the first step, we need to get gradient here matrix
                if  (step==0)
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
                    if (verbose)
                    {
                        WriteLine($"Step {step}");
                        WriteLine(x.getString("x="));
                        WriteLine("");
                        WriteLine(GradFx.getString("Numeric ∇ f(x)="));
                        WriteLine("");
                    }

                    //Now need to do backtracking

                    // Let Delta x = - B ∇ f(x)
                    vector Dx = -B*GradFx;


                    // Let s = lambda Delta x
                    double lambda = 1;
                    vector S = Dx;
                    vector x_new = x+S;
                    for (int i = 0; i < 32; ++i)
                    {
                        // Perform backtracking lambda -> lambda/2 and update s until f(x+s)<f(x)+alpha dot(s,∇ f(x)) (or until lambda too small)
                        if (f(x_new) < fx+ alpha*S.dot(GradFx)  )
                            break;
                        lambda*=0.5;
                        S = lambda*Dx;
                        x_new = x+S;
                    }

                    x = x_new;
                    fx = f(x);
                    if (verbose)
                    {
                        WriteLine("");
                        WriteLine(x.getString("This step x -> "));
                        WriteLine($"This step f(x) -> {fx}");
                    }

                    //With enough steps, this will actually work without resetting B. But we can do betterm now entering SR1 update


                    // CALCULATE ∇f(x + s) for the next step

                    vector GradFx_new = new vector(n);


                    for (int i = 0; i < n; ++i)
                    {
                        //Get the input, offset on the relevant element
                        vector offsetx = x.copy();
                        double deltax = Max(Abs(x[i])*little,0.25*little);//The tiny step to use when calculating the Jacobi matrix, note I do not enjoy 0 divisions, and it is possible that the user guesses that x[k]=0, so I add a floor to deltax

                        offsetx[i]+=deltax;

                        double foffsetxk=f(offsetx);
                        offsetx[i]+=deltax;
                        GradFx_new[i] = (foffsetxk-fx)/deltax;


                        if(double.IsInfinity(GradFx_new[i]))//This is very very rare
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



                    // Let y = ∇f(x + s)-∇f(x) and u = s- By
                    vector y = GradFx_new-GradFx;
                    vector u = S-B*y;


                    if (verbose)
                    {
                        WriteLine("");
                        WriteLine(GradFx_new.getString("∇f(x + s)"));
                        WriteLine("HAVE ");
                        WriteLine(y.getString("y="));
                        WriteLine("");
                        WriteLine(u.getString("u="));
                        WriteLine($"SO dot(y,u)={u.dot(y)}");
                    }

                    if (Abs(u.dot(y))>epsilon)
                    {

                        B = B+ matrix.outer_product(u,u)/u.dot(y);
                        if (verbose)
                        {
                            WriteLine("UPDATED B");
                            WriteLine(B.getString("B="));
                        }
                    }
                    else
                    {
                        B = new matrix(n,n);
                        if (verbose)
                        {
                            WriteLine("Reset B to identity");
                            WriteLine(B.getString("B="));
                        }
                    }

                    //Now, we already have whatever the gradient should be next step.
                    GradFx=GradFx_new;

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

        return (x,step);
    }
}
