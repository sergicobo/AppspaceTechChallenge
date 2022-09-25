using AppspaceTechChallenge.Domain.Proxies;
using AppspaceTechChallenge.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppspaceTechChallenge.API.Models.Billboards;
using AppspaceTechChallenge.API.Services;
using AppspaceTechChallenge.Domain.Entities;
using NSubstitute;
using Xunit;
using FluentAssertions;
using AppspaceTechChallenge.API.Models;

namespace AppspaceTechChallenge.API.Tests.Services
{
    public class BillboardServiceShould
    {
        private readonly ITMDBProxy _tmdbProxy;
        private readonly IBeezyCinemaRepository _beezyCinemaRepository;

        public BillboardServiceShould()
        {
            _tmdbProxy = Substitute.For<ITMDBProxy>();
            _beezyCinemaRepository = Substitute.For<IBeezyCinemaRepository>();

            _tmdbProxy.GetGenres().Returns(GetSomeGenres());
            _beezyCinemaRepository.GetGenres().Returns(GetSomeGenres());

            _tmdbProxy.GetMovies(Arg.Any<int>(), Arg.Any<DateTime>(), 
                Arg.Any<DateTime>(), true)
                .Returns(GetSomeBigScreenMovies());
            _tmdbProxy.GetMovies(Arg.Any<int>(), Arg.Any<DateTime>(),
                    Arg.Any<DateTime>(), false)
                .Returns(GetSomeSmallScreenMovies());

            _beezyCinemaRepository.GetMovies(Arg.Any<string>(), Arg.Any<DateTime>(),
                    Arg.Any<DateTime>(), true)
                .Returns(GetSomeBigScreenMovies());
            _beezyCinemaRepository.GetMovies(Arg.Any<string>(), Arg.Any<DateTime>(),
                    Arg.Any<DateTime>(), false)
                .Returns(GetSomeSmallScreenMovies());
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        public void Return_ArgumentException_When_Ask_For_Billboards_With_Any_Invalid_Screen_Numbers(int bigRooms, int smallRooms)
        {
            var service = new BillboardService(_tmdbProxy, _beezyCinemaRepository);

            var request = new IntelligentBillboardRequest
            {
                TimePeriod = new TimePeriod(DateTime.Now, DateTime.Now.AddDays(1)),
                BigRooms = bigRooms,
                SmallRooms = smallRooms
            };

            Func<Task> task = async () =>
            {
                await service.BuildIntelligentBillboards(request);
            };

            task.Should().ThrowAsync<ArgumentException>().WithMessage("Rooms needs to be positive.");
        }

        [Fact]
        public void Return_ArgumentException_When_Ask_For_Billboards_Without_Screen_Numbers()
        {
            var service = new BillboardService(_tmdbProxy, _beezyCinemaRepository);

            var request = new IntelligentBillboardRequest
            {
                TimePeriod = new TimePeriod(DateTime.Now, DateTime.Now.AddDays(1)),
                BigRooms = 0,
                SmallRooms = 0,
            };

            Func<Task> task = async () =>
            {
                await service.BuildIntelligentBillboards(request);
            };

            task.Should().ThrowAsync<ArgumentException>().WithMessage("You need at least one room.");
        }

        [Fact]
        public async void Return_Billboards_When_Ask_For_Billboards_With_Valid_Data_Asking_For_City()
        {
            var service = new BillboardService(_tmdbProxy, _beezyCinemaRepository);

            var startDate = new DateTime(2022, 01, 01);
            var endDate = new DateTime(2022, 02, 07);

            var request = new IntelligentBillboardRequest
            {
                TimePeriod = new TimePeriod(startDate, endDate),
                BigRooms = 1,
                SmallRooms = 1,
                City = "city"
            };

            var billboards = await service.BuildIntelligentBillboards(request);

            billboards.Should().HaveCount(5);
            billboards.Should().Match(bb => bb.First().StartDate == startDate);
            billboards.Should().Match(bb => bb.All(bs => bs.BigScreenMovies.Count() == 1));
            billboards.Should().Match(bb => bb.All(bs => bs.SmallScreenMovies.Count() == 1));
            billboards.Should().Match(bb => bb.All(bs => bs.BigScreenMovies.All(m => !string.IsNullOrWhiteSpace(m.Title))));
            billboards.Should().Match(bb => bb.All(bs => bs.SmallScreenMovies.All(m => !string.IsNullOrWhiteSpace(m.Title))));
        }

        [Fact]
        public async void Return_Billboards_When_Ask_For_Billboards_With_Valid_Data_Asking_Without_City()
        {
            var service = new BillboardService(_tmdbProxy, _beezyCinemaRepository);

            var request = new IntelligentBillboardRequest
            {
                TimePeriod = new TimePeriod(new DateTime(2022, 01, 01), new DateTime(2022, 02, 07)),
                BigRooms = 1,
                SmallRooms = 1
            };

            var billboards = await service.BuildIntelligentBillboards(request);

            billboards.Should().HaveCount(5);
        }

        private IEnumerable<GenreData> GetSomeGenres()
        {
            return new List<GenreData>
            {
                new GenreData(1, "Action"),
                new GenreData(2, "Adventure"),
                new GenreData(3, "Thriller"),
                new GenreData(4, "Comedy"),
                new GenreData(5, "Drama"),
                new GenreData(6, "Fantasy"),
                new GenreData(7, "Horror"),
                new GenreData(8, "Western"),
                new GenreData(9, "Animation"),
                new GenreData(10, "Romance")
            };
        }

        private IEnumerable<MovieData> GetSomeBigScreenMovies()
        {
            return new List<MovieData>()
            {
                new MovieData("Title1", "Overview1", new[] {1, 2}, "es", new DateTime(2022, 01, 01), true),
                new MovieData("Title3", "Overview3", new[] {2, 3}, "es", new DateTime(2022, 01, 08), true),
                new MovieData("Title5", "Overview5", new[] {4, 5}, "es", new DateTime(2022, 01, 16), true),
                new MovieData("Title7", "Overview7", new[] {6, 7}, "es", new DateTime(2022, 01, 29), true),
                new MovieData("Title9", "Overview9", new[] {8, 9}, "es", new DateTime(2022, 02, 06), true)
            };
        }

        private IEnumerable<MovieData> GetSomeSmallScreenMovies()
        {
            return new List<MovieData>()
            {
                new MovieData("Title2", "Overview2", new[] {1, 2}, "es", new DateTime(2022, 01, 01), false),
                new MovieData("Title4", "Overview4", new[] {3, 4}, "es", new DateTime(2022, 01, 08), false),
                new MovieData("Title6", "Overview6", new[] {5, 6}, "es", new DateTime(2022, 01, 16), false),
                new MovieData("Title8", "Overview8", new[] {7, 8}, "es", new DateTime(2022, 01, 29), false),
                new MovieData("Title10", "Overview10", new[] {9, 10}, "es", new DateTime(2022, 02, 06), false)
            };
        }
    }
}
