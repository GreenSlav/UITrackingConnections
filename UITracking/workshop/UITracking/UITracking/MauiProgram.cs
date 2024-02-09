using Microsoft.Extensions.Logging;
using UITracking.CustomServices;
using UITracking.CustomServices;
using UITracking.ViewModels;

namespace UITracking;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddTransient<RegisterServerPage>();
        builder.Services.AddTransient<RegisterServerPageViewModel>();
        
        builder.Services.AddTransient<ServerPage>();
        builder.Services.AddTransient<ServerPageViewModel>();

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        builder.Services.AddSingleton<SshService>();
        builder.Services.AddSingleton<AddressService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}