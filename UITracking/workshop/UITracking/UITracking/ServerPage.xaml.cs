using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITracking.ViewModels;

namespace UITracking;

public partial class ServerPage : ContentPage
{
    public ServerPage(ServerPageViewModel viewmodel)
    {
        InitializeComponent();
        BindingContext = viewmodel;
    }
}