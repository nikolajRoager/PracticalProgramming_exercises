using static System.Console;

public static class main
{//I personally always drop down the brackets to the next line... it just makes it easier for me to spot them


    static int Main(){

		WriteLine($"Demonstrating vectors");

        vec v=new vec(0,1,0);
        vec u=new vec(1,0,0);

        (u).print($"(from u.print) u=");
        WriteLine("Print with no argument");
        (u).print();
        WriteLine("vec.print(u)");
        vec.print(u);
        (v).print($"u=");
        (v+u).print($"u+v=");
        (v-u).print($"u-v=");

        return 0;

    }
}
