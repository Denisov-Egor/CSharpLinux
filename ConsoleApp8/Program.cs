class User
{
  public int ID {get; set;}
  public string Name {get; set;}
  public int Age {get; set;}

  public User (int id, string name, int age)
  {
    if (age < 18)
    {
      Console.WriteLine("Возраст не может быть меньше 18 лет.");
      Environment.Exit(1);
    }
    ID = id;
    Name = name;
    Age = age;
  }

  public void DisplayInfo()
  {
    Console.WriteLine($"ID {ID}, Имя {Name}, Возраст {Age}");
  }
}

class Program
{
  static void Main()
  {
    User[] users = new User[3];

    for (int i = 0; i < users.Length; i++)
    {
      Console.WriteLine("Введите ID ");
      int ID = Convert.ToInt32(Console.ReadLine());

      Console.WriteLine("Введите Имя ");
      string Name = Console.ReadLine();
      
      Console.WriteLine("Введите возраст ");
      int Age = Convert.ToInt32(Console.ReadLine());
      
      users[i] = new User(ID, Name, Age);
    }

  List<User> olderUsers = new List<User>();

    Console.WriteLine("Больше 18 лет:");
    foreach (var user in olderUsers)
    {
      user.DisplayInfo();
    }
    double averageAge = 0;
    if (olderUsers.Count > 0)
    {
      averageAge = olderUsers.Average(u => u.Age);
    }
    Console.WriteLine($"Средний возраст пользователей 18: {averageAge}");
  }
}
