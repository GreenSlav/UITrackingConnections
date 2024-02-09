using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UITracking.Models;

namespace UITracking.ViewModels;

[QueryProperty("PropertyTrulyPassed", "propertyGiven")]
[QueryProperty(nameof(Server), "serverPassed")]

public partial class MainPageViewModel : BaseViewModel
{
    bool propertyTrulyPassed;
    public bool PropertyTrulyPassed
    {
        get => propertyTrulyPassed;
        set
        {
            propertyTrulyPassed = value;
            OnPropertyChanged();
        }
    }
    
    
    
    // Implementation of given parameter
    Server server;
    public Server Server
    {
        get => server;
        set
        {
            // if (value is null)
            //     return;

            if (PropertyTrulyPassed)
            {
                // for (int i = 0; i < ListOfServers.Count; ++i)
                // {
                //     if (ListOfServers[i].ServerAddress == value.ServerAddress)
                //         return;
                // }
                
                ListOfServers.Add(value);
                PropertyTrulyPassed = false;
                
               
            }
            
            //server = value;
            //OnPropertyChanged();
            
            
            //if (PropertyTrulyPassed) 
            //

            // value = null;
            // server = null;
            //PropertyTrulyPassed = false;
            //server = null;
        }
    }


    public ObservableCollection<Server> ListOfServers { get; } = new();

    private IConnectivity Connectivity;
    public MainPageViewModel(IConnectivity connectivity)
    {
        Title = "Servers";
        Connectivity = connectivity;
    }
    

    [RelayCommand]
    async Task GoToRegisterServer()
    {
        await Shell.Current.GoToAsync(nameof(RegisterServerPage));
    }


    [RelayCommand]
    async Task DeleteServer(Server serverToDelete)
    {
        if (ListOfServers is null)
            return;
    
        ListOfServers.Remove(serverToDelete);
        // Server = null;
        //ListOfServers[ListOfServers.IndexOf(serverToDelete)] = null;
    
    
        // int indexToDelete = -1;
        //
        // for (int i = 0; i < ListOfServers.Count; ++i)
        // {
        //     if (ListOfServers[i].ServerAddress == serverToDelete.ServerAddress)
        //     {
        //         indexToDelete = i;
        //         break;
        //     }
        // }
        //
        // if (indexToDelete != -1)
        //     ListOfServers.RemoveAt(indexToDelete);
    }

    [RelayCommand]
    async Task GoToDetailsAsync(Server ServerToDetailed)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Error", "Check your Internet connection", "OK");
        }

        try
        {
            IsBusy = true;

            if (ServerToDetailed is null)
                return;

            await Shell.Current.GoToAsync(nameof(ServerPage), true,
                new Dictionary<string, object>()
                {
                    { "detailedServer", ServerToDetailed },
                });

        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", e.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}