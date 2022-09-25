using AppspaceTechChallenge.API.Models.Billboards;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace AppspaceTechChallenge.API.Tests.Models.Billboards
{
	public class IntelligentBillboardRequestShould
	{
		[Theory, AutoData]
		public void Get_Intelligent_Billboard_Request_With_Expected_Data(TimePeriod timePeriod, int bigRooms, int smallRooms, string city)
		{
			var billboardRequest = new IntelligentBillboardRequest(timePeriod, bigRooms, smallRooms, city);

			billboardRequest.TimePeriod.StartDate.Should().Be(timePeriod.StartDate);
			billboardRequest.TimePeriod.EndDate.Should().Be(timePeriod.EndDate);
			billboardRequest.BigRooms.Should().Be(bigRooms);
			billboardRequest.SmallRooms.Should().Be(smallRooms);
			billboardRequest.City.Should().Be(city);
        }
        [Theory, AutoData]
        public void Get_Intelligent_Billboard_Request_With_Expected_Data_On_Empty_Constructor(TimePeriod timePeriod, int bigRooms, int smallRooms, string city)
        {
            var billboardRequest = new IntelligentBillboardRequest
            {
                TimePeriod = timePeriod, 
                BigRooms = bigRooms, 
                SmallRooms = smallRooms, 
                City = city
            };

            billboardRequest.TimePeriod.StartDate.Should().Be(timePeriod.StartDate);
            billboardRequest.TimePeriod.EndDate.Should().Be(timePeriod.EndDate);
            billboardRequest.BigRooms.Should().Be(bigRooms);
            billboardRequest.SmallRooms.Should().Be(smallRooms);
            billboardRequest.City.Should().Be(city);
        }
    }
}
