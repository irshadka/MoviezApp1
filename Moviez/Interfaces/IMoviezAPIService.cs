using System;
using Moviez.Models.ApiResponses;

namespace Moviez.Interfaces
{
	public interface IMoviezAPIService
	{
        Task<MovieListResponse> GetTrendingMovies(string pageNumber);
        Task<MovieListResponse> SearchMovies(String keyword, string pageNumber);
        Task<MovieDetailsResponse> GetMovieDetails(String movieId);
        Task<CreditResponse> GetMovieGredits(string movieId);
        Task<PersonMovieCreditsResponse> GetPersonMovieCredits(string personId);
    }
}

