using System;
using AppspaceTechChallenge.API.Models.Billboards;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace AppspaceTechChallenge.API.Tests.Models.Billboards
{
	public class TimePeriodShould
	{
		[Theory, AutoData]
		public void Return_ArgumentException_When_End_Date_Are_Earlier_Than_Start_Date_When_Validation_Are_Done(DateTime date)
		{
			var action = new Action(() =>
			{
				var period = new TimePeriod(date.AddDays(1), date);
				period.Validate();
			});

			action.Should().Throw<ArgumentException>().WithMessage("End date cannot be earlier than the start date.");
		}

		[Theory]
		[InlineAutoData(0)]
		[InlineAutoData(1)]
		[InlineAutoData(2)]
		[InlineAutoData(3)]
		[InlineAutoData(4)]
		[InlineAutoData(5)]
		[InlineAutoData(6)]
		public void Return_ArgumentException_When_Time_Difference_Are_Less_Than_One_Week_When_Validation_Are_Done(int timeDifference, DateTime date)
		{
			var action = new Action(() =>
			{
				var period = new TimePeriod(date, date.AddDays(timeDifference));
				period.Validate();
			});

			action.Should().Throw<ArgumentException>().WithMessage("The time between dates must be greater than one week.");
		}

		[Theory]
		[InlineData("01/01/2022", "08/01/2022", 1)]
		[InlineData("01/01/2022", "15/01/2022", 2)]
		[InlineData("01/01/2022", "22/01/2022", 3)]
		[InlineData("01/01/2022", "29/01/2022", 4)]
		[InlineData("01/01/2022", "05/02/2022", 5)]
		public void Return_An_Appropriate_Number_Of_Weeks(string startDateAsString, string endDateAsString, int expectedWeeks)
        {
            var startDate = Convert.ToDateTime(startDateAsString);
            var endDate = Convert.ToDateTime(endDateAsString);
			
			var period = new TimePeriod(startDate, endDate);
            var weeks = period.GetWeeks();

            weeks.Should().Be(expectedWeeks);
        }

		[Fact]
		public void Get_Time_Period_With_Start_Date_Set_To_Today_If_Not_Provided_When_Validation_Are_Done()
		{
			var weekDefinitionInDays = 7;
			var validEndDate = DateTime.Now.Date.AddDays(weekDefinitionInDays);

			var period = new TimePeriod(null, validEndDate);
			period.Validate();

			period.StartDate.Should().Be(DateTime.Now.Date);
			period.EndDate.Should().Be(validEndDate);
		}
	}
}
