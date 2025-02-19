class Program
{
    static void Main()
    {
        string[] inputs = Console.ReadLine().Split();
        
        int x = int.Parse(inputs[0]);
        long y = long.Parse(inputs[1]);
        char z = char.Parse(inputs[2]);
        float g = float.Parse(inputs[3]);
        double h = double.Parse(inputs[4]);

        Console.WriteLine(x);
        Console.WriteLine(y);
        Console.WriteLine(z);
        Console.WriteLine(g);
        Console.WriteLine(h);
    }
}