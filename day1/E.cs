
using System;

class Program
{
    static void Main()
    {
        
        

        double x = double.Parse(Console.ReadLine());
        double area = 3.141592653 * x * x; ;


        Console.WriteLine($"{area:F9}");


    }
}