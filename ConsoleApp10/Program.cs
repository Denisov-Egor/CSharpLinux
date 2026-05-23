// разрабоать класс << калькулятор логарифмов >> с возможностью сложение, вычитания, умножения, вызвыдения в степень и перехода к другому основанию.
// программа должна выполнять ввод данных, проверку введение данных, выдачу сообщений в случае ошибок
// протокол работы калькулятора записать в файле предусмотреть возможномть просмотра файла из программы

public class Logariphmic
{
 public double Number, Osn;

 public Logariphmic(double number, double osn)
 {
    this.Number = number;
    this.Osn = osn;
  }

  public double Log()
  {
    return Math.Log(this.Number, this.Osn);
  }

  public double LogSolution(Logariphmic first, Logariphmic secon)
  {
    if (first.Osn == secon.Osn)
    {
      return Math.Log(first.Number + secon.Number, first.Osn);
    }else
    {
      Console.WriteLine("Нельзя складывать логарифм с разными осноаниями");
    }
    return 0;
  }

  public double LogMultiplay(Logariphmic first, Logariphmic secon)
  {
    if (first.Osn == secon.Osn)
    {
      return Math.Log(first.Number + secon.Number, first.Osn);
    }else
    {
      Console.WriteLine("Нельзя складывать логарифм с разными осноаниями");
    }
    return 0;
  }

  public double LogSubstraction(Logariphmic first, Logariphmic secon)
  {
    if (first.Osn == secon.Osn)
    {
      return Math.Log(first.Number + secon.Number, first.Osn);
    }else
    {
      Console.WriteLine("Нельзя складывать логарифм с разными осноаниями");
    }
    return 0;
  }

  public double LogDevite(Logariphmic first, Logariphmic secon)
  {
    if (first.Osn == secon.Osn)
    {
      return Math.Log(first.Number + secon.Number, first.Osn);
    }else
    {
      Console.WriteLine("Нельзя складывать логарифм с разными осноаниями");
    }
    return 0;
  }
}