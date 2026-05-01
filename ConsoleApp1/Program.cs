class Program
{
  static void Main()
  {
    int n = Convert.ToInt32(Console.ReadLine());

    int[] arr = new int[n];

    Random rand = new Random();

    for (int i = 0; i < n; i++)
    {
      arr[i] = rand.Next(1, 101);
    }

    Console.WriteLine("Массив");

    foreach (int val in arr)
    {
      Console.Write(val + " ");
    }
    Console.WriteLine();

    int max = arr[0];

    for (int i = 0; i < n; i++)
    {
      if (arr[i] > max)
      {
        max = arr[i];
      }
    }

    Console.WriteLine(max);
  }
}