using Moviez.ViewModels;

namespace Moviez.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchPageViewModel viewModel)
	{
		Shell.SetNavBarIsVisible(this,false);
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}