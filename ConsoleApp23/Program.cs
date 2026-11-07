using System;

enum InsuranceType
{
  OMS,
  DMS
}

class Patient
{
  public string Name { get; set; }
  public InsuranceType Insurance { get; set; }

  public Patient(string name, InsuranceType insurance)
  {
    Name = name;
    Insurance = insurance;
  }

  public void ShowInfo()
  {
    Console.WriteLine("\nИнформация о пациенте: ");
    Console.WriteLine($"ФИО: {Name}");
    Console.WriteLine($"Страховка: {Insurance}");
  }

  public void GetServices()
  {
    Console.WriteLine("\nДоступные медицинские услуги:");
    Console.WriteLine("- Консультация врача");
    Console.WriteLine("- Диагностика");

    if (Insurance == InsuranceType.DMS)
    {
      Console.WriteLine("- Стоматология");
      Console.WriteLine("- Массаж");
    }
  }
}

class Program
{
  static void Main()
  {
    Console.Write("Введите ФИО пациента: ");
    string name = Console.ReadLine() ?? "";

    Console.WriteLine("\nВыберите страховку:");
    Console.WriteLine("1. ОМС");
    Console.WriteLine("2. ДМС");
    Console.Write("Ваш выбор: ");
    
    string choice = Console.ReadLine() ?? "";
    InsuranceType insurance;

    if (choice == "1")
    {
      insurance = InsuranceType.OMS;
    }else if (choice == "2")
    {
      insurance = InsuranceType.DMS;
    }else
    {
      Console.WriteLine("Неверный выбор!");
      return;
    }

    Patient patient = new Patient(name, insurance);
    
    patient.ShowInfo();
    patient.GetServices();
  }
}
