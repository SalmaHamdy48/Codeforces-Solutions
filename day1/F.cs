
using System;

class Program
{
    static void Main()
    {
        string[] inputs = Console.ReadLine().Split();

        long x = long.Parse(inputs[0]);
        long y = long.Parse(inputs[1]);

        long lastX = (x % 10);
        long lastY = (y % 10);


        Console.WriteLine(lastX + lastY);


    }
}