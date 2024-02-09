using Renci.SshNet;

namespace UITracking.CustomServices;

public class AddressService
{
    //private SshClient _client;
    
    /*
    public AddressService()
    {
        _client = new SshClient();
    }
    */
    

    public async Task<List<string>> GetXrayNetstatFromServerAsync(string host, string login, string password)
    {
        return await Task.Run(() =>
        {
            var result = new List<string>();

            using (var client = new SshClient(host, login, password))
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
                                if (!FilterMethods.IsListeningOnAllPorts(line) && line.Length != 100 &&
                                    result.Count != 0 && result[^1].Length != 100)
                                {
                                    result[^1] += line;
                                }
                                else
                                {
                                    if (!FilterMethods.IsListeningOnAllPorts(line))
                                        result.Add(line);
                                }
                            }
                        }
                    
                        cmd.EndExecute(asyncResult);
                    }

                    client.Disconnect();
                }
            }
            return result;
        });
    }
    
    
    public async Task<HashSet<string>> GetForeignIPListFromServerAsync(string host, string login, string password)
    {
        var result = new HashSet<string>();

        var list = await GetXrayNetstatFromServerAsync(host, login, password);

        foreach (var VARIABLE in list)
        {
            result.Add(FilterMethods.GetForeignAddressFromLine(VARIABLE));
        }

        return result;
    }
}