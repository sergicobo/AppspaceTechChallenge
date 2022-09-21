namespace AppspaceTechChallenge.API.Models
{
    public class TvShowDTO : RecommendationDTO
    {
        public int NumberOfSeasons { get; set; }
        public int NumberOfEpisodes { get; set; }
        public bool IsFinished { get; set; }
    }
}
