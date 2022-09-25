using System;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Models
{
    /// <summary>
    /// Fields for a documentary.
    /// </summary>
    public class DocumentaryDTO : RecommendationDTO
    {
        public DocumentaryDTO(string title, string overview, IEnumerable<string> genres, string language, 
            DateTime releaseDate, string webSite, IEnumerable<string> associatedKeywords) 
            : base(title, overview, genres, language, releaseDate, webSite, associatedKeywords)
        {
        }
    }
}
