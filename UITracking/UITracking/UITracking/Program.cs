using System;
using Renci.SshNet;

namespace UITracking;

class Program
{
    static void Main(string[] args)
    {
        string host = "ip_address";
        string username = "username";
        string password = "password";

        using (var client = new SshClient(host, username, password))
        {
            client.Connect();

            var command = client.CreateCommand("ls");
            var result = command.Execute();
            
            Console.Write(result);
        }
    }
}