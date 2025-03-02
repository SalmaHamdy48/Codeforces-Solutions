
using System;
using System.Drawing;

class Program
{
    static void Main()
    {
        long x = long.Parse(Console.ReadLine());

        while (x >= 10)
        {
            x /= 10;
        }
        if (x % 2 == 0)
        {
            Console.WriteLine("EVEN");
        }


        else
        {
            Console.WriteLine("ODD");
        }






    }
}