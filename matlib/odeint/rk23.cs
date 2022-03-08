using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

public partial class ode{

public static vector rk23(
	Func<double,vector,vector> F, /* equation */
	double a, vector ya, /* initial condition: {a,y(a)} */
	double b, 
	double h=0.1, 
	double acc=1e-3,
	double eps=1e-3,
	List<double> xlist=null,
	List<vector> ylist=null,
	int limit=999)
{// Generic ODE driver
int nsteps=0;
vector[] trial;
if(xlist!=null) {xlist.Clear(); xlist.Add(a);}
if(ylist!=null) {ylist.Clear(); ylist.Add(ya);}
do{
	if(a>=b) return ya;
	if(nsteps>limit) {
		Error.Write($"ode.driver: nsteps>{limit}\n");
		return ya;
		}
	if(a+h>b) h=b-a;
	trial=rkstep23(F,a,ya,h);
	vector   yh=trial[0];
	vector   er=trial[1];
	vector tol = new vector(er.size);
	for(int i=0; i<tol.size; i++){
		tol[i]=Max(acc,Abs(yh[i])*eps)*Sqrt(h/(b-a));
		if(er[i]==0)er[i]=tol[i]/4;
		}
	double factor=tol[0]/Abs(er[0]);
	for(int i=1; i<tol.size; i++)
		factor=Min(factor,Abs(tol[i]/er[i]));
	double hnew=Min(h*Pow(factor,0.25)*0.95, 2*h);
	int ok=1;
	for(int i=0;i<tol.size;i++)if(Abs(er[i])>tol[i])ok=0;
	if(ok==1){
		a+=h; ya=yh; h=hnew; nsteps++;
		if(xlist!=null) xlist.Add(a);
		if(ylist!=null) ylist.Add(ya);
		}
	else {
		Error.WriteLine($"bad step: x={a}");
		h=hnew;
		}
	}while(true);
}// driver
}// class
