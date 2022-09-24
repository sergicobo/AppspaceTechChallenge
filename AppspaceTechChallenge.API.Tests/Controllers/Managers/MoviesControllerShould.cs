using AppspaceTechChallenge.API.Contracts;
using AppspaceTechChallenge.API.Controllers.Managers;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using AppspaceTechChallenge.API.Models.Billboards;
using Xunit;
using System;
using System.Linq;
using AppspaceTechChallenge.API.Models;
using AppspaceTechChallenge.API.Services;
using AppspaceTechChallenge.API.Tests.Mocks;
using AppspaceTechChallenge.Domain.Proxies;
using AppspaceTechChallenge.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace AppspaceTechChallenge.API.Tests.Controllers.Managers
{
    public class MoviesControllerShould
    {
        private readonly IBillboardService _service;
        private readonly NotFoundObjectResult _notFoundResult;

        public MoviesControllerShould()
        {
            _service = Substitute.For<IBillboardService>();
            _notFoundResult = new NotFoundObjectResult("... Coming Soon ...");
        }

        [Theory, AutoData]
        public void Return_NotFoundError_When_Ask_For_Upcoming_Recommendations(int timePeriodFromNow, List<string> ageRates, List<string> genres)
        {
            var controller = new MoviesController(_service);

            var result = controller.GetUpcomingRecommendations(timePeriodFromNow, ageRates, genres);

            result.Should().NotBeNull();
            ((NotFoundObjectResult)result).StatusCode.Should().Be(_notFoundResult.StatusCode);
            ((NotFoundObjectResult)result).Value.Should().Be(_notFoundResult.Value);
        }

        [Theory, AutoData]
        public void Return_NotFoundError_When_Ask_For_Suggested_Billboard(int timePeriod, int numberOfScreens)
        {
            var controller = new MoviesController(_service);

            var result = controller.GetSuggestedBillboard(timePeriod, numberOfScreens);

            result.Should().NotBeNull();
            ((NotFoundObjectResult) result).StatusCode.Should().Be(_notFoundResult.StatusCode);
            ((NotFoundObjectResult) result).Value.Should().Be(_notFoundResult.Value);
        }

        [Fact]
        public async void Return_ValidationProblem_When_Ask_For_Intelligent_Billboards_With_Some_Invalid_Data()
        {
            var proxy = Substitute.For<ITMDBProxy>();
            var repository = Substitute.For<IBeezyCinemaRepository>();

            var service = new BillboardService(proxy, repository);
            var controller = new MoviesController(service);

            //Needed for tests. See https://stackoverflow.com/questions/62899597/asp-net-core-unit-test-throws-null-exception-when-testing-controller-problem-res
            controller.ProblemDetailsFactory = new MockProblemDetailsFactory(); 

            var billboardRequest = new IntelligentBillboardRequest(GetValidTimePeriod(), 0, 0, null);

            var result = await controller.GetSuggestedIntelligentBillboard(billboardRequest);

            var expectedResult = Assert.IsType<ObjectResult>(result);
            expectedResult.StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
            ((ValidationProblemDetails) expectedResult.Value).Detail.Should().Be("You need at least one room.");
        }

        [Theory, AutoData]
        public async void Return_Billboards_When_Ask_For_Intelligent_Billboards(TimePeriod timePeriod, int bigRooms, int smallRooms, string city)
        {
            _service.BuildIntelligentBillboards(Arg.Any<IntelligentBillboardRequest>()).Returns(GetSomeBillboards());

            var controller = new MoviesController(_service);
            var billboardRequest = new IntelligentBillboardRequest(timePeriod, bigRooms, smallRooms, city);

            var result = await controller.GetSuggestedIntelligentBillboard(billboardRequest);

            var expectedResult = (IEnumerable<BillboardDTO>) Assert.IsType<OkObjectResult>(result).Value;
            expectedResult.Count().Should().Be(GetSomeBillboards().Count());
        }

        private TimePeriod GetValidTimePeriod()
        {
            return new TimePeriod()
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(10),
            };
        }

        private IEnumerable<BillboardDTO> GetSomeBillboards()
        {
            return  new List<BillboardDTO>
                    {
                        new BillboardDTO
                        (
                            new DateTime(2022, 09, 19),
                            new List<MovieDTO>()
                            {
                                new MovieDTO()
                                {
                                    Title = "Title1",
                                    Overview = "Overview1",
                                    Genres = new List<string> { "Action", "Adventure" },
                                    Language = "es",
                                    ReleaseDate = new DateTime(2022, 09, 20)
                                }
                            },
                            new List<MovieDTO>()
                            {
                                new MovieDTO()
                                {
                                    Title = "Title2",
                                    Overview = "Overview2",
                                    Genres = new List<string> { "Action", "Adventure" },
                                    ReleaseDate = new DateTime(2022, 09, 21)
                                }
                            }
                        ),

                        new BillboardDTO
                        (
                            new DateTime(2022, 09, 26),
                            new List<MovieDTO>()
                            {
                                new MovieDTO()
                                {
                                    Title = "Title3",
                                    Overview = "Overview3",
                                    Genres = new List<string> { "Action", "Adventure" },
                                    Language = "es",
                                    ReleaseDate = new DateTime(2022, 09, 27)
                                }
                            },
                            new List<MovieDTO>()
                            {
                                new MovieDTO()
                                {
                                    Title = "Title4",
                                    Overview = "Overview4",
                                    Genres = new List<string> { "Action", "Adventure" },
                                    ReleaseDate = new DateTime(2022, 09, 28)
                                }
                            }
                        ),

                        new BillboardDTO
                        (
                            new DateTime(2022, 10, 3),
                            new List<MovieDTO>()
                            {
                                new MovieDTO()
                                {
                                    Title = "Title5",
                                    Overview = "Overview5",
                                    Genres = new List<string> { "Action", "Adventure" },
                                    Language = "es",
                                    ReleaseDate = new DateTime(2022, 10, 4)
                                }
                            },
                            new List<MovieDTO>()
                            {
                                new MovieDTO()
                                {
                                    Title = "Title6",
                                    Overview = "Overview6",
                                    Genres = new List<string> { "Action", "Adventure" },
                                    ReleaseDate = new DateTime(2022, 10, 5)
                                }
                            }
                        ),

                        new BillboardDTO
                        (
                            new DateTime(2022, 10, 10),
                            new List<MovieDTO>()
                            {
                                new MovieDTO()
                                {
                                    Title = "Title7",
                                    Overview = "Overview7",
                                    Genres = new List<string> { "Action", "Adventure" },
                                    Language = "es",
                                    ReleaseDate = new DateTime(2022, 10, 11)
                                }
                            },
                            new List<MovieDTO>()
                            {
                                new MovieDTO()
                                {
                                    Title = "Title8",
                                    Overview = "Overview8",
                                    Genres = new List<string> { "Action", "Adventure" },
                                    ReleaseDate = new DateTime(2022, 10, 12)
                                }
                            }
                        )
                        };
        }
    }
}
