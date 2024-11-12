
namespace m2
{
  internal class Program
  {
    public record Points(Dictionary<string, decimal> SubjectPoints)
    {
      public decimal GetAverage()
      {
        return SubjectPoints.Values.Average();
      }
      public void AddSubject(string subject, decimal score)
      {
        SubjectPoints[subject] = score;
      }
    }

    static void GetInfoAndPoints()
    {
      string fullName = "Иванов Иван Иванович";
      byte age = 20;
      string email = "ivanovivan@ivanovich.ru";
      var pointsBase = new Dictionary<string, decimal>
            {
                { "Информатика", 76.5m },
                { "Математика", 55.9m },
                { "Физика", 65.7m },
                { "Русский язык", 75.2m },
                { "Английский язык", 80.2m }
            };
      Points points = new Points(pointsBase);
      decimal average = points.GetAverage();

      string infoOutput = $"ФИО: {fullName}\n" +
                          $"Возраст: {age}\n" +
                          $"Email: {email}\n";

      string pointsOutput = $"Средний балл: {average}\n" +
                            "Оценки:\n" +
                            string.Join("\n", points.SubjectPoints.Select(s => $"\t{s.Key}: {s.Value}"));

      Console.WriteLine(infoOutput);
      Console.ReadKey(true);
      Console.WriteLine(pointsOutput);
      Console.ReadLine();
    }

    static void Main(string[] args)
    {
      GetInfoAndPoints();
    }
  }
}