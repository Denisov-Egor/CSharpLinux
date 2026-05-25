/*
Написать программу в которой создается экземпляр делегата который вызывается без аргумнтов а результатом возвращения текстовое значение с название недели
"Понедельник" "Вторник"  и так до "Воскресения" При каждом новом вызову экземпляра результатом возвращается название следющего дня недели
после "Вщскресения" результатом возвращается "Понедельник" и так далее
*/

class Program
{
  delegate string DayOfWeekDelegate();  static int currentDay = 0;

  static string GetNextDay()
  {
    string[] daysOfWeek = {"Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье"};
    string result = daysOfWeek[currentDay];
    currentDay = (currentDay + 1) % daysOfWeek.Length;

    return result;
  }

  static void Main()
  {
    DayOfWeekDelegate dayOfWeekDelegate = new DayOfWeekDelegate(GetNextDay);

     for (int i = 0; i < 10; i++) 
      {
        Console.WriteLine(dayOfWeekDelegate());
      }
  }
}