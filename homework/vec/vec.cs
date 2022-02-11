using static System.Console;

public class vec
{
    public double x=0;
    public double y=0;
    public double z=0;

    public vec()
    {
        x=0;
        y=0;
        z=0;
    }
    public vec(double _x,double _y,double _z)
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

    public static vec operator+(vec u, vec v)
    {
        return new vec(u.x+v.x,u.y+v.y,u.z+v.z);
    }

    public static vec operator-(vec u, vec v)
    {
        return new vec(u.x-v.x,u.y-v.y,u.z-v.z);
    }

    public static vec operator+(vec u)
    {
        return u;
    }

    public static vec operator-(vec u)
    {
        return (-1)*u;
    }
}
