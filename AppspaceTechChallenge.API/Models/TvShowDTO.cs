using System;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Models
{
    /// <summary>
    /// Fields for a TV show.
    /// </summary>
    public class TvShowDTO : RecommendationDTO
    {
        /// <summary>
        /// Number of seasons on the show.
        /// </summary>
        public int NumberOfSeasons { get; set; }

        /// <summary>
        /// Number of episodes on the show.
        /// </summary>
        public int NumberOfEpisodes { get; set; }

        /// <summary>
        /// Says if show are ended or still on air.
        /// </summary>
        public bool IsFinished { get; set; }

        public TvShowDTO(string title, string overview, IEnumerable<string> genres, string language, DateTime releaseDate, string webSite, 
            IEnumerable<string> associatedKeywords, int numberOfSeasons, int numberOfEpisodes, bool isFinished) 
            : base(title, overview, genres, language, releaseDate, webSite, associatedKeywords)
        {
            NumberOfSeasons = numberOfSeasons;
            NumberOfEpisodes = numberOfEpisodes;
            IsFinished = isFinished;
        }
    }
}
