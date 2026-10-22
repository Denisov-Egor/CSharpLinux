using System;

class Player
{
  public int age = 15;
  private int money = 100;
  protected int healt = 100;
  internal string name = "Pidor";
  protected internal int level = 5;
  private protected int armor = 10;
}

class Program
{
  static void Main()
  {
    Player player = new Player();

    Console.WriteLine(player.age);
    Console.WriteLine(player.name);
    Console.WriteLine(player.level);
    
  }
}