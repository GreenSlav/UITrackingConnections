using Renci.SshNet;

namespace UITracking;

public static class Server
{
    public static List<string> GetXrayNetstatFromServer(string host, string username, string password)
    {
        var result = new List<string>();
        
        using (var client = new SshClient(host, username, password))
        {
            client.Connect();

            using (var cmd = client.CreateCommand("netstat -natp | grep xray"))
            {
                var asyncResult = cmd.BeginExecute();
                using (var reader = new StreamReader(cmd.OutputStream))
                {
                    while (!asyncResult.IsCompleted || !reader.EndOfStream)
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            result.Add(line);
                        }
                    }
                    
                    cmd.EndExecute(asyncResult);
                }

                client.Disconnect();
            }
        }

        return result;
    }

    public static HashSet<string> GetForeignIPListFromServer(string host, string username, string password)
    {
        var result = new HashSet<string>();

        var list = GetXrayNetstatFromServer(host, username, password);

        foreach (var VARIABLE in list)
        {
            result.Add(FilterMethods.GetForeignAddressFromLine(VARIABLE));
        }

        return result;
    }


    public static void BanAddress(string host, string username, string password, ref ForeignAddressUnit addressToBan)
    {
        try
        {
            using (var client = new SshClient(host, username, password))
            {
                client.Connect();
            
                string banCommand = $"sudo iptables -A INPUT -s {addressToBan} -j DROP";
                var cmdToBan = client.CreateCommand(banCommand);
                cmdToBan.Execute();

                string saveCommand = "sudo netfilter-persistent save";
                var cmdToSave = client.CreateCommand(saveCommand);
                cmdToSave.Execute();

                addressToBan.IsBanned = true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }


    public static void UnBanAddress(string host, string username, string password, ref ForeignAddressUnit addressToUnBan)
    {
        try
        {
            using (var client = new SshClient(host, username, password))
            {
                client.Connect();
            
                string banCommand = $"sudo iptables -A INPUT -s {addressToUnBan} -j DROP";
                var cmdToBan = client.CreateCommand(banCommand);
                cmdToBan.Execute();

                string saveCommand = "sudo netfilter-persistent save";
                var cmdToSave = client.CreateCommand(saveCommand);
                cmdToSave.Execute();

                addressToUnBan.IsBanned = false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}