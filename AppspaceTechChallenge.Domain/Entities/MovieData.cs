using System;

namespace AppspaceTechChallenge.Domain.Entities
{
    public class MovieData
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public int[] Genres { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool Blockbuster { get; set; }
    }
}
