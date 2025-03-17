using System;

class Program
{
    static void Main()
    {
        String[] str = Console.ReadLine().Split();
        int A = int.Parse(str[0]);
        int B = int.Parse(str[1]);
        int C = int.Parse(str[2]);

        int[] origin = {A, B, C};

        int[] sort = {A, B, C};
        Array.Sort(sort);

        for (int i = 0; i < sort.Length; i++)
        {
            Console.WriteLine(sort[i]);
        }

        Console.WriteLine();

        for (int i = 0; i < origin.Length; i++)
        {
            Console.WriteLine(origin[i]);
        }

    }
}
