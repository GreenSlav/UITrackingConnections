using System;
using Renci.SshNet;

namespace UITracking;

class Program
{
    static void Main(string[] args)
    {
        /*string host = "185.102.136.186";
        string username = "root";
        string password = "r8IM7O1ww02L";

        var list = ReadingServerMethods.GetXrayIPsFromServer(host, username, password);

        foreach (var VARIABLE in list)
        {
            Console.WriteLine(VARIABLE);
        }*/

        var unit = new ForeignAddressUnit("111312");
        
        Console.WriteLine(unit.IP);


    }
}