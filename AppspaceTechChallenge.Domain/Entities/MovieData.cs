using System;

namespace AppspaceTechChallenge.Domain.Entities
{
    public class MovieData
    {
        private readonly string _title;
        private readonly string _overview;
        private readonly int[] _genres;
        private readonly string _language;
        private readonly DateTime _releaseDate;
        private readonly bool _blockbuster;

        public MovieData(string title, string overview, int[] genres, string language, DateTime releaseDate, bool blockbuster)
        {
            _title = title;
            _overview = overview;
            _genres = genres;
            _language = language;
            _releaseDate = releaseDate;
            _blockbuster = blockbuster;
        }

        public string GetTitle()
        {
            return _title;
        }

        public string GetOverview()
        {
            return _overview;
        }

        public int[] GetGenres()
        {
            return _genres;
        }

        public string GetLanguage()
        {
            return _language;
        }

        public DateTime GetReleaseDate()
        {
            return _releaseDate;
        }

        public bool GetBlockbuster()
        {
            return _blockbuster;
        }
    }
}
