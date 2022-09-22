using System;
using System.Collections.Generic;

namespace AppspaceTechChallenge.Domain.Models
{
    public class MovieData
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public List<string> Genres { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
