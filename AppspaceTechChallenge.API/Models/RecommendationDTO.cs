using System;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Models
{
    public abstract class RecommendationDTO
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string WebSite { get; set; }
        public IEnumerable<string> AssociatedKeywords { get; set; }
    }
}
