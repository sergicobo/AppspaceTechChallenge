using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AppspaceTechChallenge.Infrastructure.Models
{
    public class TMDBResult
    {
        public IEnumerable<Result> Results { get; set; }
    }

    public class Result
    {
        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        [JsonProperty("genre_ids")] 
        public int[] Genres { get; set; }
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }
        [JsonProperty("release_date")] 
        public DateTime ReleaseDate { get; set; }

    }
}
