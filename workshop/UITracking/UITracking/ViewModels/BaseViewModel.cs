using CommunityToolkit.Mvvm.ComponentModel;

namespace UITracking.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty] private string title;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(isNotBusy))]
    private bool isBusy;

    private bool isNotBusy => !isBusy;


}