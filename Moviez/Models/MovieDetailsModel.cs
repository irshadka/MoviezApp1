using System;
using Moviez.Models.ApiResponses;

namespace Moviez.Models
{
	public class MovieDetailsModel :BaseModel
	{
        public string Id { get; set; }
        public string TmdbMovieId { get; set; }
        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private int _releaseYear;
        public int ReleaseYear { get => _releaseYear; set => SetProperty(ref _releaseYear, value); }
        private string _director;
        public string Director { get => _director; set => SetProperty(ref _director, value); }
        private string _genere;
        public string Genere { get => _genere; set => SetProperty(ref _genere, value); }
        private string _moviePoster;
        public string MoviePoster { get => _moviePoster; set => SetProperty(ref _moviePoster, value); }
        private string _tmdbRating;
        public string TmdbRating { get => _tmdbRating; set => SetProperty(ref _tmdbRating, value); }
        private string _duration;
        public string Duration { get => _duration; set => SetProperty(ref _duration, value); }
        public int RunTime { get; set; }
        private string _description;
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        private List<CastModel> _casts;
        public List<CastModel> Casts { get => _casts; set => SetProperty(ref _casts, value); }
        public List<Genre> Genresids { get; set; }
    }
    public class CastModel : BaseModel
    {
        public int Id;
        private string _castName;
        public string CastName { get => _castName; set => SetProperty(ref _castName, value); }
        private string _castImage;
        public string CastImage { get => _castImage; set => SetProperty(ref _castImage, value); }
        private string _nameInMovie;
        public string NameInMovie { get => _nameInMovie; set => SetProperty(ref _nameInMovie, value); }
        public string ProfilePath { get; set; }
    }
}

