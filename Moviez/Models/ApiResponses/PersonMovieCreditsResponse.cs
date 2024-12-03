using System;
using Moviez.Common;

namespace Moviez.Models.ApiResponses
{
	
    public class PersonMovieCast
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public string character { get; set; }
        public string credit_id { get; set; }
        public int order { get; set; }
        public string imageurl { get => MoviezAppConstants.TMDBimagePath + poster_path; }
    }

    public class PersonMovieCrew
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public string credit_id { get; set; }
        public string department { get; set; }
        public string job { get; set; }
        public string imageurl { get => MoviezAppConstants.TMDBimagePath + poster_path; }
    }

    public class PersonMovieCreditsResponse: BaseResponse
    {
        public List<PersonMovieCast> cast { get; set; }
        public List<PersonMovieCrew> crew { get; set; }
        public int id { get; set; }
    }
}

