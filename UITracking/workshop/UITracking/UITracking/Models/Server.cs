namespace UITracking.Models;

public class Server
{
    public string ServerAddress { get; }
    public string Name { get; set; } // name.Length < 18
    public string Login { get; }
    public string Password { get; }

    public HashSet<string> BannedAddresses;
    public Server(string serverAddress, string name, string login, string password)
    {
        ServerAddress = serverAddress;
        Name = (name is null) ? "___" : name;
        Name = (Name.Length < 18) ? Name : Name.Substring(0, 17);
        Login = login;
        Password = password;
    }
}