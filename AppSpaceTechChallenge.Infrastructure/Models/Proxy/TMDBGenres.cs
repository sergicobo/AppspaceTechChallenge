using Newtonsoft.Json;

namespace AppspaceTechChallenge.Infrastructure.Models.Proxy
{
    public class TMDBGenres
    {
        [JsonProperty("genres")]
        public Genre[] Result { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
