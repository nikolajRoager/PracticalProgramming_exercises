using System;
using static System.Math;
//using static System.Console;
using static doublelist;

public static class ode
{
    //Dormand Prince stepper
    public static (doublelist,doublelist) rkstepper54(Func<double,doublelist,doublelist> ODE, double x ,doublelist Data,double h, double xf)
    {

        //Do not overshoot
        if(x+h>xf)
            h =xf-x;

        //substep 1, was  calculated that last substep of last step
        doublelist K1 = ODE(x,Data);

        //substep 2
        doublelist K2 = ODE(x+h/5.0,Data+h*K1/5.0);

        //substep 3
        doublelist K3 = ODE(x+h*3/10.0,Data+h*K1*3/40.0+h*K2*9/40.0);

        //substep 4
        doublelist K4 = ODE(x+h*4/5.0,Data+h*K1*44/45.0-h*K2*56/15.0+h*K3*32/9.0);

        //substep 5
        doublelist K5 = ODE(x+h*8/9.0,Data+h*K1*19372/6561.0-h*K2*25360/2187.0+h*K3*64448/6561.0-h*K4*212/729.0);//   19372/6561  -        25360/2187           64448/6561  -        212/729

        //substep 6
        doublelist K6 = ODE(x+h,Data+h*K1*9017/3168.0-h*K2*355/33.0+h*K3*46732/5247.0+h*K4*49/176.0-h*K5*5103/18656.0);//         9017/3168  -        355/33           46732/5247           49/176  -      5103/18656

        //substep 7, and ALSO the 5'th order estimate
        doublelist Data_5 = Data+h*K1*35/384.0+h*K3*500/1113.0+h*K4*125/192.0-h*K5*2187/6784.0+h*K6*11/84.0;
        doublelist K7 = ODE(x+h,Data_5);//         35/384  0        500/1113           125/192  −        2187/6784           11/84

        doublelist Data_4 = Data+h*((5179/57600.0)*K1+(7571/16695.0)*K3+(393/640.0)*K4-(92097/339200.0)*K5+(187/2100.0)*K6+(1/40.0)*K7);

        //Estimate the error
        doublelist Error = (Data_5-Data_4).abs();//doublelist.abs need to be defined


        return (Data_5,Error);
    }

    //Adaptive stepsize driver, returns a list of data,x at every step
    public static (genlist<doublelist>,doublelist) driver(
        Func<double,doublelist,doublelist> ODE,
        double x0,
        double xf,
        doublelist Data0,
        double h=0.01,
        double absErr= 0.01,
        double relErr = 0.01,
        double h_max = 0
    )
    {
        double h_new=h;
        doublelist Data = Data0.copy();//I much prefer c/c++, where the copy constructor would be called by default

        double x=x0;
        int rejections=0;
        int steps=0;

        genlist<doublelist> datalist = new genlist<doublelist>();
        doublelist xlist = new doublelist();

        datalist.push(Data.copy());
        xlist.push(x0);
        while (x<xf)
        {

            bool reject = false;
            do
            {
                reject = false;
                doublelist Data5;
                doublelist Error;
                (Data5,Error) = rkstepper54(ODE, x ,Data,h, xf);

                double error = Error.norm();//Part A wants to do this the clearly inferior way of looking at the norm
                double delta = Sqrt(h/(xf-x0))*Max(relErr*Abs(Data.norm()),absErr);
                if (error>delta)
                {
                    reject = true;
                }
                if (error!=0)
                {

                    h_new = 0.9*h*Pow(delta/error,0.2);//What step size would fix this p=1/5
                    if (h_new>h_max && h_max !=0)//Don't overstep, if h_max is 0, no limit is used
                    {
                        h_new =h_max;
                    }

                }
                /*
                first_loop = true;
                for (int i = 0; i<3 ; ++i)
                {
                    double delta = Sqrt(h/(xf-x0))*Max(relErr*Abs(Data[i]),absErr);
                    if (Error[i]>delta)
                    {
                        reject = true;
                    }
                    if (Error[i]!=0)
                    {

                        double my_h_new = 0.95*h*Pow(delta/Error[i],0.2);//What step size would fix this p=1/5

                        //If this is the worst parameter so far , resize to fit that
                        if (my_h_new<h_new || first_loop)
                        {
                            h_new =my_h_new;
                            first_loop = false;
                            if (h_new>h_max && h_max !=0)//Don't overstep, if h_max is 0, no limit is used
                            {
                                h_new =h_max;
                            }
                        }
                    }
                }*/
                if (!reject)
                {
                    x+=h;
                    Data=Data5;
                }
                else
                    ++rejections;

                h=h_new;

            }
            while(reject);

            //If we KEPT the previous step, then K7 is the same as K1 for the next step
            //K1=K7; alas, the way you asked us to set this up, I can not implement this without changing the arguments or outputs from the stepper

            ++steps;
            datalist.push(Data.copy());
            xlist.push(Min(x,xf));
        }

        return (datalist,xlist);
    }

}
