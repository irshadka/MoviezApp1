using System.Diagnostics;
using Moviez.Common;
using Moviez.ViewModels;

namespace Moviez.Views;

public partial class HomePage : ContentPage
{
    private HomePageViewModel _vmodel;

    public HomePage(HomePageViewModel viewModel)
    {
        Shell.SetNavBarIsVisible(this, false);
        InitializeComponent();
        _vmodel = viewModel;
       
        this.BindingContext = viewModel;
      
    }

}
