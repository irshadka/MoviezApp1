using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moviez.Interfaces;
using Moviez.Models.ApiResponses;
using Moviez.Views;

namespace Moviez.ViewModels
{
    public partial class CastDetailPageViewModel : PageBaseViewModel, IQueryAttributable
    {
        private IMoviezAPIService _movieApiService;

        [ObservableProperty]
        private Cast _castData;

        [ObservableProperty]
        private List<PersonMovieCast> _personCast;

        public CastDetailPageViewModel(IMoviezAPIService movieApiService)
        {
            _movieApiService = movieApiService;
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count == 0)
                return;
            CastData = query["CastData"] as Cast;
            OnPropertyChanged("Moviedata");
            if (CastData != null)
            {
                Task.Run(async () =>
                {
                    await GetPersonMovieCredits(CastData.id.ToString());
                });
            }
        }
        [RelayCommand]
        private async Task MovieTapped(PersonMovieCast selectedItem)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            MoviesData movies = new MoviesData();
            movies.id = selectedItem.id;
            movies.overview = selectedItem.overview;
            movies.poster_path = selectedItem.poster_path;
            movies.genre_ids = selectedItem.genre_ids;
            movies.title = selectedItem.title;
            var navigationParameter = new ShellNavigationQueryParameters
            {
               { "MovieData", movies }
            };
            await Shell.Current.GoToAsync(nameof(MovieDetailPage),false, navigationParameter);
            IsBusy = false;
        }

        private async Task GetPersonMovieCredits(string personId)
        {
            var response = await _movieApiService.GetPersonMovieCredits(personId);
            if (response.Success)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (response != null && response.cast != null)
                    {
                        PersonCast = response.cast;
                    }
                });
            }
        }
    }
}

