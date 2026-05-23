/*
Создать следубщее классы автомобиль Car гараж Garage мойка Washer
гараж это коллекция автомобилей
мойка - назависимое предприятие которое может только мыть автомобил методом мойка
необходимо делегировать помывку всех автомобилей этому предприятию
*/

public class Car
{
  public string Make;
  public string Model;

  public override string ToString()
  {
    return $"{Make} {Model}";
  }
}

public class Garage
{
  public List<Car> cars;
  public Garage()
  {
    cars = new List<Car>();
  }
  public void AddCar(Car car)
  {
    cars.Add(car);
  }
  public void RemoveCar(Car car)
  {
    cars.Remove(car);
  }
  public List<Car> GetCars()
  {
    return cars;
  }
}

public class Washer
{
  private Garage garage;
  public Washer(Garage garage)
  {
    this.garage = garage;
  }
  public void WashAllCars()
  {
    List<Car> cars = garage.GetCars();
    foreach (var car in cars)
    {
      WashCar(car);
    }
  }
  public void WashCar(Car car)
  {
    Console.WriteLine($"Мойка {car}");
  }
}

class Program()
{
  static void Main()
  {
    Garage garage = new Garage();
    Washer washer = new Washer(garage);

    garage.AddCar(new Car { Make = "Toyota", Model = "Corolla" });
    garage.AddCar(new Car { Make = "Ford", Model = "Mustang" });

    washer.WashAllCars();
  }
}