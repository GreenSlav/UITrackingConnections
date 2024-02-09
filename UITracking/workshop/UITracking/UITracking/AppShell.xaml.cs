using UITracking.Models;

namespace UITracking;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(RegisterServerPage), typeof(RegisterServerPage));
        Routing.RegisterRoute("..", typeof(MainPage));
        Routing.RegisterRoute(nameof(ServerPage), typeof(ServerPage));
    }
}