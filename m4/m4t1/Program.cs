namespace m4t1;

internal class Program
{
    /// <summary>
    /// Метод принимает на вход сообщение и запрашивает ввод числа у пользователя
    /// </summary>
    /// <param name="message">Сообщение, отображаемое при запросе ввода</param>
    private static int GetIntInput(string message)
    {
        Console.Write(message);
        string? input = Console.ReadLine();
        if (!int.TryParse(input, out int result))
        {
            throw new Exception("Неверный формат ввода!");
        }

        return result;
    }

    /// <summary>
    /// Метод принимает на вход матрицу и возвращает максимальное значение длины элемента в ней
    /// </summary>
    /// <param name="matrix">Матрица на вход</param>
    /// <returns>Максимальное значение длины элемента</returns>
    private static int GetMaxNumberLength(int[,] matrix)
    {
        int maxLength = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j].ToString().Length > maxLength)
                {
                    maxLength = matrix[i, j].ToString().Length;
                }
            }
        }

        return maxLength;
    }

    /// <summary>
    /// Метод получает матрицу на вход и выводит ее в консоль
    /// </summary>
    /// <param name="matrix">Матрица на вход</param>
    private static void PrintMatrix(int[,] matrix)
    {
        // используем метод для получения длины самого длинного элемента в матрице (масло масляное, извиняюсь) 
        int maxNumberLength = GetMaxNumberLength(matrix);
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // применяем полученное значение для отрисовки адекватного отступа между элементами разной размерности
                Console.Write(matrix[i, j].ToString().PadLeft(maxNumberLength) + " ");
            }

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Метод получает на вход количество строк и столбцов и возвращает двумерный массив (матрицу) с указанным количеством строк и столбцов
    /// </summary>
    /// <param name="rows">Количество строк</param>
    /// <param name="columns">Количество столбцов</param>
    /// <returns>Матрица с `rows` строк и `columns` столбцов</returns>
    private static int[,] GenerateMatrix(int rows, int columns)
    {
        int[,] matrix = new int[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // диапазон для разнообразия
                matrix[i, j] = Random.Shared.Next(-10, 50);
            }
        }

        return matrix;
    }

    /// <summary>
    /// Метод получает две матрицы на вход, складывает их и возвращает результат в виде матрицы (суммы матриц) той же размерности 
    /// </summary>
    /// <param name="matrix">Матрица А для сложения</param>
    /// <param name="addMatrix">Матрица В для сложения</param>
    /// <returns>Сумма матриц</returns>
    private static int[,] AddMatrix(int[,] matrix, int[,] addMatrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);
        int[,] result = new int[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = matrix[i, j] + addMatrix[i, j];
            }
        }

        return result;
    }

    /// <summary>
    /// Метод получает на вход матрицу и возвращает сумму всех ее элементов
    /// </summary>
    /// <param name="matrix">Матрица на вход</param>
    /// <returns>Сумма всех элементов матрицы</returns>
    private static int GetMatrixElementsSum(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                sum += matrix[i, j];
            }
        }

        return sum;
    }

    public static void Main()
    {
        // получаем количество строк и столбцов от пользователя
        int rows = GetIntInput("Введите количество строк: ");
        int columns = GetIntInput("Введите количество столбцов: ");
        
        // генерируем матрицу 1
        int[,] matrixA = GenerateMatrix(rows, columns);
        
        // выводим ее в консоль
        Console.WriteLine("Сгенерированная матрица:");
        PrintMatrix(matrixA);
        
        // считаем и выводим сумму ее элементов
        int elementsSum = GetMatrixElementsSum(matrixA);
        Console.WriteLine($"Сумма элементов матрицы: {elementsSum}");
        
        // предлагаем пользователю проверить метод сложения матриц
        Console.WriteLine("Протестировать сложение матриц? (y/n)");
        string? input = Console.ReadLine();
        
        // проверяем, валидируем ввод
        if (input?.ToLower() is "y" or "н")
        {
            // генерируем матрицу 2 для сложения
            int[,] matrixB = GenerateMatrix(rows, columns);
            
            Console.WriteLine("Сложение матриц:");
            PrintMatrix(matrixA);
            Console.WriteLine("+");
            PrintMatrix(matrixB);
            Console.WriteLine("=");
            
            // складываем матрицы
            int[,] result = AddMatrix(matrixA, matrixB);
            
            // выводим результат
            PrintMatrix(result);
            int resultElementsSum = GetMatrixElementsSum(result);
            Console.WriteLine($"Сумма элементов результирующей матрицы: {resultElementsSum}");
        }
    }
}