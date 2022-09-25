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
    }
}
