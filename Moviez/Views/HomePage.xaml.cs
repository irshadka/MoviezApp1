using System.Diagnostics;
using Camera.MAUI;
using Camera.MAUI.ZXing;
using Moviez.Common;
using Moviez.ViewModels;

namespace Moviez.Views;

public partial class HomePage : ContentPage
{
    private HomePageViewModel _vmodel;

    public HomePage(HomePageViewModel viewModel)
    {
        InitializeComponent();
        _vmodel = viewModel;
        this.BindingContext = viewModel;
    }

}
