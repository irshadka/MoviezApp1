using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moviez.Common;
using Moviez.Interfaces;
using Moviez.Models.ApiResponses;
using Moviez.Views;

namespace Moviez.ViewModels;

public partial class SearchPageViewModel : PageBaseViewModel
{
    private IMoviezAPIService _movieApiService;
    private bool _hasMoreItems;
    private int _currentPage;
    private int _totalPages;
    [ObservableProperty]
    private string _searchBarText;
    private ObservableCollection<MoviesData> _baseMoviesData { get; set; }
    [ObservableProperty]
    private ObservableCollection<MoviesData> _movies;
    public SearchPageViewModel(IMoviezAPIService movieApiService)
    {
        _movieApiService = movieApiService;
    }

    [RelayCommand]
    private async Task MovieTapped(MoviesData selectedItem)
    {
        var navigationParameter = new ShellNavigationQueryParameters
            {
               { "MovieData", selectedItem }
            };
        await Shell.Current.GoToAsync(nameof(MovieDetailPage), navigationParameter);
    }
    [RelayCommand]
    private async Task SearchMovies(string query)
    {
        await SearchMoviesAsync(query);
    }
    private async Task SearchMoviesAsync(string query)
    {
        if (IsBusy)
            return;

        IsBusy = true;
        try
        {
            var response = await _movieApiService.SearchMovies(query);
            if (response.Success)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Movies = response.results;
                    if (Movies == null || Movies.Count == 0)
                        NoDataLabelVisible = true;
                    else
                        NoDataLabelVisible = false;
                });
            }
            else
            {
                Movies = new ObservableCollection<MoviesData>();
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert(AppResources.AppTitle, response.Message);
                });
            }
        }
        finally
        {
            IsBusy = false;
        }
        IsBusy = false;
    }
}
