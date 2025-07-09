
using System;
using System.Drawing;

class Program
{
    static void Main()

    {
        string[] input = Console.ReadLine().Split();

        long a = long.Parse(input[0]);
        long b = long.Parse(input[1]);
        long c = long.Parse(input[2]);
        long d = long.Parse(input[3]);


        long res = 1;

        res = (res * a) % 100;
        res = (res * b) % 100;
        res = (res * c) % 100;
        res = (res * d) % 100;



        Console.WriteLine(res.ToString("D2"));


    }





}
