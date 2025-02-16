
using System;

class Program
{
    static void Main()
    {
        string[] inputs = Console.ReadLine().Trim().Split();
        

        long x = long.Parse(inputs[0]);
        long y = long.Parse(inputs[1]);
        long z = long.Parse(inputs[2]);
        long j = long.Parse(inputs[3]);

        long m = (x * y) - (z * j);
            

            Console.WriteLine("Difference"+" = "+m);
  

    }
}