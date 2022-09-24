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
    public class BillboardService : IBillboardService
    {
        private readonly ITMDBProxy _tmdbProxy;
        private readonly IBeezyCinemaRepository _beezyCinemaRepository;
        private IEnumerable<GenreData> Genres { get; set; }
        private bool UseProxy { get; set; }

        public BillboardService(ITMDBProxy tmdbProxy, IBeezyCinemaRepository beezyCinemaRepository)
        {
            _tmdbProxy = tmdbProxy;
            _beezyCinemaRepository = beezyCinemaRepository;
        }

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

        private async Task<IEnumerable<BillboardDTO>> BuildBillboards(TimePeriod period, int bigRooms, int smallRooms, string city)
        {
            var billboards = new List<BillboardDTO>();

            var weeks = period.GetWeeks();
            var currentWeekDate = period.StartDate.Value; //We validate value previously so it cannot be null.

            //Preloading week 1
            var moviesForBigRooms = bigRooms > 0 ? await GetMovies(city, 1, currentWeekDate, blockbuster: true) : new List<MovieData>();
            var moviesForSmallRooms = smallRooms > 0 ? await GetMovies(city, 1, currentWeekDate, blockbuster: false) : new List<MovieData>();

            for (var week = 1; week <= weeks; ++week)
            {
                if (!moviesForBigRooms.Any()) moviesForBigRooms = await GetMovies(city, week, currentWeekDate, blockbuster: true);
                if (!moviesForSmallRooms.Any()) moviesForSmallRooms = await GetMovies(city, week, currentWeekDate, blockbuster: false);

                billboards.Add(new BillboardDTO(currentWeekDate,
                    ToDto(moviesForBigRooms.Take(bigRooms)), 
                    ToDto(moviesForSmallRooms.Take(smallRooms))));

                moviesForBigRooms = moviesForBigRooms.Skip(bigRooms);
                moviesForSmallRooms = moviesForSmallRooms.Skip(smallRooms);

                currentWeekDate = currentWeekDate.AddDays(TimePeriod.WeeekDefinition);
            }

            return billboards;
        }

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
