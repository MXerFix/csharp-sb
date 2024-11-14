namespace m3t2;

internal class Program
{
    private static int GetCardWeight(string card)
    {
        if (!int.TryParse(card, out int weight))
            return card.ToUpper() switch
            {
                "K" or "Q" or "T" or "J" => 10,
                _ => throw new Exception("Неверный формат карты!")
            };
        if (weight is <= 10 and >= 2)
        {
            return weight;
        }

        throw new Exception("Неверный формат карты!");
    }

    public static void Main(string[] args)
    {
        int cardsWeightSum = 0;
        Console.WriteLine("Введите количество карт: ");
        string? userInputCardsCount = Console.ReadLine();
        if (!int.TryParse(userInputCardsCount, out int cardsCount) || cardsCount <= 0 || cardsCount > 52)
        {
            throw new Exception("Неверный формат числа карт!");
        }

        for (int i = 0; i < cardsCount; i++)
        {
            Console.WriteLine($"Введите номинал карты {i + 1}: ");
            string? card = Console.ReadLine();
            if (card is null)
            {
                throw new Exception("Неверный формат карты!");
            }

            int weight = GetCardWeight(card);
            cardsWeightSum += weight;
        }

        Console.WriteLine($"Сумма ваших карт - {cardsWeightSum}");
    }
}