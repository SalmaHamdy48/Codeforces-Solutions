
using System;

class Program
{
    static void Main()
    {
        string[] inputs = Console.ReadLine().Trim().Split();
        

        long x = long.Parse(inputs[0]);
        long y = long.Parse(inputs[1]);

        long z = x + y;
            long m = x * y;
            long s = x - y;

            Console.WriteLine(x + " + " + y + " = " + z);
            Console.WriteLine(x + " * " + y + " = " + m);
            Console.WriteLine(x + " - " + y + " = " + s);

        

        
        


    }
}