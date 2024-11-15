namespace m3t5;

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

    private static void GuessNumber(int range)
    {
        int randomNumber = Random.Shared.Next(range + 1);
        int counter = 0;
        while (true)
        {
            Console.Write("Твой вариант: ");
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int userNum))
            {
                if (input != "") throw new Exception("Невалидный ввод числа!");
                Console.WriteLine($"Эх, неудача, но ты был близок. Загаданное число: {randomNumber}");
                break;
            }

            counter++;
            if (userNum != randomNumber)
            {
                Console.Write("Не угадал. ");
                Console.WriteLine(
                    userNum > randomNumber
                        ? "Твое число больше загаданного."
                        : "Твое число меньше загаданного."
                );
                continue;
            }

            Console.WriteLine(counter == 1
                ? "Да ты крут! Угадал с первого раза!"
                : $"Поздравляю! Ты угадал с {counter}-й попытки!");
            break;
        }
    }

    public static void Main()
    {
        Console.WriteLine("Привет! Ты в игре 'Угадай число'.");
        Console.Write("В каком диапазоне будешь отгадывать? От 0 до ");
        int range = GetIntFromConsole();
        GuessNumber(range);
    }
}