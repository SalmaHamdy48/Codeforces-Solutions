using System;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();
        double number = double.Parse(input);

        if (number == (int)number)
        {
            Console.WriteLine("int " + (int)number);
        }
        else
        {
            int integerPart = (int)number;
            double decimalPart = number - integerPart;

        
            Console.WriteLine("float {0} {1:F3}", integerPart, decimalPart);
        }
    }
}
