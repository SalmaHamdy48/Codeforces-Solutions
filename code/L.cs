
using System;
using System.Drawing;

class Program
{
    static void Main()
    {
       string str1 = Console.ReadLine();
       string str2 = Console.ReadLine();

        string[] fatherOfstr1 = str1.Split(' ',StringSplitOptions.RemoveEmptyEntries);
        string[] fatherOfstr2 = str2.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (fatherOfstr1[1].Equals(fatherOfstr2[1]))
        {
            Console.WriteLine("ARE Brothers");
        }
        else
        {
            Console.WriteLine("NOT");
        }



    }
}