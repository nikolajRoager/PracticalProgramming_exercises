using System;
using static System.Console;
using static System.Math;
class main{

public static double erf(double z){
	Func<double,double> f = delegate(double x){return Exp(-x*x);};
	double result = integrate.quad(f:f,a:0,b:z,acc:1e-6,eps:0);
	return result*2/Sqrt(PI);
}

public static void Main(){
	int ncalls=0;
	Func<double,double> f = delegate(double x){
		ncalls++;
		return Log(x)/Sqrt(x);
		};
	double result = integrate.quad(f,a:0,b:1,acc:1e-6,eps:0);
	WriteLine($"result={result}, ncalls={ncalls}");
	for(double x=-3;x<=3;x+=1.0/8)
		Error.WriteLine($"{x} {erf(x)}");
}

}
