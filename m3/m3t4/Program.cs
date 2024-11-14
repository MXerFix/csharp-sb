namespace m3t4;

internal class Program
{
    private static int GetIntFromConsole()
    {
        string? input = Console.ReadLine();
        if (!int.TryParse(input, out int result))
        {
            throw new Exception("Невалидный ввод числа!");
        }

        return result;
    }

    private static int GetMinFromChain(int count)
    {
        int min = int.MaxValue;
        for (int i = 0; i < count; i++)
        {
            Console.Write($"Введите {i + 1}-e число: ");
            int current = GetIntFromConsole();
            if (current < min)
            {
                min = current;
            }
        }

        return min;
    }

    public static void Main()
    {
        Console.Write("Введите длину последовательности: ");
        int count = GetIntFromConsole();
        if (count < 1)
        {
            throw new Exception("Длина последовательности не может быть меньше 1!");
        }

        int min = GetMinFromChain(count);
        Console.WriteLine($"Минимальное число: {min}");
    }
}