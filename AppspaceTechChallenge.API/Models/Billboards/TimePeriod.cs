using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppspaceTechChallenge.API.Models.Billboards
{
    /// <summary>
    /// Specifies a time period to build (week by week) billboards.
    /// </summary>
    public class TimePeriod
    {
        /// <inheritdoc />
        public const int WeeekDefinition = 7;

        /// <summary>
        /// Start date for build billboards. If not provided it will be today.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// End date for build billboards. 
        /// </summary>
        [BindRequired]
        public DateTime EndDate { get; set; }

        /// <inheritdoc />
        public TimePeriod() { }

        /// <inheritdoc />
        public TimePeriod(DateTime? startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Validates if data provided requires the requirements.
        /// </summary>
        public void Validate()
        {
            if(!StartDate.HasValue) StartDate = DateTime.Now.Date;
            if (StartDate.Value > EndDate) throw new ArgumentException("End date cannot be earlier than the start date.");
            if((EndDate-StartDate.Value).Days < WeeekDefinition) throw new ArgumentException("The time between dates must be greater than one week.");
        }

        /// <summary>
        /// Get total weeks needed to fullfill with billboards.
        /// </summary>
        public int GetWeeks()
        {
            Validate();
            return (EndDate - StartDate.Value).Days / WeeekDefinition;
        }
    }
}