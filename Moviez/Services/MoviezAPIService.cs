using System;
using Moviez.Common;
using System.Net.Http.Headers;
using Moviez.Interfaces;
using Moviez.Models.ApiResponses;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Moviez.Services
{
    public class MoviezAPIService : IMoviezAPIService
    {
        #region Vaiables
        private HttpClient _baseClient;
        private string BaseIpAddress = "https://api.themoviedb.org/3/";

        private string AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJkNGQwMTZhNTY1OTAyYTFiY2VmMjJjZGZmZWRhYjdiZSIsIm5iZiI6MTcyOTkxMTc1Ni4wMDcxMTgsInN1YiI6IjYxYjlhM2NhMWM2MzI5MDA5ZDE5YzFhNSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.641Ek1fQJGIOHbirTnk4E3uGZ_4dbOpksljo2ACiofk";

        private string GetTrendingMoviesUri = "trending/movie/week?language=en-US&page=";
        private string SearchMoviesUri = "search/movie?query=";
        private string GetMovieDetailsUri = "movie/";
        private string GetCreditsUri = "/credits?language=en-US";



        #endregion

        /// <summary>
        /// Gets the base client.
        /// </summary>
        /// <value>The base client.</value>
        private HttpClient BaseClient
        {
            get
            {
                return _baseClient ?? (_baseClient = new HttpClient());
            }
        }

        public bool IsConnected()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
                return true;
            else
                return false;
        }

        public async Task<MovieDetailsResponse> GetMovieDetails(string movieId)
        {
            MovieDetailsResponse response = new MovieDetailsResponse();

            try
            {
                if (IsConnected())
                {


                    var httpResp = new HttpResponseMessage();


                    BaseClient.DefaultRequestHeaders.Clear();
                    BaseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                    var url = string.Format(@"{0}{1}{2}{3}", BaseIpAddress, GetMovieDetailsUri, movieId, "?language=en-US");
                    httpResp = BaseClient.GetAsync(url).Result;
                    var responseData = await httpResp.Content.ReadAsStringAsync();
                    Debug.WriteLine(responseData);
                    response = JsonConvert.DeserializeObject<MovieDetailsResponse>(responseData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    response.Success = true;
                }
                else
                {

                    response.Success = false;
                    response.Message = AppResources.NoConnectionMessage;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception : {0}", ex.Message);
                response.Success = false;
                response.Message = AppResources.ErrorWentWrong;
            }
            return response;
        }

        public async Task<MovieListResponse> GetTrendingMovies(string pageNumber)
        {
            MovieListResponse response = new MovieListResponse();

            try
            {
                if (IsConnected())
                {


                    var httpResp = new HttpResponseMessage();


                    BaseClient.DefaultRequestHeaders.Clear();
                    BaseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                    var url = string.Format(@"{0}{1}{2}", BaseIpAddress, GetTrendingMoviesUri, pageNumber);
                    httpResp = BaseClient.GetAsync(url).Result;
                    if (httpResp.IsSuccessStatusCode)
                    {
                        var responseData = await httpResp.Content.ReadAsStringAsync();
                        Debug.WriteLine(responseData);
                        
                        response = JsonConvert.DeserializeObject<MovieListResponse>(responseData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        response.Success = true;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = AppResources.ErrorWentWrong;
                    }
                }
                else
                {

                    response.Success = false;
                    response.Message = AppResources.NoConnectionMessage;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception : {0}", ex.Message);
                response.Success = false;
                response.Message = AppResources.ErrorWentWrong;
            }
            return response;
        }

        public async Task<MovieListResponse> SearchMovies(string keyword)
        {
            MovieListResponse response = new MovieListResponse();

            try
            {
                if (IsConnected())
                {


                    var httpResp = new HttpResponseMessage();


                    BaseClient.DefaultRequestHeaders.Clear();
                    BaseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                    var url = string.Format(@"{0}{1}{2}{3}", BaseIpAddress, SearchMoviesUri,keyword, "&include_adult=false&language=en-US&page=1");
                    httpResp = BaseClient.GetAsync(url).Result;
                    var responseData = await httpResp.Content.ReadAsStringAsync();
                    Debug.WriteLine(responseData);
                    response = JsonConvert.DeserializeObject<MovieListResponse>(responseData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    response.Success = true;
                }
                else
                {

                    response.Success = false;
                    response.Message = AppResources.NoConnectionMessage;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception : {0}", ex.Message);
                response.Success = false;
                response.Message = AppResources.ErrorWentWrong;
            }
            return response;
        }

        public async Task<CreditResponse> GetMovieGredits(string movieId)
        {
            CreditResponse response = new CreditResponse();

            try
            {
                if (IsConnected())
                {


                    var httpResp = new HttpResponseMessage();


                    BaseClient.DefaultRequestHeaders.Clear();
                    BaseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                    var url = string.Format(@"{0}{1}{2}{3}", BaseIpAddress, GetMovieDetailsUri, movieId, GetCreditsUri);
                    httpResp = BaseClient.GetAsync(url).Result;
                    var responseData = await httpResp.Content.ReadAsStringAsync();
                    Debug.WriteLine(responseData);
                    response = JsonConvert.DeserializeObject<CreditResponse>(responseData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    response.Success = true;
                }
                else
                {

                    response.Success = false;
                    response.Message = AppResources.NoConnectionMessage;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception : {0}", ex.Message);
                response.Success = false;
                response.Message = AppResources.ErrorWentWrong;
            }
            return response;
        }
    }
}

