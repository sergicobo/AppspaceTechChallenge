using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppspaceTechChallenge.API.Contracts;
using AppspaceTechChallenge.API.Models;
using AppspaceTechChallenge.API.Models.Billboards;
using AppspaceTechChallenge.Domain.Entities;
using AppspaceTechChallenge.Domain.Extensions;
using AppspaceTechChallenge.Domain.Proxies;
using AppspaceTechChallenge.Domain.Repositories;

namespace AppspaceTechChallenge.API.Services
{
    /// <inheritdoc />
    public class BillboardService : IBillboardService
    {
        /// <inheritdoc />
        private readonly ITMDBProxy _tmdbProxy;
        private readonly IBeezyCinemaRepository _beezyCinemaRepository;

        private IEnumerable<GenreData> Genres { get; set; }
        private bool UseProxy { get; set; }

        public BillboardService(ITMDBProxy tmdbProxy, IBeezyCinemaRepository beezyCinemaRepository)
        {
            _tmdbProxy = tmdbProxy;
            _beezyCinemaRepository = beezyCinemaRepository;
        }

        /// <summary>
        /// Build a BuildIntelligentBillboards for get the parameters.
        /// </summary>
        /// <remarks>
        /// Build a BuildIntelligentBillboards for get all the parameters of BillboardDTO.
        /// </remarks>
        /// <param name="request">List of BillboardDTO parameters</param>
        public async Task<IEnumerable<BillboardDTO>> BuildIntelligentBillboards(IntelligentBillboardRequest request)
        {
            ValidateRequest(request);

            UseProxy = string.IsNullOrWhiteSpace(request.City);

            Genres = await (UseProxy ? _tmdbProxy.GetGenres() : _beezyCinemaRepository.GetGenres());

            var billboards = await BuildBillboards(request.TimePeriod, 
                request.BigRooms, request.SmallRooms,
                request.City);

            return billboards;
        }

        /// <summary>
        /// Build a BuildBillboards for get the parameters.
        /// </summary>
        /// <remarks>
        /// Build a BuildBillboards for get all the parameters of BillboardDTO.
        /// </remarks>
        /// <param name="period">Period of Date</param>
        /// <param name="bigRooms">Number of Big Rooms</param>
        /// <param name="smallRooms">Number of Small Rooms</param>
        /// <param name="city">City ​​chosen by the user</param>
        private async Task<IEnumerable<BillboardDTO>> BuildBillboards(TimePeriod period, int bigRooms, int smallRooms, string city)
        {
            var billboards = new List<BillboardDTO>();

            var weeks = period.GetWeeks();
            var currentWeekDate = period.StartDate.Value; //We validate value previously so it cannot be null.

            //Preloading week 1
            var moviesForBigRooms = Enumerable.Empty<MovieData>();
            var moviesForSmallRooms = Enumerable.Empty<MovieData>();

            for (var week = 1; week <= weeks; ++week)
            {
                if (!moviesForBigRooms.Any()) moviesForBigRooms = bigRooms > 0 ? 
                        await GetMovies(city, week, currentWeekDate, blockbuster: true) : Enumerable.Empty<MovieData>();

                if (!moviesForSmallRooms.Any()) moviesForSmallRooms = smallRooms > 0 ?
                        await GetMovies(city, week, currentWeekDate, blockbuster: false) : Enumerable.Empty<MovieData>();

                billboards.Add(new BillboardDTO(currentWeekDate,
                    ToDto(moviesForBigRooms.Take(bigRooms)), 
                    ToDto(moviesForSmallRooms.Take(smallRooms))));

                moviesForBigRooms = moviesForBigRooms.Skip(bigRooms);
                moviesForSmallRooms = moviesForSmallRooms.Skip(smallRooms);

                currentWeekDate = currentWeekDate.AddDays(TimePeriod.WeeekDefinition);
            }

            return billboards;
        }

        /// <summary>
        /// Build a GetMovies for get the movie.
        /// </summary>
        /// <remarks>
        /// Build a GetMovies for get all the parameters of MovieData.
        /// </remarks>
        /// <param name="city">City ​​chosen by the user</param>
        /// <param name="week">Number of Week</param>
        /// <param name="currentWeekDate">Date of Specific day in a week</param>
        /// <param name="blockbuster">Is Movie a Blockbuster?</param>
        private async Task<IEnumerable<MovieData>> GetMovies(string city, int week, DateTime currentWeekDate, bool blockbuster)
        {
            if (UseProxy)
            {
                return await _tmdbProxy.GetMovies(week, currentWeekDate,
                    currentWeekDate.AddDays(TimePeriod.WeeekDefinition), blockbuster);
            }
            return await _beezyCinemaRepository.GetMovies(city, currentWeekDate,
                currentWeekDate.AddDays(TimePeriod.WeeekDefinition), blockbuster);
        }

        /// <summary>
        /// Build a ValidateRequest for validate of parameters.
        /// </summary>
        /// <remarks>
        /// Build a ValidateRequest for Validations of parameters .
        /// </remarks>
        /// <param name="request">validate the parameters if they are big or small rooms wrong</param>
        private void ValidateRequest(IntelligentBillboardRequest request)
        {
            request.TimePeriod.Validate();
            if (request.BigRooms < 0 || request.SmallRooms < 0) throw new ArgumentException("Rooms needs to be positive.");
            if (request.BigRooms == 0 && request.SmallRooms == 0) throw new ArgumentException("You need at least one room.");
        }

        /// <summary>
        /// Build a ToDto give the MovieData parameters.
        /// </summary>
        /// <remarks>
        /// Build a ToDto for give the moviedata parameters and pass them to movieDTO.
        /// </remarks>
        /// <param name="movies">List of MovieData parameters</param>
        private IEnumerable<MovieDTO> ToDto(IEnumerable<MovieData> movies)
        {
            return movies.Select(m => new MovieDTO
            (
                m.GetTitle(),
                m.GetOverview(),
                m.GetGenres().Select(g => Genres.MatchById(g)),
                m.GetLanguage(),
                m.GetReleaseDate(),
                null,
                null
            ));
        }
    }
}
