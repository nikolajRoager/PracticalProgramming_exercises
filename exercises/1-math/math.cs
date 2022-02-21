using static System.Math;



public static class main
{//I personally always drop down the brackets to the next line... it just makes it easier for me to spot them


    static int Main(){

        double sqrt2 = Sqrt(2);
        double exp1 = Exp(1);
		System.Console.Write($"Demonstrating some math\nsqrt(2) = {sqrt2}\n"+$"exp(pi) = {Exp(PI)}\n"+$"pi^e = {Pow(PI,exp1 )}\n");

		System.Console.Write($"sqrt(2)^2 = {sqrt2*sqrt2} should be 2\n");

        return 0;

    }
}
