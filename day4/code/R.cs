
using System;
using System.Drawing;

class Program
{
    static void Main()
    {
        long n = long.Parse(Console.ReadLine());

        long years = n / 365;
        n %= 365;

        long month = n / 30;
        n %= 30;

        long days = n;

        Console.WriteLine(years + " years");
        Console.WriteLine(month + " months");
        Console.WriteLine(days + " days");





    }
}