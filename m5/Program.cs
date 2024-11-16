namespace m5;

internal class Program
{
    /// <summary>
    /// Метод для проверки, содержит ли текст хотя бы два слова.
    /// </summary>
    /// <param name="text">Текст для проверки</param>
    /// <returns>True, если текст корректен</returns>
    private static bool IsValidSentence(string text)
    {
        return text.Split(' ').Length > 1 && !string.IsNullOrWhiteSpace(text);
    }

    /// <summary>
    /// Метод для запроса ввода текста, соответствующего заданным критериям.
    /// </summary>
    /// <param name="message">Сообщение для пользователя</param>
    /// <returns>Корректный ввод текста</returns>
    private static string WaitForValidInput(string message)
    {
        Console.WriteLine(message);
        while (true)
        {
            string input = Console.ReadLine() ?? string.Empty;
            // Проверка: текст должен содержать хотя бы два слова.
            if (IsValidSentence(input))
                return input;

            Console.WriteLine("Ошибка: введите хотя бы два слова!\nДля выхода нажмите Ctrl+C.");
        }
    }

    /// <summary>
    /// Метод для разделения строки на отдельные слова по пробелу
    /// </summary>
    /// <remarks>
    /// В методе не используется встроенный метод Split, так как требовалась более гибкая обработка ввода пользователем неожиданного количества пробелов между словами 
    /// </remarks>
    /// <param name="text">Предложение на вход</param>
    /// <returns>Массив слов (строк)</returns>
    private static string[] SplitText(string text)
    {
        // используем List, так как заранее не знаем, сколько слов в предложении
        List<string> words = new List<string>();
        int currentCharIndex = 0;

        while (currentCharIndex < text.Length)
        {
            // Пропускаем все подряд идущие пробелы
            while (currentCharIndex < text.Length && char.IsWhiteSpace(text[currentCharIndex]))
            {
                currentCharIndex++;
            }

            if (currentCharIndex >= text.Length) break;

            // Если встретили не пробел, запоминаем индекс символа, с которого начинается слово
            int startWordCharIndex = currentCharIndex;
            while (currentCharIndex < text.Length && !char.IsWhiteSpace(text[currentCharIndex]))
            {
                currentCharIndex++;
            }
            // как только встречаем пробел, выходим из цикла и
            // добавляем слово в результирующий массив
            string word = text[startWordCharIndex..currentCharIndex];
            words.Add(word);
        }

        return words.ToArray();
    }

    /// <summary>
    /// Метод для построчного вывода слов из массива 
    /// </summary>
    /// <param name="textArray">Массив слов (строк)</param>
    private static void PrintArrayText(string[] textArray)
    {
        Console.WriteLine("Построчный вывод слов: ");
        foreach (string word in textArray)
        {
            Console.WriteLine(word);
        }
    }

    /// <summary>
    /// Метод для вывода предложения в обратной последовательности 
    /// </summary>
    /// <remarks>Если на ввод дана строка с количеством слов меньше 2, то метод вернет эту строку без изменений</remarks>
    /// <param name="inputPhrase">Предложение для разворота</param>
    /// <returns>Предложение в обратной последовательности</returns>
    private static string ReverseWords(string inputPhrase)
    {
        string[] words = SplitText(inputPhrase);
        if (words.Length <= 1) return inputPhrase;
        Array.Reverse(words);
        return string.Join(" ", words);
    }

    /// <summary>
    /// Метод для получения ответа от пользователя в формате да/нет 
    /// </summary>
    /// <remarks>Метод принимает только ответ на английском языке, но допускает "н" вместо "y", для обработки случаев, когда пользователь не переключил раскладку. Полноценный ответ на русском языке не допускается!</remarks>
    /// <param name="message">Сообщение для пользователя</param>
    /// <returns>True, если пользователь ввел y (yes)</returns>
    private static bool Confirm(string message)
    {
        Console.WriteLine(message + " (y/n)");
        string inputKey = Console.ReadLine() ?? "n";
        if (inputKey.ToLower() is "y" or "yes" or "н") return true;
        return false;
    }

    public static void Main()
    {
        string text = WaitForValidInput("Введите предложение из нескольких слов: ");
        string[] words = SplitText(text);
        PrintArrayText(words);
        bool textReverseUserAgreement = Confirm("Развернем предложение?");
        if (textReverseUserAgreement)
        {
            string reversedText = ReverseWords(text);
            Console.WriteLine(reversedText);
        }
    }
}