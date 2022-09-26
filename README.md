# AppspaceTechChallenge
### Implementation

A REST Api has been developed using .Net Core 3.1
- __Part 1__: The controller structure is implemented for all described endpoints. <br/>Currently returns a __404 error__ with the following text: __"... Coming Soon ..."__.

- __Part 2__: `api/managers/movies/intelligent` endpoint implemented with full funcionality for both Database and  [TMDB API](https://www.themoviedb.org/documentation/api) searches.
  - Considerations:
    - If TimePeriod.StartDate are not specified, the start date will be the current day.
    - If a city are specified, the search will be done using criteria B.2 (movies that have been successful in the city).
      - If the city doesn't exists on the database an empty list are returned.
    - If a city are not specified, it will search on the external Api.

* Written documentation on DTOs and controllers to make it visible on Swagger.

* [__Autofixture__](https://github.com/AutoFixture/AutoFixture) is used to use AutoData to generate automatic data for tests.
* [__FluentAssertions__](https://fluentassertions.com/) is used to help on the creation of test cases.
* [__NSubstitute__](https://nsubstitute.github.io/) is used to mocking the needed dependencies for test.
* [__Swagger__](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) is used for the documentation and to be able to have a proper GUI to do the tests.
* [__Tiny.RestClient__](https://github.com/jgiacomini/Tiny.RestClient) is used to do REST connections to the API.
* [__Xunit__](https://github.com/xunit/xunit) is used to build tests.


### How to run

- Set `AppspaceTechChallenge.API` as startup project if not selected and run. 
  - If running via Visual Studio, the ports should be:
    - 44309 for HTTPS
    - 7332 for HTTP
  - If running via dotnet run, the ports should be:
    - 5001 for HTTPS
    - 5000 for HTTP
  - HTTP should redirect to HTTPS
  - Swagger index loaded directly without need to add /swagger


### Conclusions

* All application settings such DB connection string, API key and API url should be on appSettings, but for API config (key and url) I found some dependency problems and it will be useful to make it in some more appropiate way.
* Put some logger to log important things like request/responses and some exceptions will be useful.
* On Infrastructure, on database repository, the method to get the movies is really big. I tried to simplify lines extracting some logic on methods, but started to fail getting data from database for the multi-select queries. According to [__this thread__](https://stackoverflow.com/questions/19536064/select-multiple-columns-using-entity-framework) you can do it creating classes for that multi-selects, but I've tried without success. To not running against the clock I prefer to leave it as is.
