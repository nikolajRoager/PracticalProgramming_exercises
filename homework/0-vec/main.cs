using System;//Random
using static System.Console;//Writelines
using static System.Math;//Trigonometry

public static class main
{//I personally always drop down the brackets to the next line... it just makes it easier for me to spot them


    static int Main()
    {

		WriteLine("Testing the vector class");


        int return_code=0;
        bool success;//Flag to tell us the test worked

        var rnd=new Random();
        int n=64;
        vec[] V = new vec[n];
        for(int i=0;i<n;i++)
        {
            //I want the vectors to have uniformly random direction and length
            double Theta = 2*PI*rnd.NextDouble();
            double Phi = Acos(2*rnd.NextDouble()-1.0);
            double R = rnd.NextDouble()*10;
            double CT=Cos(Theta);
            double CP=Cos(Phi);
            double ST=Sin(Theta);
            double SP=Sin(Phi);

            V[i]=new vec(CT*CP,ST*CP,SP);
        }
        WriteLine("Testing various printing functions");




        (V[0]).print($"(from v.print) v=");
        WriteLine("Print with no argument");
        (V[0]).print();
        WriteLine("vec.print(v)");
        vec.print(V[0]);

        WriteLine("using vec.ToString() implicitly "+V[0]);

        Write("Testing --v=v");

        success=true;
        for(int i=0;i<n;i++)
        {

            success=success&& V[i].approx(-(-V[i]));
        }
        if(success)
            WriteLine(" ...passed\n");
        else
        {
            WriteLine(" ...FAILED\n");
            return_code += 1;
        }

        Write("Testing v+u=u+v");

        success=true;
        for(int i=0;i<n;i++)
            for(int j=i;j<n;j++)//Sure lets test all pairs, why not
                success=success&& (V[i]+V[j]).approx((V[j]+V[i]));
        if(success)
            WriteLine(" ...passed\n");
        else
        {
            WriteLine(" ...FAILED\n");
            return_code += 1;
        }

        Write("Testing c*v=v*c");

        success=true;
        for(int i=0;i<n;i++)
            for(int j=0;j<n;j++)
                //use the list of components as an impromptu list of scalars
            {
                success=success&& (V[i]*V[j].x).approx((V[j].x*V[i]));
                success=success&& (V[i]*V[j].y).approx((V[j].y*V[i]));
                success=success&& (V[i]*V[j].z).approx((V[j].z*V[i]));
            }

        if(success)
            WriteLine(" ...passed\n");
        else
        {
            WriteLine(" ...FAILED\n");
            return_code += 1;
        }

        Write("Testing c*v/c=v");
        success=true;
        for(int i=0;i<n;i++)
            for(int j=0;j<n;j++)
                //Use the list of components as an impromptu list of scalars
            {
                success=success&& (V[j].x*V[i]/V[j].x).approx(V[i]);
                success=success&& (V[j].y*V[i]/V[j].y).approx(V[i]);
                success=success&& (V[j].z*V[i]/V[j].z).approx(V[i]);
            }
        if(success)
            WriteLine(" ...passed\n");
        else
        {
            WriteLine(" ...FAILED\n");
            return_code += 1;
        }

        Write("Testing dot(v,u)=v.dot(u)");
        success=true;
        for(int i=0;i<n;i++)
            for(int j=i;j<n;j++)
                success=success&& vec.approx((V[j].dot(V[i])),vec.dot(V[i],V[j]));
        if(success)
            WriteLine(" ...passed\n");
        else
        {
            WriteLine(" ...FAILED\n");
            return_code += 1;
        }

        Write("Testing dot(v,u+w)=dot(v,u)+dot(v,w)");
        success=true;
        for(int i=0;i<n;i++)
            for(int j=0;j<n;j++)
                for(int k=j;k<n;k++)
                    success=success&& vec.approx((V[i].dot(V[j]+V[k])),V[i].dot(V[j])+V[i].dot(V[k]));
        if(success)
            WriteLine(" ...passed\n");
        else
        {
            WriteLine(" ...FAILED\n");
            return_code += 1;
        }


        Write("Testing dot(-v,u)=dot(-v,u)=-dot(v,u)");
        success=true;
        for(int i=0;i<n;i++)
            for(int j=i;j<n;j++)
            {
                double res = -(V[i].dot(V[j]));
                success=success&& vec.approx(res,(-V[i]).dot(V[j]));
                success=success&& vec.approx(res,(V[i]).dot(-V[j]));
            }
        if(success)
            WriteLine(" ...passed\n");
        else
        {
            WriteLine(" ...FAILED\n");
            return_code += 1;
        }


        Write("Testing cross(v,u)=-cross(u,v)");
        success=true;
        for(int i=0;i<n;i++)
            for(int j=i;j<n;j++)
            {
                success=success&& V[i].cross(V[j]).approx(-V[j].cross(V[i]));
            }
        if(success)
            WriteLine(" ...passed\n");
        else
        {
            WriteLine(" ...FAILED\n");
            return_code += 1;
        }


        if(return_code==0)
            Write("all tests passed\n");
        else
            Write($"{return_code} tests FAILED\n");

        return 0;//I want to be able to run this from inside the makefile, and if it returns anything but 1 make will quit, I don't want this so I always return 0.

    }
}
