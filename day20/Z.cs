using System;

class Program
{
    static void Main()
    {
        string[] input = Console.ReadLine().Split();

        double A = double.Parse(input[0]);
        double B = double.Parse(input[1]);
        double C = double.Parse(input[2]);
        double D = double.Parse(input[3]);

        double left = B * Math.Log(A);
        double right = D * Math.Log(C);

        if (left > right)
            Console.WriteLine("YES");
        else
            Console.WriteLine("NO");
    }
}
