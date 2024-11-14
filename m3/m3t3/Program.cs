namespace m3t3;

internal class Program
{
    private static bool IsSimpleNumber(long number)
    {
        bool result = true;
        long div = 2;
        while (div <= (long)Math.Sqrt(number))
        {
            if (number % div == 0)
            {
                result = false;
                break;
            }

            div++;
        }

        return result;
    }

    public static void Main()
    {
        Console.WriteLine("Программа выполняет проверку на простое число.");
        Console.WriteLine("Введите число для проверки: ");
        string? userInputNumber = Console.ReadLine();
        if (!long.TryParse(userInputNumber, out long number))
        {
            throw new Exception("Число введено некорректно!");
        }

        Console.WriteLine(
            IsSimpleNumber(number)
                ? "Число является простым."
                : "Число не является простым."
        );
    }
}