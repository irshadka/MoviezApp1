using System;
using System.Collections.ObjectModel;
using Moviez.Common;

namespace Moviez.Models.ApiResponses
{
    public class MovieListResponse :BaseResponse
    {
        public int page { get; set; }
        public ObservableCollection<MoviesData> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class MoviesData
    {
        public string backdrop_path { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
        public string media_type { get; set; }
        public bool adult { get; set; }
        public string original_language { get; set; }
        public List<int> genre_ids { get; set; }
        public double popularity { get; set; }
        public string release_date { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public string imageurl { get => MoviezAppConstants.TMDBimagePath + poster_path; }

    }
}

