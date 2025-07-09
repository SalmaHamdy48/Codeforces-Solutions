using System;
using System.Drawing;

class Program
{
    static void Main()

    {
        string[] input = Console.ReadLine().Split();

        int l1 = int.Parse(input[0]);
        int r1 = int.Parse(input[1]);
        int l2 = int.Parse(input[2]);
        int r2 = int.Parse(input[3]);


        int start = Math.Max(l1, l2);
        int end = Math.Min(r1, r2);

        if (start <= end)
        {
            Console.WriteLine($"{start} {end}");
        }

        else
        {
            Console.WriteLine("-1");
        }


    }





}