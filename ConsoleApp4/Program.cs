public class Transaction
{
  public double Amount {get; set;}
  public DateTime Date {get; set;}

  public Transaction(double amount)
  {
    Amount = amount;
    Date = DateTime.Now;
  }
}

public class Customer
{
  public string Name {get; set;}
  public string Email {get; set;}

  public Customer(string name, string email)
  {
    Name = name;
    Email = email;
  }
}

public class Account
{
  public double Balance {get; set;}
  public string Number {get; set;}
  public Customer Owner {get; set;}
  public List<Transaction> transactions {get; private set;}

  public Account (string number, Customer owner)
  {
    Number = number;
    Owner = owner;
    Balance = 0.0;
    transactions = new List<Transaction>();
  }

  public void Deposit(double amount)
  {
    if (amount > 0)
    {
      Balance +=amount;
      transactions.Add(new Transaction(amount));
    }else
    {
      Console.WriteLine("депозит ранвен 0");
    }
  }

  public bool Withdraw(double amount)
  {
    if (amount > 0 && Balance >= amount)
    {
      Balance -= amount;
      transactions.Add(new Transaction(-amount));
      return true;
    }else
    {
      return false;
    }
  }

  public double GetBalance()
  {
    return Balance;
  }
}

public class SavingsAccount : Account
{
  private const double  InterastRate = 0.05;

  public SavingsAccount(string number, Customer owner) : base(number, owner)
  {}

  public void Interface()
  {
    Balance += Balance * InterastRate;
    transactions.Add(new Transaction(Balance + InterastRate));
  } 
}

public class CheckedAccount : Account
{
   private const double  OverdraftFee = 20; 

   public CheckedAccount(string number, Customer owner): base(number, owner)
   {}

     public bool Withdraw(double amount)
  {
    if (amount > 0 && Balance >= amount)
    {
      Balance -= amount;
      transactions.Add(new Transaction(-amount));
    }else
    {
      Balance -= OverdraftFee;
      return false;
    }
    return true;
  }
}

public class Program
{
    public static void Main()
    {
        Customer customer = new Customer("John Doe", "john.doe@example.com");

        SavingsAccount savingsAccount = new SavingsAccount("S123456789", customer);
        CheckingAccount checkingAccount = new CheckingAccount("C123456789", customer);

        savingsAccount.Deposit(1000.0);
        savingsAccount.ApplyInterest();

        checkingAccount.Deposit(500.0);
        checkingAccount.Withdraw(300.0);

        Console.WriteLine($"Savings Account Balance: {savingsAccount.GetBalance()}");
        Console.WriteLine($"Checking Account Balance: {checkingAccount.GetBalance()}");
    }
}


