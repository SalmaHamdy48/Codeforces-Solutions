
using System;
using System.Drawing;

class Program
{
    static void Main()
    {
       char ch = char.Parse(Console.ReadLine());
       char lower = char.ToLower(ch);
       char upper = char.ToUpper(ch);  

        if (char.IsUpper(ch))
        {
            Console.WriteLine(lower);
        }
        else if (char.IsLower(ch))
        {
            Console.WriteLine(upper);
        }
       
      

        



    }
}