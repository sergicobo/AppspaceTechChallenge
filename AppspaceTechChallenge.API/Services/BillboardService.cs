using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppspaceTechChallenge.API.Contracts;
using AppspaceTechChallenge.API.Models;
using AppspaceTechChallenge.API.Models.Billboards;
using AppspaceTechChallenge.Domain.Extensions;
using AppspaceTechChallenge.Domain.Models;
using AppspaceTechChallenge.Domain.Proxies;

namespace AppspaceTechChallenge.API.Services
{
    public class BillboardService : IBillboardService
    {
        private readonly ITMDBProxy _tmdbProxy;
        private IEnumerable<GenreData> Genres { get; set; }

        public BillboardService(ITMDBProxy tmdbProxy)
        {
            _tmdbProxy = tmdbProxy;
        }

        public async Task<IEnumerable<BillboardDTO>> BuildIntelligentBillboards(IntelligentBillboardRequest request)
        {
            ValidateRequest(request);

            Genres = await _tmdbProxy.GetGenres();

            var billboards = await BuildBillboards(request.TimePeriod, 
                request.BigRooms, request.SmallRooms,
                request.City);

            return billboards;
        }

        private async Task<IEnumerable<BillboardDTO>> BuildBillboards(TimePeriod period, int bigRooms, int smallRooms, string city)
        {
            var billboards = new List<BillboardDTO>();

            var weeks = period.GetWeeks();
            var currentWeekDate = period.StartDate.Value;
            
            for (var week = 0; week < weeks; ++week)
            {
                var moviesForBigRooms = bigRooms > 0 ? await _tmdbProxy.GetMovies(bigRooms, currentWeekDate,
                    currentWeekDate.AddDays(TimePeriod.WeeekDefinition), true) : new List<MovieData>();
                var moviesForSmallRooms = smallRooms > 0 ? await _tmdbProxy.GetMovies(smallRooms, currentWeekDate, 
                    currentWeekDate.AddDays(TimePeriod.WeeekDefinition), false) : new List<MovieData>();

                billboards.Add(new BillboardDTO(currentWeekDate,
                    ToDto(moviesForBigRooms.Take(bigRooms)), 
                    ToDto(moviesForSmallRooms.Take(smallRooms))));

                currentWeekDate = currentWeekDate.AddDays(TimePeriod.WeeekDefinition);
            }

            return billboards;
        }

        private void ValidateRequest(IntelligentBillboardRequest request)
        {
            request.TimePeriod.Validate();
            if (request.BigRooms < 0 || request.SmallRooms < 0) throw new ArgumentException("Rooms needs to be positive.");
            if (request.BigRooms == 0 && request.SmallRooms == 0) throw new ArgumentException("You need at least one room.");
        }

        private IEnumerable<MovieDTO> ToDto(IEnumerable<MovieData> movies)
        {
            return movies.Select(m => new MovieDTO()
            {
                Title = m.Title,
                Language = m.Language,
                Overview = m.Overview,
                Genres = m.Genres.Select(g => Genres.MatchById(g)),
                ReleaseDate = m.ReleaseDate
            });
        }
    }
}
