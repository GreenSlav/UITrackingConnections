using Renci.SshNet;

namespace UITracking;

public static class ReadingServerMethods
{
    public static List<string> GetXrayIPsFromServer(string host, string username, string password)
    {
        using (var client = new SshClient(host, username, password))
        {
            client.Connect();

            using (var command = client.CreateCommand("netstat -natp | grep xray"))
            {
                
            }
        }
    }
}