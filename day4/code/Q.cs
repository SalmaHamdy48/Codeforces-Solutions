
using System;
using System.Drawing;

class Program
{
    static void Main()
    {
        string[] str = Console.ReadLine().Split();
        double x = double.Parse(str[0]);
        double y = double.Parse(str[1]);


        if (x > 0 && y > 0)
        {
            Console.WriteLine("Q1");
        }

        else if (x < 0 && y > 0)
        {
            Console.WriteLine("Q2");
        }

        else if (x < 0 && y < 0)
        {
            Console.WriteLine("Q3");
        }

        else if (x > 0 && y < 0)
        {
            Console.WriteLine("Q4");
        }

        else if (x == 0 && y == 0)
        {
            Console.WriteLine("Origem");
        }

        else if (x == 0 && (y > 0 || y < 0))
        {
            Console.WriteLine("Eixo Y");
        }

        else
        {
            Console.WriteLine("Eixo X");
        }




    }
}