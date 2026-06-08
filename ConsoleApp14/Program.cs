abstract class Figure
{
  public abstract double CalculateArea();
  public abstract double CalculatePerimeter();

  public void DisplayInfo()
  {
    Console.WriteLine($"Площадь: {CalculateArea()}");
    Console.WriteLine($"Периметр: {CalculatePerimeter()}");
  }
}

class Rectangle : Figure
{
  public double Length;
  public double Width;
  public Rectangle(double length, double width)
  {
    Length = length;
    Width = width;
  }
  public override double CalculateArea()
  {
    return Length * Width;
  }
  public override double CalculatePerimeter()
  {
    return 2 * (Length + Width);
  }
}

class Circle : Figure
{
  public double Radius;
  public Circle(double radius)
  {
    Radius = radius;
  }
  public override double CalculateArea()
  {
    return Math.PI * Radius * Radius;
  }
  public override double CalculatePerimeter()
  {
    return 2 * Math.PI * Radius;
  }
}

class Triangle : Figure
{
  public double Side1;
  public double Side2;
  public double Side3;
  public Triangle(double side1, double side2, double side3)
  {
    Side1 = side1;
    Side2 = side2;
    Side3 = side3;
  }
  public override double CalculateArea()
  {
    double s = (Side1 + Side2 + Side3) / 2;
    return Math.Sqrt(s * (s - Side1) * (s - Side2) * (s - Side3));
  }
  public override double CalculatePerimeter()
  {
    return Side1 + Side2 + Side3;
  }
}

class Program
{
  static void Main()
  {
    Figure[] figures = new Figure[5];
    figures[0] = new Rectangle(4, 5);
    figures[1] = new Circle(3);
    figures[2] = new Triangle(3, 4, 5);
    figures[3] = new Rectangle(6, 7);
    figures[4] = new Circle(2.5);
    
    foreach (Figure figure in figures)
    {
      if (figure is Rectangle rect)
      {
        Console.WriteLine($"Прямоугольник с длиной {rect.Length} и шириной {rect.Width}:");
      }
      else if (figure is Circle circ)
      {
        Console.WriteLine($"Круг c радиусом {circ.Radius}:");
      }
      else if (figure is Triangle tria)
      {
        Console.WriteLine($"Треугольник со сторонами {tria.Side1}, {tria.Side2}, и {tria.Side3}:");
      }
      figure.DisplayInfo();
      Console.WriteLine("--------------------");
    }
  }
}