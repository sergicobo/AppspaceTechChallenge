namespace AppspaceTechChallenge.Domain.Entities
{
    public class TvShow : Recommendation
    {
        public int NumberOfSeasons { get; set; }
        public int NumberOfEpisodes { get; set; }
        public bool IsFinished { get; set; }
    }
}
