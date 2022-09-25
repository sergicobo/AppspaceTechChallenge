using System;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Models
{
    /// <summary>
    /// Generic fields for any recommendation.
    /// </summary>
    public abstract class RecommendationDTO
    {
        /// <summary>
        /// Title of the recommendation.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Overview of the recommendation.
        /// </summary>
        public string Overview { get; set; }

        /// <summary>
        /// List of genres associated with the recommendation.
        /// </summary>
        public IEnumerable<string> Genres { get; set; }

        /// <summary>
        /// Original language of the recommendation.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Release date of the recommendation.
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Official website of the recommendation.
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// List of keywords associated with the recommendation.
        /// </summary>
        public IEnumerable<string> AssociatedKeywords { get; set; }

        protected RecommendationDTO(string title, string overview, IEnumerable<string> genres, string language, 
            DateTime releaseDate, string webSite, IEnumerable<string> associatedKeywords)
        {
            Title = title;
            Overview = overview;
            Genres = genres;
            Language = language;
            ReleaseDate = releaseDate;
            WebSite = webSite;
            AssociatedKeywords = associatedKeywords;
        }
    }
}
