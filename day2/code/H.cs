
using System;
using System.Drawing;

class Program
{
    static void Main()
    {
        string[] str = Console.ReadLine().Split();
        int num1 = int.Parse(str[0]);
        int num2 = int.Parse(str[1]);

        double d = (double)num1 / num2;

        Console.WriteLine($"floor {num1} / {num2} = {Math.Floor(d)}");
        Console.WriteLine($"ceil {num1} / {num2} = {Math.Ceiling(d)}");
        Console.WriteLine($"round {num1} / {num2} = {Math.Floor(d+0.5)}");
    }
}