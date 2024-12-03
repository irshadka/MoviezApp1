using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moviez.Common;
using Moviez.Interfaces;
using Moviez.Models;
using Moviez.Models.ApiResponses;
using Moviez.Views;

namespace Moviez.ViewModels
{
    public partial class MovieDetailpageViewModel : PageBaseViewModel, IQueryAttributable
    {
        private IMoviezAPIService _movieApiService;
        [ObservableProperty]
        private MoviesData _movieData;
        [ObservableProperty]
        private MovieDetailsModel _movieDetails;
        [ObservableProperty]
        private List<Cast> _movieCast;

        public MovieDetailpageViewModel(IMoviezAPIService movieApiService)
        {
            _movieApiService = movieApiService;
            MovieDetails = new MovieDetailsModel();
            MovieCast = new List<Cast>();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count == 0)
                return;
            MovieData = query["MovieData"] as MoviesData;
            OnPropertyChanged("Moviedata");

            if (MovieData != null)
            {
                MovieDetails.MoviePoster = MoviezAppConstants.TMDBimageDetailPath + MovieData.poster_path;
                Task.Run(() =>
                {

                    GetMovieDetails(MovieData.id.ToString());
                    GetMovieCredits(MovieData.id.ToString());
                });

            }
        }
        private async void GetMovieCredits(string movieId)
        {
            var response = await _movieApiService.GetMovieGredits(movieId);
            if (response.Success)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (response != null && response.cast != null)
                    {
                        MovieCast = response.cast;
                    }
                });
            }
        }
        private async void GetMovieDetails(string movieId)
        {
            IsBusy = true;
            try
            {
                var response = await _movieApiService.GetMovieDetails(movieId);
                if (response.Success)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        if (response != null)
                        {

                            MovieDetails.Id = response.id.ToString();
                            DateTime date = Convert.ToDateTime(response.release_date);
                            int year = date.Year;
                            MovieDetails.ReleaseYear = year;
                            var generlist = new List<string>();
                            foreach (var gener in response.genres)
                            {
                                generlist.Add(gener.name);
                            }
                            string combindedgeners = string.Join(", ", generlist);
                            MovieDetails.Genere = combindedgeners;
                            var time = TimeSpan.FromMinutes(response.runtime);
                            Console.WriteLine("{0:00}:{1:00}", (int)time.TotalHours, time.Minutes);
                            MovieDetails.Duration = string.Format("{0}h {1}mins", time.Hours, time.Minutes);
                        }
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
        [RelayCommand]
        private async Task CastTapped(Cast selectedItem)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            var navigationParameter = new ShellNavigationQueryParameters
            {
               { "CastData", selectedItem }
            };
            await Shell.Current.GoToAsync(nameof(CastDetailPage), navigationParameter);
            IsBusy = false;
        }
            
    }
}

