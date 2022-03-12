using System;
using System.Collections.Generic;
using static System.Console;
using static System.Math;

class main
{
    public static void Main()
    {

//Python example
/*
def pend(y, t, b, c):

    theta, omega = y

    dydt = [omega, -b*omega - c*np.sin(theta)]

    return dydt
*/
        //t and the list need to be in different order, also b and c can just be capture automatically (c++ would have them in [...]()
        double b=0.25;
        double c=5;
        Func<double,vector,vector> pend = delegate(double t,vector y){
            double theta=y[0], omega=y[1];
            return new vector(omega,-b*omega-c*Sin(theta));
        };


//Python example t = np.linspace(0, 10, 101)
        double start=0;
        double stop=10;
//Here we use adaptive step size

//Python example: y0 = [np.pi - 0.1, 0.0]
        vector ystart = new vector(PI-0.1,0.0);

        //Here we need different notation to call odeint
        var xlist = new List<double>();
        var ylist = new List<vector>();
        vector ystop= ode.ivp(pend,start,ystart,stop,xlist,ylist);

        //Dump to a tap seperated table
        for(int i=0 ; i < xlist.Count; i++)
            WriteLine($"{xlist[i]}\t{ylist[i][0]}\t{ylist[i][1]}");
    }

}
