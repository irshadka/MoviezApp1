using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Moviez.Common;
using Moviez.Interfaces;
using Moviez.Models;
using Moviez.Models.ApiResponses;

namespace Moviez.ViewModels
{
    public class MovieDetailpageViewModel : PageBaseViewModel, IQueryAttributable
    {
        private IMoviezAPIService _movieApiService;
        private MoviesData _movieData;
        private MovieDetailsModel _movieDetails;
        private List<Cast> _movieCast;
        public MoviesData Moviedata
        {
            get => _movieData;
            set => SetProperty(ref _movieData, value);
        }

        public MovieDetailsModel MovieDetails
        {
            get => _movieDetails;
            set => SetProperty(ref _movieDetails, value);
        }
        public List<Cast> MovieCast
        {
            get => _movieCast;
            set => SetProperty(ref _movieCast, value);
        }
        public MovieDetailpageViewModel(IMoviezAPIService movieApiService)
        {
            _movieApiService = movieApiService;
            MovieDetails = new MovieDetailsModel();
            MovieCast = new List<Cast>();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Moviedata = query["MovieData"] as MoviesData;
            OnPropertyChanged("Moviedata");

            if (Moviedata != null)
            {
                MovieDetails.MoviePoster = MoviezAppConstants.TMDBimageDetailPath + Moviedata.poster_path;
                Task.Run(() =>{

                    GetMovieDetails(Moviedata.id.ToString());
                    GetMovieCredits(Moviedata.id.ToString());
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
    }
}

