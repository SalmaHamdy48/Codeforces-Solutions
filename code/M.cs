using System;
using System.Drawing;
 
class Program
{
    static void Main()
    {
       char ch = char.Parse(Console.ReadLine());
 
        if (char.IsUpper(ch))
        {
            Console.WriteLine("ALPHA");
            Console.WriteLine("IS CAPITAL");
        }
        else if (char.IsLower(ch))
        {
            Console.WriteLine("ALPHA");
            Console.WriteLine("IS SMALL");
        }
        else
        {
            Console.WriteLine("IS DIGIT");
        }
      
 
        
 
 
 
    }
}