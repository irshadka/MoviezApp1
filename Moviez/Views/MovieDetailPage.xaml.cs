using Moviez.ViewModels;

namespace Moviez.Views;

public partial class MovieDetailPage : ContentPage
{
	public MovieDetailPage(MovieDetailpageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}
