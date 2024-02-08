namespace UITracking;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(RegisterServerPage), typeof(RegisterServerPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}