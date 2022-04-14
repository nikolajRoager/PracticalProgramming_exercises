using static System.Math;
using static System.Exception;

public class doublelist
{
    private double[] data;
    private int n;
    private int capacity;

    public doublelist(int N=0)
    {
        data = new double[Max(N,1)];
        n=N;
        capacity=Max(N,1);
    }

    //I like to specify where we push, i.e. push_back (maybe I am just biased in favor of c++ std::doublelisttor), but the exercise asked me for this name
    public void push(double item)
    {

        //Check if we need to increase size
        if (n==capacity)
        {
            capacity*=2;
            double[] newdata = new double[capacity];

            for (int i = 0; i<n; ++i)
                newdata[i]=data[i];
            data=newdata;//Move by reference, old data will be picked up by the garbage
        }

        data[n]=item;


        ++n;//Always increment the size
    }

    public double get(int i)
    {
        if (i>0)
            return data[i];//if i>n we get an error, so no reason to throw anything manually
        else
            return data[n-1-i];//I like the python way of accessing the back of the array, lets copy that
    }


    //Ok, the exercise wanted a signed integer ... I want to use an unsigned integer, but C# is much worse at implicit conversions than c++, so ok, signed it is
	public int size {get{return n;}} // property, ok this is kind of cool that this looks like a variable without being one


    public doublelist copy()
    {
        doublelist Out = new doublelist(n);

        for (int i = 0; i < n; ++i)
            Out[i]=this[i];

        return Out;
    }

    //I define these explicitly, rather than refering to other, i.e. I do not define v-u as v+(-1)*v, that would work too but it would be inefficient, and I think it is just as readable
    public static doublelist operator*(doublelist u, double c)
    {
        doublelist Out = new doublelist(u.size);

        for (int i = 0; i < u.size; ++i)
            Out[i]=c*u[i];
        return Out;
    }

    public static doublelist operator*(double c,doublelist u)
    {
        doublelist Out = new doublelist(u.size);

        for (int i = 0; i < u.size; ++i)
            Out[i]=c*u[i];
        return Out;
    }


    public static doublelist operator/(doublelist u, double c)
    {
        doublelist Out = new doublelist(u.size);
        double invc = 1.0/c;
        for (int i = 0; i < u.size; ++i)
            Out[i]=u[i]*invc;
        return Out;
    }


    public static doublelist operator+(doublelist u, doublelist v)
    {
        if(u.size!=v.size) throw new System.Exception("Addition of double lists with different size");

        doublelist Out = new doublelist(u.size);

        for (int i = 0; i < u.size; ++i)
            Out[i]=u[i]+v[i];
        return Out;
    }
    public static doublelist operator+(doublelist u)
    {
        return u;
    }

    //In the lecture, you showed that we could use the previously defined functions to make these definitions shorter, but I do not want to do that, as it would be moderately worse for performance, and I do not think this looks any less readable
    public static doublelist operator-(doublelist u, doublelist v)
    {
        if(u.size!=v.size) throw new System.Exception("Subtraction of double lists with different size");

        doublelist Out = new doublelist(u.size);

        for (int i = 0; i < u.size; ++i)
            Out[i]=u[i]-v[i];
        return Out;
    }


    public static doublelist operator-(doublelist u)
    {
        doublelist Out = new doublelist(u.size);

        for (int i = 0; i < u.size; ++i)
            Out[i]=-u[i];
        return Out;
    }

    public static double dot (doublelist v, doublelist u)
    {
        if(u.size!=v.size) throw new System.Exception("dot product of double lists with different size");

        double Out = 0;

        for (int i = 0; i < u.size; ++i)
            Out+=u[i]*v[i];
        return Out;
    }
    public double dot (doublelist u)
    {
        if(u.size!=n) throw new System.Exception("dot product of double lists with different size");

        double Out = 0;

        for (int i = 0; i < u.size; ++i)
            Out+=u[i]*this[i];
        return Out;
    }

    public double norm ()
    {

        double Out = 0;

        for (int i = 0; i < n; ++i)
            Out+=this[i]*this[i];
        return Sqrt(Out);
    }
    //Override, since this already exists by default
    public override string ToString()
    {
        string Out = "";

        for (int i = 0; i < n; ++i)
            Out+=$"{this[i]}\t";
        return Out;
    }

    //Create get and set functions
	public double this[int i]
    {


		get {
            if (i>=0)
                return data[i];//if i>n we get an error, so no reason to throw anything manually
            else
                return data[n-1-i];//I like the python way of accessing the back of the array, lets copy that
        }
		set {
            if (i>=0)
                data[i]=value;//if i>n we get an error, so no reason to throw anything manually
            else
                data[n-1-i]=value;//I like the python way of accessing the back of the array, lets copy that
        }
	}

    //A little annoying that the correct functions were placed on the webite, I would have liked to figure out how to do this myself.
    //double precision version version
    public static bool approx(double a,double b,double tau=1e-9,double eps=1e-9)
    {
        if (Abs(a-b)<tau)
            return true;
        if (Abs(a-b)/(Abs(a)+Abs(b))<eps)
            return true;
        return false;
    }

    public bool approx(doublelist u)
    {//Only true if all components are about the same


        if(u.size!=n) return false;

        double Out = 0;

        for (int i = 0; i < u.size; ++i)
            if (!approx(this[i],u[i]))
                return false;

        return true;
    }


    public doublelist abs()
    {
        doublelist Out = new doublelist(n);
        for (int i = 0; i < n; ++i)
            Out[i]=Abs(this[i]);
        return Out;
    }

    public static bool approx(doublelist u, doublelist v)
    {
        return u.approx(v);
    }



}
