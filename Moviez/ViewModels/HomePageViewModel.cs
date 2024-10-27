using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Moviez.Common;
using Moviez.Interfaces;
using Moviez.Models.ApiResponses;
using Moviez.Views;

namespace Moviez.ViewModels
{
    public class HomePageViewModel : PageBaseViewModel
    {
        private IMoviezAPIService _movieApiService;

        private bool _hasMoreItems;
        private int _currentPage;
        private int _totalPages;
        private string _searchBarText;
        private ObservableCollection<MoviesData> _baseMoviesData { get; set; }
        private ObservableCollection<MoviesData> _movies;


        public string SearchBarText
        {
            get => _searchBarText;
            set => SetProperty(ref _searchBarText, value);
        }

        public ObservableCollection<MoviesData> Movies
        {
            get => _movies;
            set => SetProperty(ref _movies, value);
        }
        public ICommand SearchMoviesCommand { get; set; }
        public ICommand MovieTappedCommand { private set; get; }
        public ICommand LoadMoreItemsCommand { get; }
        public ICommand SearchBarTextChangedCommand { get; }
        public HomePageViewModel(IMoviezAPIService movieApiService)
        {
            _movieApiService = movieApiService;
            SearchMoviesCommand = new AsyncRelayCommand<string>(async (query) =>
            {
                await SearchMoviesAsync(query);
            });
            MovieTappedCommand = new Command<MoviesData>((selectedItem) => ExecuteMovieTappedCommand(selectedItem));
            LoadMoreItemsCommand = new AsyncRelayCommand(LoadMoreItemsAsync);
            IsBusy = true;
            Task.Run(() =>
            {
                _hasMoreItems = true;
                _currentPage = 1;
                GetTrendingMoviesAsync(_currentPage);
            });
            SearchBarTextChangedCommand = new Command(() => ExecuteSearchBarTextChangedCommand());
        }

        private void ExecuteSearchBarTextChangedCommand()
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

        private async void ExecuteMovieTappedCommand(MoviesData selectedItem)
        {
            var navigationParameter = new ShellNavigationQueryParameters
            {
               { "MovieData", selectedItem }
            };
            await Shell.Current.GoToAsync(nameof(MovieDetailPage), navigationParameter);
            // await DisplayAlert(AppResources.AppTitle, AppResources.NotImplemented);
        }
        private async Task LoadMoreItemsAsync()
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
                IsBusy = false;
            }
        }
    }
}

