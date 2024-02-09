using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UITracking.Models;
using UITracking.CustomServices;

namespace UITracking.ViewModels;

[QueryProperty(nameof(Server), "detailedServer")]
public partial class ServerPageViewModel : BaseViewModel
{
    private IConnectivity Connectivity;
    AddressService addressService;
    public ServerPageViewModel(IConnectivity connectivity, AddressService addressService)
    {
        Title = "Connections";
        this.addressService = addressService;
        Connectivity = connectivity;
    }
    
    public ObservableCollection<ForeignAddress> ListOfAddresses { get; } = new();

    [ObservableProperty] private Server server;
    
    
    
    [RelayCommand]
    async Task GetIPsAsync()
    {
        if (IsBusy)
            return;

        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Error", "Check your Internet connection", "OK");
            return;
        }

        try
        {
            ListOfAddresses.Clear();
            IsBusy = true;
            var ipHash  = await addressService.GetForeignIPListFromServerAsync(Server.ServerAddress, Server.Login, Server.Password);
    
            foreach (var ip in ipHash)
            {
                ListOfAddresses.Add(new ForeignAddress(ip));
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}