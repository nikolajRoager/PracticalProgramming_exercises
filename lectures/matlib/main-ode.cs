using System;
using System.Collections.Generic;
using static System.Console;
using static System.Math;
class main{

public static void Main(){

	double b=0.25, c=5;

	Func<double,vector,vector> F = delegate(double t,vector y){
		double theta=y[0], omega=y[1];
		return new vector(omega,-b*omega-c*Sin(theta));
	};

	double start=0;
	double stop=10;
	vector ystart = new vector(PI-0.1,0.1);
	var xlist = new List<double>();
	var ylist = new List<vector>();
	vector ystop= ode.ivp(F,start,ystart,stop,xlist,ylist);

	for(int i=0;i<xlist.Count;i++)
		WriteLine($"{xlist[i]} {ylist[i][0]} {ylist[i][1]}");
}

}
