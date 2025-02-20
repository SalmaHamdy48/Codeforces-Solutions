
using System;
using System.Drawing;

class Program
{
    static void Main()
    {
        string[] str = Console.ReadLine().Split();
        int num1 = int.Parse(str[0]);
        int num2 = int.Parse(str[1]);

        if (num1 >= num2)
        {
            Console.WriteLine("Yes");
        }
        else
        {
            Console.WriteLine("No");
        }
             
    }
}