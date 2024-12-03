using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moviez.Common;
using Moviez.Interfaces;
using Moviez.Models.ApiResponses;
using Moviez.Views;

namespace Moviez.ViewModels
{
    public partial class HomePageViewModel : PageBaseViewModel
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
        public HomePageViewModel(IMoviezAPIService movieApiService)
        {
            _movieApiService = movieApiService;

            IsBusy = true;
            Task.Run(() =>
            {
                _hasMoreItems = true;
                _currentPage = 1;
                GetTrendingMoviesAsync(_currentPage);
            });
        }

      
        [RelayCommand]
        private void SearchBarTextChanged()
        {
            Debug.WriteLine(SearchBarText);
            if (SearchBarText == string.Empty)
            {
                if (Movies != _baseMoviesData)
                {
                    Movies = _baseMoviesData;
                    NoDataLabelVisible = false;
                }
            }
        }
        [RelayCommand]
        private async Task MovieTapped(MoviesData selectedItem)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            var navigationParameter = new ShellNavigationQueryParameters
            {
               { "MovieData", selectedItem }
            };
            await Shell.Current.GoToAsync(nameof(MovieDetailPage), navigationParameter);
            IsBusy = false;
        }
        [RelayCommand]
        private async Task LoadMoreItems()
        {
            if (Movies != _baseMoviesData)
                return;
            if (IsBusy || !_hasMoreItems) return;

            IsBusy = true;
            try
            {
                var response = await _movieApiService.GetTrendingMovies(_currentPage.ToString());

                if (response.results.Any())
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        foreach (var movie in response.results)
                        {
                            Movies.Add(movie);
                        }
                        _baseMoviesData = Movies;
                        _currentPage++;
                        _totalPages = response.total_pages;
                        if (_totalPages < _currentPage)
                            _hasMoreItems = false;
                        else
                            _hasMoreItems = true;
                    });

                }
                else
                {
                    _hasMoreItems = false; // No more items to load
                }
            }
            finally
            {
                MainThread.BeginInvokeOnMainThread(() => { IsBusy = false; });

            }
        }

        private async void GetTrendingMoviesAsync(int currentPage)
        {
            try
            {

                var response = await _movieApiService.GetTrendingMovies(currentPage.ToString());
                if (response.Success)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Movies = response.results;
                        _baseMoviesData = Movies;
                    });
                    _currentPage++;
                    _totalPages = response.total_pages;
                    if (_totalPages < _currentPage)
                        _hasMoreItems = false;
                    else
                        _hasMoreItems = true;

                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert(AppResources.AppTitle, response.Message);
                    });
                }
            }
            finally
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsBusy = false;
                });
            }
        }
    }
}

