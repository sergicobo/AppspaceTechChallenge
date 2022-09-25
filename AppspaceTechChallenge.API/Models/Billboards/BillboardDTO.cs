using System;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Models.Billboards
{
    /// <summary>
    /// Suggested movies for each week.
    /// </summary>
    public class BillboardDTO
    {
        /// <summary>
        /// Start date for the billboard.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Movies to be projected on big screens.
        /// </summary>
        public IEnumerable<MovieDTO> BigScreenMovies { get; set; }

        /// <summary>
        /// Movies to be projected on small screens.
        /// </summary>
        public IEnumerable<MovieDTO> SmallScreenMovies { get; set; }

        /// <inheritdoc/>
        public BillboardDTO(DateTime startDate, IEnumerable<MovieDTO> bigScreenMovies, IEnumerable<MovieDTO> smallScreenMovies)
        {
            StartDate = startDate;
            BigScreenMovies = bigScreenMovies;
            SmallScreenMovies = smallScreenMovies;
        }
    }
}