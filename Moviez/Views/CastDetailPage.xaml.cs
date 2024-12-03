using Moviez.ViewModels;

namespace Moviez.Views;

public partial class CastDetailPage : ContentPage
{
	public CastDetailPage(CastDetailPageViewModel viewModel)
	{
        Shell.SetNavBarIsVisible(this, false);
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}
