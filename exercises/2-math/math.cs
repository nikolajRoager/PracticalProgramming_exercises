using static System.Math;


//I made this before the lecture, so it is not the same as the example given in class, still jus as the lecture I try two class, to see how the scopes work

public static class math_vars
{
    public static double exp1 = Exp(1);
}

public static class main
{//I personally always drop down the brackets to the next line... it just makes it easier for me to spot them


    static int Main(){

        double sqrt2 = Sqrt(2);
        //I would like to test also doing the calculations in-line, i.e. do not save them beforehand
		System.Console.Write($"Demonstrating some math\nsqrt(2) = {sqrt2}\n"+$"exp(pi) = {Exp(PI)}\n"+$"pi^e = {Pow(PI,math_vars.exp1 )}\n");

        return 0;

    }
}
