namespace m6;

internal class Program
{
    /// <summary>
    /// Выводит горизонтальную линию-разделитель в консоль.
    /// </summary>
    /// <remarks>
    /// Линия состоит из символов "-" и используется для визуального разделения блоков текста в консоли.
    /// </remarks>
    private static void ConsoleDivider()
    {
        Console.WriteLine("-----------------------");
    }

    /// <summary>
    /// Класс конфигурации для валидации входных данных.
    /// </summary>
    /// <typeparam name="T">Тип данных, который ожидается получить.</typeparam>
    private class InputValidationConfig<T>
    {
        /// <summary>
        /// Минимальная допустимая длина вводимой строки.
        /// </summary>
        public int MinLength { get; init; } = 1;

        /// <summary>
        /// Максимальная допустимая длина вводимой строки.
        /// </summary>
        public int MaxLength { get; init; } = 32;

        /// <summary>
        /// Разрешает ли наличие пробелов во вводимых данных.
        /// </summary>
        public bool AllowWhitespace { get; init; }

        /// <summary>
        /// Делегат-функция для конвертации строки в тип T.
        /// </summary>
        public Func<string, T>? Converter { get; set; }
    }

    /// <summary>
    /// Проверяет, соответствует ли введённая строка критериям валидации.
    /// </summary>
    /// <param name="input">Строка ввода для проверки.</param>
    /// <param name="config">Конфигурация валидации.</param>
    /// <returns>True, если строка соответствует критериям, иначе false.</returns>
    private static bool IsValidInput<T>(string? input, InputValidationConfig<T> config)
    {
        if (input == null) return false;

        bool lengthValid = input.Length >= config.MinLength && input.Length <= config.MaxLength;
        bool whitespaceValid = config.AllowWhitespace || !string.IsNullOrWhiteSpace(input);

        return lengthValid && whitespaceValid;
    }

