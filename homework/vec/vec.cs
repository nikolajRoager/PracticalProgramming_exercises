using static System.Console;//for Writelines
using static System.Math;//for Abs

public class vec
{
    public double x=0;
    public double y=0;
    public double z=0;

    //I was supposed to make a default and a regular constructor, but using default parameters saves me some typing
    public vec(double _x=0,double _y=0,double _z=0)
    {
        x=_x;
        y=_y;
        z=_z;
    }

    //The exercise wanted me to define both print(s) and print(), I do both at the same time with default values
    public void print(string s="")
    {
        WriteLine(s+$"[{x}, {y}, {z}]");
    }

    public static void print(vec v)
    {
        v.print();
    }

    //I define these explicitly, rather than refering to other, i.e. I do not define v-u as v+(-1)*v, that would work too but it would be inefficient, and I think it is just as readable
    public static vec operator*(vec u, double c)
    {
        return new vec(c*u.x,c*u.y,c*u.z);
    }

    public static vec operator*(double c,vec u)
    {
        return new vec(c*u.x,c*u.y,c*u.z);
    }


    //Not a part of the exercise, but I think I need this
    public static vec operator/(vec u, double c)
    {
        return new vec(u.x/c,u.y/c,u.z/c);
    }
    //No reverse version of this, as dividing scalars by vectors is not defined

    public static vec operator+(vec u, vec v)
    {
        return new vec(u.x+v.x,u.y+v.y,u.z+v.z);
    }
    public static vec operator+(vec u)
    {
        return u;
    }

    //In the lecture, you showed that we could use the previously defined functions to make these definitions shorter, but I do not want to do that, as it would be moderately worse for performance, and I do not think this looks any less readable
    public static vec operator-(vec u, vec v)
    {
        return new vec(u.x-v.x,u.y-v.y,u.z-v.z);
    }


    public static vec operator-(vec u)
    {
        return new vec(-u.x,-u.y,-u.z);
    }

    public static double dot (vec v, vec u)
    {
        return v.x*u.x+v.y*u.y+v.z*u.z;
    }
    public double dot (vec u)
    {
        return x*u.x+y*u.y+z*u.z;//The keyword this form the example is not required so I prefer to skip it
    }

    //Whoops, turns out this was not part of the exercise
    public static vec cross (vec v, vec u)
    {
        return new vec(v.y*u.z-v.z*u.y,v.z*u.x-v.x*u.z,v.x*u.y-v.y*u.x);
    }
    public vec cross (vec u)
    {
        return new vec(y*u.z-z*u.y,z*u.x-x*u.z,x*u.y-y*u.x);
    }

    //Override, since this already exists by default
    public override string ToString()
    {
        return $"[{x},{y},{z}]";
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

    public bool approx(vec other)
    {//Only true if all components are about the same
        return approx(x,other.x) && approx(y,other.y) && approx(z,other.z);
    }

    public static bool approx(vec u, vec v)
    {
        return !approx(u.x,v.x) && !approx(u.y,v.y) && !approx(u.z,v.z);
    }



}
