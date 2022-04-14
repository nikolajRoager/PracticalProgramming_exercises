using System;
using static System.Console;
using static System.Math;
using static montecarlo;

public static class main
{


    //double precision approximation
    public static bool approx(double a,double b,double tau=1e-9,double eps=1e-9)
    {
        if (Abs(a-b)<tau)
            return true;
        if (Abs(a-b)/(Abs(a)+Abs(b))<eps)
            return true;
        return false;
    }

    public static int Main()
    {
        WriteLine($"NOTE, Monte Carlo is random, and give DIFFERENT results each run (different seeds). It will occasionally fail to get within error of te true result, this is to be expected, more often than not it should work");

        Func<doublelist,double> xy = (X)  => X[0]*X[1];
        //First test, integrate x*y from 0,0 to 1,1
        doublelist a=new doublelist(2);
        doublelist b=new doublelist(2);
        a[0]=0;
        a[1]=0;
        b[0]=1;
        b[1]=1;
        double res0;
        double err0;
        (res0,err0) = plain_box(xy,a,b,10000);
        WriteLine($"plain integral of x*y from 0,0 to 1,1 ={res0}±{err0} "+(approx(res0,0.25,err0,err0) ? " PASS" : " FAIL"));



        Func<doublelist,double> FF = (X)  => (X[0]*X[0]+X[1]*X[1] < 0.8*0.8 ? 1.0 : 0.0);
        //First test, integrate x*y from 0,0 to 1,1
        a=new doublelist(2);
        b=new doublelist(2);
        a[0]=0;
        a[1]=0;
        b[0]=1;
        b[1]=1;
        double res2;
        double err2;
        (res2,err2) = plain_box(FF,a,b,10000);
        WriteLine($"integral of f(x,y)=1 if x^2+y^2<0.8, otherwise 0  from 0,0 to 1,1 ={res2}±{err2} ");



        Func<doublelist,double> F = (X)  => 1/((1-Cos(X[0])*Cos(X[1])*Cos(X[2]))*(PI*PI*PI));
        //First test, integrate x*y from 0,0 to 1,1
        a=new doublelist(3);
        b=new doublelist(3);
        a[0]=0;
        a[1]=0;
        a[2]=0;
        b[0]=PI;
        b[1]=PI;
        b[2]=PI;
        double res1;
        double err1;
        (res1,err1) = plain_box(F,a,b,100000);
        WriteLine($"plain integral of (1-Cos(X[0])*Cos(X[1])*Cos(X[2]))/(PI*PI*PI) from 0,0,0 to PI PI PI ={res1}±{err1} "+(approx(res1,1.3932039296856768591842462603255,err1,err1) ? " PASS" : " FAIL"));
        return 0;
    }

}
