using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UITracking.Models;

namespace UITracking.ViewModels;

//[QueryProperty(nameof(Server), "serverPassed")]
[QueryProperty(nameof(Server), "serverPassed")]
[QueryProperty("Sample", "example")]
public partial class MainPageViewModel : BaseViewModel
{
    // Реализация переданного параметра

    //[ObservableProperty] private string sample;
    // Реализация переданного параметра
    Server server;
    public Server Server
    {
        get => server;
        set
        {
            server = value;
            OnPropertyChanged();
            for (int i = 0; i < ListOfServers.Count; ++i)
            {
                if (ListOfServers[i].ServerAddress == server.ServerAddress)
                    return;
            }
            ListOfServers.Add(value);
        }
    }
    
    public ObservableCollection<Server> ListOfServers { get; } = new();
    
    public MainPageViewModel()
    {
        Title = "Servers";
    }
    
    // необходимо реализовать queryproperty вручную чтоб при передаче добавлять Server в ListOfServers
    
    
    

    //[ObservableProperty] private Server server;

    [RelayCommand]
    async Task GoToRegisterServer()
    {
        await Shell.Current.GoToAsync(nameof(RegisterServerPage));
    }
}