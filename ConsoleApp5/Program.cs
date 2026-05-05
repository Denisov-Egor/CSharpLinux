public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }

    public User(string name, int age, string address)
    {
        Name = name;
        Age = age;
        Address = address;
    }
    public void ChangeName(string newName)
    {
        Name = newName;
    }

    public void ChangeAddress(string newAddress)
    {
        Address = newAddress;
    }
}

public class UserProfile : User
{
    public string PhoneNumber { get; set; }

    public UserProfile(string name, int age, string address, string phoneNumber) : base(name, age, address)
    {
        PhoneNumber = phoneNumber;
    }

    public void ChangePhoneNumber(string newPhoneNumber)
    {
        PhoneNumber = newPhoneNumber;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter user name:");
        string userName = Console.ReadLine();

        Console.WriteLine("Enter user age:");
        int userAge = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter user address:");
        string userAddress = Console.ReadLine();

        User user = new User(userName, userAge, userAddress);
        Console.WriteLine($"User Name: {user.Name}, Age: {user.Age}, Address: {user.Address}");

        Console.WriteLine("Enter new name:");
        string newName = Console.ReadLine();
        user.ChangeName(newName);

        Console.WriteLine("Enter new address:");
        string newAddress = Console.ReadLine();
        user.ChangeAddress(newAddress);
        Console.WriteLine($"Updated User Name: {user.Name}, Updated Address: {user.Address}");

        Console.WriteLine("Enter phone number:");
        string phoneNumber = Console.ReadLine();

        UserProfile userProfile = new UserProfile(userName, userAge, userAddress, phoneNumber);
        Console.WriteLine($"User Name: {userProfile.Name}, Age: {userProfile.Age}, Address: {userProfile.Address}, Phone Number: {userProfile.PhoneNumber}");

        Console.WriteLine("Enter new name:");
        newName = Console.ReadLine();
        userProfile.ChangeName(newName);

        Console.WriteLine("Enter new address:");
        newAddress = Console.ReadLine();
        userProfile.ChangeAddress(newAddress);

        Console.WriteLine("Enter new phone number:");
        string newPhoneNumber = Console.ReadLine();
        userProfile.ChangePhoneNumber(newPhoneNumber);
        Console.WriteLine($"Updated User Name: {userProfile.Name}, Updated Address: {userProfile.Address}, Updated Phone Number: {userProfile.PhoneNumber}");
    }
}