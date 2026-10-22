using System;

class Program
{
  static void Main()
  {
    string s1  = "123asdfg456";

    int count = 0;

    for (int i = 0; i < s1.Length; i++)
    {
      if (s1[i] >= '0' && s1[i] <= '9')
      {
        count++;
      }
    }
    
    Console.WriteLine(count);
  }
}