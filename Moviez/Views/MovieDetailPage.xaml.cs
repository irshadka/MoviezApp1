using Moviez.ViewModels;

namespace Moviez.Views;

public partial class MovieDetailPage : ContentPage
{
	public MovieDetailPage(MovieDetailpageViewModel viewModel)
	{
		Shell.SetNavBarIsVisible(this, false);
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}
