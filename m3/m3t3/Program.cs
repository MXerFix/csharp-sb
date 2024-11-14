namespace m3t3;

internal class Program
{
    private static bool IsSimpleNumber(ulong number)
    {
        bool result = true;
        ulong div = 2;
        while (div <= (ulong)Math.Sqrt(number))
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
        if (!ulong.TryParse(userInputNumber, out ulong number) || number <= 1)
        {
            throw new Exception("Введено некорректное число!");
        }

        Console.WriteLine(
            IsSimpleNumber(number)
                ? "Число является простым."
                : "Число не является простым."
        );
    }
}