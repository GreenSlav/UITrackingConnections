using UITracking.Models;
using Renci.SshNet;
using Shell = Microsoft.Maui.Controls.Shell;

namespace UITracking.CustomServices;        

public class SshService
{
    private Server Client;

    public SshService(string host, string name, string login, string password)
    {
        Client = new Server(host, name, login, password);
    }

    public async Task<bool> IsDataCorrect()
    {
        try
        {
            using (var sshClient = new SshClient(Client.ServerAddress, Client.Login, Client.Password))
            {
                sshClient.Connect();

                return true;
            }
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", e.Message, "OK");
            return false;
        }
    }
    
}