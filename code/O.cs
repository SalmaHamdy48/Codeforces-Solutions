
using System;
using System.Drawing;

class Program
{
    static void Main()
    {

        string s = Console.ReadLine();
        string[] str = s.Split(new char[] { '+', '-', '*', '/' });

      int n1 = int.Parse(str[0].Trim());
    
      int n2 = int.Parse(str[1].Trim());

        if(s.Contains("+"))
        {
            Console.WriteLine(n1 + n2);
        }
        else if (s.Contains("-"))
        {
            Console.WriteLine(n1 - n2);
        }
        else if (s.Contains("*"))
        {
            Console.WriteLine(n1 * n2);
        }
        else
        {
            Console.WriteLine(n1 / n2);
        }







    }
}