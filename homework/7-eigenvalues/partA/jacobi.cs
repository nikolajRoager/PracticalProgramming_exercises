using static matrix;
using System;
using static System.Math;

public static class jacobi
{
    //Faster jacobi matrix multiplication, A*J or J*A
    public static void timesJ(matrix A, int p, int q, double theta)
    {
        double c=Cos(theta);
        double s=Sin(theta);
        for(int i=0;i<A.height;i++)
        {
            double a_ip=A[i,p];
            double a_iq=A[i,q];
            A[i,p]=c*a_ip-s*a_iq;
            A[i,q]=s*a_ip+c*a_iq;
		}
    }
    public static void Jtimes(matrix A, int p, int q, double theta)
    {


        double c=Cos(theta);
        double s=Sin(theta);
        for(int i=0;i<A.width;i++)
        {

            double a_pi=A[p,i];
            double a_qi=A[q,i];
            A[p,i]= c*a_pi+s*a_qi;
            A[q,i]=-s*a_pi+c*a_qi;
		}
    }

    //Versions where the Sines and CoSines are pre-computed, this is useful if we don't actually want to bother with trigonometry (I have tested that it is not THAT slow, but surely more operations introduce more rounding errors)
    public static void timesJ(matrix A, int p, int q, double c, double s)
    {
        for(int i=0;i<A.height;i++)
        {
            double a_ip=A[i,p];
            double a_iq=A[i,q];
            A[i,p]=c*a_ip-s*a_iq;
            A[i,q]=s*a_ip+c*a_iq;
		}
    }
    public static void Jtimes(matrix A, int p, int q, double c, double s)
    {


        for(int i=0;i<A.width;i++)
        {

            double a_pi=A[p,i];
            double a_qi=A[q,i];
            A[p,i]= c*a_pi+s*a_qi;
            A[q,i]=-s*a_pi+c*a_qi;
		}
    }


    //Various version of getting eigenvalues and eigenvectors

    //Same notation as in numpy, the D matrix has diagonal elemetns, which are the eigenvalues
    //V has each column being an eigenvector
    //NOTE, this is only works for A being symmetric matrix, as specified in the exercise
    public static (matrix D, matrix V) getDV(matrix constA , double tau = 1e-9, double eps = 1e-9)
    {
        matrix V =new  matrix(constA.height,constA.width);
        matrix A = constA.copy();//This method is destructive to the matrix, take a copy of the original matrix so we don't break it
        //This is much the way you suggested we do this
        bool changed=false;
        int iteration = 0;
        do
        {
            changed=false;

            //Cycle all the elements
            for(int p=0;p<A.height-1;p++)
            {
                for(int q=p+1;q<A.height;q++)
                {

                    //Get the original entries, remember this matrix is symmetric already
                    double a_pq=A[p,q];
                    double a_pp=A[p,p];
                    double a_qq=A[q,q];

                    //Get the angle to do zero-set the pq element
                    double theta=0.5*Atan2(2*a_pq,a_qq-a_pp);
                    double c=Cos(theta),s=Sin(theta);


                    //If we were to apply this rotation, what would happen to the diagonal elements, if nothing would happen, don't aooly
                    double new_a_pp=c*c*a_pp-2*s*c*a_pq+s*s*a_qq;
                    double new_a_qq=s*s*a_pp+2*s*c*a_pq+c*c*a_qq;

                    if(!  matrix.approx(new_a_pp,a_pp,tau ,eps)|| !  matrix.approx(new_a_qq,a_qq,tau ,eps)  )
                    {
                        changed=true;
                        timesJ(A,p,q, theta);//theta
                        Jtimes(A,p,q, -theta);//-theta, i.e. this is do being sandwhich


                        timesJ(V,p,q, theta);
                    }
                }
            }
            ++iteration;
        }
        while(changed);




        return (A,V);
    }


    //Same, but don't calculate V
    public static matrix getD(matrix  constA, double tau = 1e-9, double eps = 1e-9)
    {
        matrix A = constA.copy();//This method is destructive to the matrix, take a copy of the original matrix so we don't break it
        //This is much the way you suggested we do this
        bool changed=false;
        int iteration = 0;
        do
        {
            changed=false;

            //Cycle all the elements
            for(int p=0;p<A.height-1;p++)
            {
                for(int q=p+1;q<A.height;q++)
                {

                    //Get the original entries, remember this matrix is symmetric already
                    double a_pq=A[p,q];
                    double a_pp=A[p,p];
                    double a_qq=A[q,q];

                    //Get the angle to do zero-set the pq element
                    double theta=0.5*Atan2(2*a_pq,a_qq-a_pp);
                    double c=Cos(theta),s=Sin(theta);


                    //If we were to apply this rotation, what would happen to the diagonal elements, if nothing would happen, don't aooly
                    double new_a_pp=c*c*a_pp-2*s*c*a_pq+s*s*a_qq;
                    double new_a_qq=s*s*a_pp+2*s*c*a_pq+c*c*a_qq;

                    if(!  matrix.approx(new_a_pp,a_pp,tau ,eps)|| !  matrix.approx(new_a_qq,a_qq,tau ,eps)  )
                    {
                        changed=true;
                        timesJ(A,p,q, theta);//theta
                        Jtimes(A,p,q, -theta);//-theta, i.e. this is do being sandwhich


                        //timesJ(V,p,q, theta); //theta
                    }
                }
            }
            ++iteration;
        }
        while(changed);




        return A;
    }

}

