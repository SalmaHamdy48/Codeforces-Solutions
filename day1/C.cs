class Program
{
    static void Main()
    {
        string[] inputs = Console.ReadLine().Split();
        
        long X = long.Parse(inputs[0]);
        long Y = long.Parse(inputs[1]);

        Console.WriteLine($"{X} + {Y} = {X + Y}");
        Console.WriteLine($"{X} * {Y} = {X * Y}");
        Console.WriteLine($"{X} - {Y} = {X - Y}");
    }
}