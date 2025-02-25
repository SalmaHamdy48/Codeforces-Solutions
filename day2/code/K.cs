
using System;
using System.Drawing;

class Program
{
    static void Main()
    {
        string[] str = Console.ReadLine().Split();
        int num1 = int.Parse(str[0]);
        int num2 = int.Parse(str[1]);
        int num3 = int.Parse(str[2]);

        int minValue = Math.Min(num1, Math.Min(num2, num3));
        int maxValue = Math.Max(num1, Math.Max(num2, num3));

        Console.WriteLine($"{minValue} {maxValue}");
    }
}