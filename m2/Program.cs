
namespace m2
{
  internal class Program
  {
    public record Points(Dictionary<string, decimal> SubjectPoints)
    {
      public decimal GetAverage()
      {
        return Math.Round(SubjectPoints.Values.Average(), 3);
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
            };
      Points points = new Points(pointsBase);
      points.AddSubject("Русский язык", 75.2m);
      points.AddSubject("Английский язык", 80.3m);
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