    /// <summary>
    /// Считывает и возвращает корректную строку ввода, соответствующую конфигурации.
    /// </summary>
    /// <param name="config">Конфигурация валидации.</param>
    /// <returns>Объект типа T, полученный из корректного ввода.</returns>
    private static T ReadValidLine<T>(InputValidationConfig<T> config)
    {
        if (typeof(T) == typeof(string) && config.Converter == null)
        {
            config.Converter = input => (T)(object)input;
        }
        else if (typeof(T) == typeof(int) && config.Converter == null)
        {
            config.Converter = input => (T)(object)int.Parse(input);
        }

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (!IsValidInput(input, config))
            {
                Console.WriteLine("Пожалуйста, введите корректные данные!");
                continue;
            }

            try
            {
                return config.Converter!(input!);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка преобразования: {ex.Message}. Попробуйте снова.");
            }
        }
    }

    
    private static int FindMaxFieldId(string[] fileData)
    {
        int maxId = 0;
        foreach (var field in fileData)
        {
            string[] columns = field.Split('#');
            int currId = int.Parse(columns[0]);
            if (currId > maxId) maxId = currId;
        }

        return maxId;
    }

    /// <summary>
    /// Форматирует массив строк, объединяя элементы через символ '#'.
    /// </summary>
    /// <param name="data">Массив строк для форматирования.</param>
    /// <returns>Отформатированная строка.</returns>
    private static string FormatData(string[] data)
    {
        return string.Join('#', data);
    }

    /// <summary>
    /// Получает данные для каждого поля из консоли.
    /// </summary>
    /// <param name="fields">Список названий полей, для которых необходимо ввести данные.</param>
    /// <returns>Массив введённых пользователем данных.</returns>
    private static string[] GetFieldsDataFromConsole(string[] fields)
    {
        var config = new InputValidationConfig<string>();
        List<string> data = new List<string>();
        foreach (var field in fields)
        {
            Console.WriteLine($"Введите данные - {field}:");
            string input = ReadValidLine(config);
            data.Add(input);
        }

        return data.ToArray();
    }

    /// <summary>
    /// Записывает данные в файл.
    /// </summary>
    /// <param name="fileName">Имя файла, куда будут записаны данные.</param>
    /// <param name="userFieldsData">Данные пользователя для записи.</param>
    private static void WriteDataToFile(string fileName, string[] userFieldsData)
    {
        StreamWriter file = new(fileName, true);
        string id = "0";
        string date = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
        if (File.Exists(fileName))
        {
            string[] fileData = File.ReadAllLines(fileName);
            id = Convert.ToString(FindMaxFieldId(fileData));
        }

        string[] data = [id, date, ..userFieldsData];
        string formatedData = FormatData(data);

        file.WriteLine(formatedData);
        file.Close();

        Console.WriteLine("Запись успешно добавлена!");
    }

    /// <summary>
    /// Получает данные от пользователя и добавляет новую строку с данными в файл
    /// </summary>
    /// <param name="fileName">Имя файла, куда будут записаны данные.</param>
    /// <param name="fields">Поля для записи.</param>
    private static void AddNewEntryToFile(string fileName, string[] fields)
    {
        string[] data = GetFieldsDataFromConsole(fields);
        WriteDataToFile(fileName, data);
    }

    /// <summary>
    /// Отображает данные в консоли.
    /// </summary>
    /// <param name="data">Массив строк с данными для отображения.</param>
    /// <param name="fieldsToShow">Массив названий полей, которые нужно показать.</param>
    private static void ShowDataInConsole(string[] data, string[] fieldsToShow)
    {
        foreach (var line in data)
        {
            ConsoleDivider();
            string[] columns = line.Split("#");
            if (columns.Length != fieldsToShow.Length)
            {
                Console.WriteLine("Ошибка формата данных для вывода!");
                continue;
            }

            for (int i = 0; i < fieldsToShow.Length; i++)
            {
                Console.WriteLine($"{fieldsToShow[i]}: {columns[i]}");
            }
        }
    }

    /// <summary>
    /// Извлекает данные из файла и отображает их в консоли.
    /// </summary>
    /// <param name="fileName">Имя файла, откуда извлекаются данные.</param>
    /// <param name="fieldsToShow">Массив названий полей, которые нужно показать.</param>
    /// <exception cref="Exception">Вернет ошибку, если файла с таким именем не существует</exception>
    private static void ExtractDataFromFile(string fileName, string[] fieldsToShow)
    {
        if (!File.Exists(fileName))
        {
            throw new Exception("Файла не существует!");
        }

        string[] data = File.ReadAllLines(fileName);
        ShowDataInConsole(data, fieldsToShow);

        ConsoleDivider();
    }

    /// <summary>
    /// Обрабатывает главное меню программы.
    /// </summary>
    /// <param name="actions">Словарь действий, где ключ - название пункта меню, а значение - действие.</param>
    private static void HandleMainMenu(Dictionary<string, Action> actions)
    {
        var config = new InputValidationConfig<int>
        {
            MinLength = 1, MaxLength = 1, AllowWhitespace = false,
            Converter = (input) => int.TryParse(input, out var result) ? result : -1
        };
        for (int i = 0; i < actions.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {actions.ElementAt(i).Key}");
        }

        int input = ReadValidLine(config);
        actions.ElementAt(input - 1).Value();
    }


    public static void Main(string[] args)
    {
        string fileName = "data.txt";
        string[] systemFields = ["ID", "Дата добавления"];
        string[] filledFields = ["ФИО", "Возраст", "Рост", "Дата рождения", "Место рождения"];
        Dictionary<string, Action> menuActions = new Dictionary<string, Action>
        {
            { "Заполнить данные и добавить новую запись", () => AddNewEntryToFile(fileName, filledFields) },
            { "Вывести данные на экран", () => ExtractDataFromFile(fileName, [..systemFields, ..filledFields]) },
            { "Выйти из программы", () => Environment.Exit(0) }
        };
        while (true)
        {
            HandleMainMenu(menuActions);
            Console.WriteLine("Возврат в меню...");
            ConsoleDivider();
        }
    }
}