class Parant
{
  private int number;

  public int Number
  {
    get {return number;}
    protected set {number = value;}
  }
  
  public void ChangeNumber (int value)
  {
    Number = value;
  }
}

class Chlild : Parant
{
  public void Change(int value)
  {
    Number = value;
  }
}

class Program
{
  static void Main()
  {
    Parant parant = new Parant();

    parant.ChangeNumber(10);
    
    Console.WriteLine(parant.Number);

    Chlild chlild = new Chlild();

    chlild.Change(20);

    Console.WriteLine(chlild.Number);
  }
}