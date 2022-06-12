using System;
using static System.Console;
using static System.Math;
using static matrix;
using static vector;

public static class rootfinder
{
    public static (vector,int) newton(Func<vector,vector>f, vector x0, double eps=1e-2, bool verbose=false)
    {


        double little = Pow(2,-26);


        vector x = x0.copy();//Better copy this, if the user wants to use the original input vector for something else.
        //Read the number of parameters the function outputs, and the number of parameters in the input



        int n = f(x).size;
        int m = x.size;

        //This approach uses Gram Schmidt QR factorization and IDIOTICALLY requires the J matrix to be tall, now I am obviously an idiot for using such an algorithm
        if (n<m)
        {
            throw new ArgumentException($"this STUPID algorithm uses QR factorization, which requires the Jacobi matrices in question to do be tall, i.e. we do require more function dimensions than input dimensions");
        }



        bool redo=true;//I have an emergency redo function, in case deltax is too small, in some rare cases, I have seen deltax be treated as a 0, breaking the algorithm uppon division, if that happen, we will retry here, with
        int recovery=0;//How many times have we had to do an emergency recovery? if we do this too often we abort
        int max_steps=10000;//should never need that much
        int step = -1;

        while (redo)
        {
            redo=false;//In 99% of cases, this will only need to be done once, but maybe we are on a computer where machine epsilon is smaller than expected.

            step = -1;

            vector fx=f(x);
            do
            {//Each step
                ++step;
                //Get Jacobi matrix
                matrix J = new matrix(n,m);


                for (int k = 0; k < m; ++k)
                {
                    //Get the input, offset on the relevant element
                    vector offsetx = x.copy();
                    double deltax = Max(Abs(x[k])*little,0.25*little);//The tiny step to use when calculating the Jacobi matrix, note I do not enjoy 0 divisions, and it is possible that the user guesses that x[k]=0, so I add a floor to deltax

                    offsetx[k]+=deltax;

                    vector foffsetx=f(offsetx);
                    offsetx[k]+=deltax;
                    vector fk = (foffsetx-fx)/deltax;

                    for (int i = 0; i < n; ++i)
                    {

                        J[i,k]=fk[i];


                        if(double.IsInfinity(fk[i]))//This is very very rare
                        {
                            //CRAP, deltax was too small, and we got a 0 division, we must retry with a larger little number... unless we have done that too many times already

                            if (little>0.25)
                                Error.WriteLine("ABORT: Got 0 division in Newton methods too many times");

                            Error.WriteLine("WARNING: Got 0 division in Newton method, this may be caused by your computer having less precision for double point numbers than expected. Will retry with a larger Delta x");
                            redo = true;
                            x = resize(x0.copy(),n);//Reset our guess, and try again with a larger little number
                            little*=2;


                            break;
                        }
                    }

                    if (redo)
                        break;//If out rare division error happened, just abbandon and try again
                }

                if (!redo)//If finding the Jacobi did NOT fail (this failure I check for is VERY RARE)
                {
                    vector b = -fx;


                    matrix Q,R;
                    (Q,R) = J.getQR();
                    vector Dx = QRsolve(Q,R,b);

                    if (verbose)
                    {
                        WriteLine($"Step {step}");
                        WriteLine(x.getString("x="));
                        WriteLine("");
                        WriteLine(f(x).getString("f(x)="));
                        WriteLine("");
                        WriteLine(J.getString("J = "));
                        WriteLine("");
                        WriteLine(b.getString("-f(x) = "));
                        WriteLine("");
                        WriteLine(Q.getString("Got Q = "));
                        WriteLine("");
                        WriteLine(R.getString("Got R = "));
                        WriteLine("");
                        WriteLine((Q*R).getString("Verify QR = "));
                        WriteLine("");
                        WriteLine(Dx.getString("Dx = "));
                        WriteLine("");
                        WriteLine((J*Dx).getString("Check J*Dx = "));
                    }

                    //OH OH, the QR decomposition failed, in my experience this might be because the starting guess was bad (for instance, if the gradient is 0
                    if (Q.isEvil() || R.isEvil())
                    {
                        if (recovery>10)
                            throw new ArgumentException($"ABORT: QR decomposition of the Jacobi matrix failed more than 10 times in a row, interrupting.");
                        Error.WriteLine($"WARNING: QR decomposition of the Jacobi matrix failed for some reason, this may be caused by a bad starting guess (maybe a gradient is 0), is trying a new random startin guess ({recovery}/10)");
                        ++recovery;
                        x = resize(x0.copy(),n);//Reset, but try a random guess somewhere around x0
                        var Rand = new vector(x.size);
                        Rand.randomize();
                        x = x+0.3*Rand;
                        fx = f(x);

                        continue;
                    }
                    //Backtracking line search, minimum lambda 1/32 as in the chapter
                    double lambda = 1;
                    vector x_new = x+lambda*Dx;
                    double fxNorm = fx.norm();
                    for (int i = 0; i < 32; ++i)
                    {
                        if (f(x_new ).norm()<(1-lambda*0.5)*fxNorm  )
                            break;
                        lambda*=0.5;
                        x_new = x+lambda*Dx;
                    }

                    x = x_new;
                    fx = f(x);
                    if (verbose)
                    {
                        WriteLine("");
                        WriteLine(x.getString("This step x -> "));
                        WriteLine(fx.getString("This step f(x) -> "));
                    }

                    //check if the step we just took was too small in all parameters
                    bool toosmall=true;
                    for(int i = 0; i < Dx.size; ++i)
                    {

                        double deltax = Abs(x[i])*little;//The tiny step to use when calculating the Jacobi matrix, note I do not enjoy 0 divisions, and it is possible that the user guesses that x[k]=0, so I add a floor to deltax

                        if (Abs(Dx[i])>deltax)//If any is appreciable
                            toosmall = false;
                    }
                    if (toosmall)
                    {
                        if (verbose)
                            WriteLine("ALGORITHM FAILED: too small steps");
                        break;
                    }

                }


            if (fx.norm()<eps)
            {
                if (verbose)
                    WriteLine("BREAK DUE TO: Criterion sattisfied");
                break;
            }

            if (step>=max_steps)//I am not sure how you would have > max steps ... ok actually you can, if the second to last step triggered a re-roll due to NaN or infinite QR decomposition
            {
                if (verbose)
                    WriteLine("ALGORITHM FAILED: too many steps");
                break;
            }


            }//check if we should stop, from too many steps, or reaching the goal
            while (fx.norm()>eps && step<max_steps );

        }

        return (x,step);
    }
}
