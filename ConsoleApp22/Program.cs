using System;

interface IWorker
{
  void Work();
}

abstract class Person
{
   public string Name { get; private set; }
   public Person(string name)
   {
      Name = name;
   }
   protected void SetName(string newName)
   {
      Name = newName;
   } 
   public abstract void SayHello();
}

class Student : Person, IWorker
{
   public Student(string name) : base(name)
   {
   }
   public override void SayHello()
   {
      Console.WriteLine("Привет, я студент!");
   }
   public void Work()
   {
      Console.WriteLine("Я учусь программировать.");
   }
   public void ChangeName(string newName)
   {
      SetName(newName);
   }
}

class Program
{
   static void Main()
   {
      Student student = new Student("Егор");
      Console.WriteLine("Старое имя: " + student.Name);
      student.ChangeName("Илья");
      Console.WriteLine("Новое имя: " + student.Name);
   }
}