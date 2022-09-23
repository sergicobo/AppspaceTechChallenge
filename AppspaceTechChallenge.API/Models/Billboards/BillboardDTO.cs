using System;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Models.Billboards
{
    public class BillboardDTO
    {
        public DateTime StartDate { get; set; }
        public IEnumerable<MovieDTO> BigScreenMovies { get; set; }
        public IEnumerable<MovieDTO> SmallScreenMovies { get; set; }

        public BillboardDTO(DateTime startDate, IEnumerable<MovieDTO> bigScreenMovies, IEnumerable<MovieDTO> smallScreenMovies)
        {
            StartDate = startDate;
            BigScreenMovies = bigScreenMovies;
            SmallScreenMovies = smallScreenMovies;
        }
    }
}