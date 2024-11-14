namespace m3t1;

internal class Program
{
    private static bool GetIsNumEven(int num)
    {
        return num % 2 == 0;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Программа проверяет число на предмет четности.");
        Console.Write("Введите число: ");
        if (!int.TryParse(Console.ReadLine(), out int num))
        {
            throw new Exception("Введено не число");
        }

        bool isEven = GetIsNumEven(num);
        Console.Write(isEven ? "Четное" : "Нечетное");
        Console.WriteLine(" ({0})", num);
    }
}