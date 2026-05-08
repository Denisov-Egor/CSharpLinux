public class Car
{
    public int MaxSpeed { get; set; }
    public int CurrentSpeed { get; private set; }

    public void Accelerate(int increment)
    {
        if (CurrentSpeed + increment <= MaxSpeed)
        {
            CurrentSpeed += increment;
            Console.WriteLine($"Скорость увеличена на {increment}. Текущая скорость: {CurrentSpeed}");
        }
        else
        {
            Console.WriteLine("Превышение максимальной скорости!");
            CurrentSpeed = MaxSpeed;
        }
    }

    public void Brake(int decrement)
    {
        if (CurrentSpeed - decrement >= 0)
        {
            CurrentSpeed -= decrement;
            Console.WriteLine($"Скорость уменьшена на {decrement}. Текущая скорость: {CurrentSpeed}");
        }
        else
        {
            Console.WriteLine("Торможение привело к остановке!");
            CurrentSpeed = 0;
        }
    }
}

class Program
{
    static void Main()
    {
        Car myCar = new Car();
        myCar.MaxSpeed = 120;

        Console.WriteLine($"Максимальная скорость автомобиля: {myCar.MaxSpeed}");
        
        myCar.Accelerate(50);
        myCar.Brake(30);
        myCar.Accelerate(40);
        myCar.Brake(70);
    }
}