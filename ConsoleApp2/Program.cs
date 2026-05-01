using System;
using System.Collections.Generic;

class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double AverageGrade { get; set; }

    public Student(string name, int age, double averageGrade)
    {
        Name = name;
        Age = age;
        AverageGrade = averageGrade;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Имя: {Name}, Возраст: {Age}, Средний балл: {AverageGrade:F2}");
    }

    public bool IsExcellent()
    {
        return AverageGrade >= 4.5;
    }
}

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>
        {
            new Student("Анна", 19, 4.8),
            new Student("Иван", 20, 4.2),
            new Student("Мария", 18, 4.9),
            new Student("Петр", 21, 3.9)
        };

        Console.WriteLine("Отличники:");
        foreach (Student s in students)
        {
            if (s.IsExcellent())
            {
                s.PrintInfo();
            }
        }
    }
}