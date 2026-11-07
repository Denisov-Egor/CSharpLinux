using System;

public class Animal
{
  public virtual void MakeSound()
  {
    Console.WriteLine("Животное издает звук");
  }
}
public class Dog : Animal
{
  public override void MakeSound()
  {
    Console.WriteLine("ГАВ");
  }
}

public class Cat : Animal
{
  public override void MakeSound()
  {
    Console.WriteLine("МЯУ");
  }
}


class Program
{
  static void Main()
  {
    Animal myDog = new Dog();
    Animal myCat = new Cat();

    myDog.MakeSound();
    myCat.MakeSound();
  }
}