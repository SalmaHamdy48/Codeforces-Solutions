
using System;

class Program
{
    static void Main()
    {

        long num = long.Parse(Console.ReadLine());
        long sum = num * (num + 1) / 2;
        Console.WriteLine(sum);
        /*long sum = 0;
        for (int i = 1; i <= num; i++)
        {
            sum += i;
            
        }
        Console.WriteLine(sum);*/

    }
}