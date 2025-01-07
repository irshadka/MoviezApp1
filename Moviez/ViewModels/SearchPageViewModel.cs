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
        _currentPage = 1;
    }

    [RelayCommand]
    private async Task MovieTapped(MoviesData selectedItem)
    {
        var navigationParameter = new ShellNavigationQueryParameters
            {
               { "MovieData", selectedItem }
            };
        await Shell.Current.GoToAsync(nameof(MovieDetailPage), false, navigationParameter);
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
            _currentPage = 1;
            var response = await _movieApiService.SearchMovies(query, _currentPage.ToString());
            if (response.Success)
            {
                if(response.results == null)
                    return;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Movies = new ObservableCollection<MoviesData>();
                    var filtered = response.results.Where(i => i.poster_path != null).ToList();
                    foreach (var movie in filtered)
                    {
                        Movies.Add(movie);
                    }
                    if (Movies == null || Movies.Count == 0)
                    {
                        NoDataLabelVisible = true;
                        _hasMoreItems = false;
                    }

                    else
                    {
                        NoDataLabelVisible = false;
                        _currentPage++;
                        _totalPages = response.total_pages;
                        if (_totalPages < _currentPage)
                            _hasMoreItems = false;
                        else
                            _hasMoreItems = true;
                    }

                });
            }
            else
            {
                _hasMoreItems = false;
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
    [RelayCommand]
    private async Task LoadMoreItems()
    {
        if (IsBusy)
            return;

        IsBusy = true;
        try
        {
            if (_hasMoreItems)
            {
                var response = await _movieApiService.SearchMovies(_searchBarText, _currentPage.ToString());
                if (response.Success)
                {
                    if(response.results == null)
                    return;
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                         var filtered = response.results.Where(i => i.poster_path != null).ToList();
                        foreach (var movie in filtered)
                        {
                            Movies.Add(movie);
                        }

                        _currentPage++;
                        _totalPages = response.total_pages;
                        if (_totalPages < _currentPage)
                            _hasMoreItems = false;
                        else
                            _hasMoreItems = true;
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


        }
        finally
        {
            IsBusy = false;
        }
        IsBusy = false;
    }
}
