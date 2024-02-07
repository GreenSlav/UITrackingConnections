using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UITracking.CustomServices;
using UITracking.Models;

namespace UITracking.ViewModels;

public partial class RegisterServerPageViewModel : BaseViewModel
{
    private IConnectivity Connectivity;
    
    public RegisterServerPageViewModel(IConnectivity connectivity)
    {
        Title = "Add a server";
        Connectivity = connectivity;
    }

    [ObservableProperty] private string enteredName;

    [ObservableProperty] private string enteredHost;

    [ObservableProperty] private string enteredLogin;

    [ObservableProperty] private string enteredPassword;

    [RelayCommand]
    async Task SaveEnteredCommand()
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Error", "Check your internet connection!", "OK");
            return;
        }

        try
        {
            var sshService = new SshService(EnteredHost, EnteredName, EnteredLogin, EnteredPassword);
            var answer = await sshService.IsDataCorrect();

            if (answer)
            {
                var serverToPass = new Server(EnteredHost, EnteredName, EnteredLogin, EnteredPassword);
                //await Shell.Current.GoToAsync($"..?serverPassed={new Server(EnteredHost, EnteredName, EnteredLogin, EnteredPassword)}");
                await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
                {
                    {"serverPassed",  serverToPass},
                    { "example", "its working"}
                });
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Entered data is not correct!", "OK");
            }
            //await Shell.Current.GoToAsync($"..?serverPassed={new Server(EnteredHost, EnteredName, EnteredLogin, EnteredPassword)}");
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", e.Message, "OK");
        }
    }
}