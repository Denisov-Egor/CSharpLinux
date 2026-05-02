class  Person
{
  public string Name;
  public int Age;

  public Person(string name, int age)
  {
    Name = name;
    Age = age;
  }

  public void DisplayInfo()
  {
    Console.WriteLine($"Имя: {Name}, Возраст: {Age}");
  }
}

class Program
{
  static void Main()
  {
    List<Person> people = new List<Person>
    {
      new Person("dsds", 19),
      new Person("dsdadads", 20),
      new Person("dsdsdd", 12),
    };

    foreach (Person p in people)
    {
      p.DisplayInfo();
    }
  }
}