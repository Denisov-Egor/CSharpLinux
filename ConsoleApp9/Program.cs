// создать класс с двумя числовыми полями и методы для вычеслениея суммы, разность, произведения и честного. 
// создать массив из трех объектов этого класса
// обратиться к методоам класс через эл массива

public class  NumberOperations
{
  public int N1;
  public int N2;

  public NumberOperations (int n1, int n2)
  {
    N1 = n1;
    N2 = n2;
  }

  public int Sum()
  {
    return N1 + N2;
  }

  public int Difference()
  {
    if (N1 > N2)
      return N1 - N2;
    else
      return N2 - N1;
  }

  public int Product()
  {
    return N1 * N2;
  }
}

class Program
{

   static void Main(string[] args)
  {
    NumberOperations[] numbers = new NumberOperations[3];
    numbers[0] = new NumberOperations(5, 3);
    numbers[1] = new NumberOperations(7, 2);
    numbers[2] = new NumberOperations(-4, 6);
    foreach (var n in numbers)
    {
        Console.WriteLine($"Сумма: {n.Sum()}, Разность: {n.Difference()}, Произведение: {n.Product()}");
    }
  }
}