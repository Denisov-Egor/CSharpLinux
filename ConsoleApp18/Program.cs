using System;

class Program
{
  static void Main()
  {
    int count = 0;

    int[] arr = new int[16];

    Random rand = new Random();

    for (int i = 0; i < arr.Length; i++)
    {
      arr[i] = rand.Next(0, 2);

      if (arr[i] == 1)
      {
        count++;
      }
    }

  Console.WriteLine(string.Join(' ', arr));
  Console.WriteLine(count);
  }
}