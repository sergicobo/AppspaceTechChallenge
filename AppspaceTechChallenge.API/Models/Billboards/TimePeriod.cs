using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppspaceTechChallenge.API.Models.Billboards
{
    public class TimePeriod
    {
        public const int WeeekDefinition = 7;

        public DateTime? StartDate { get; set; }
        [BindRequired]
        public DateTime EndDate { get; set; }

        public void Validate()
        {
            if(!StartDate.HasValue) StartDate = DateTime.Now.Date;
            if (StartDate.Value > EndDate) throw new ArgumentException("End date cannot be earlier than the start date.");
            if((EndDate-StartDate.Value).Days < WeeekDefinition) throw new ArgumentException("The time between dates must be greater than one week.");
        }

        public int GetWeeks()
        {
            Validate();
            return (EndDate - StartDate.Value).Days / WeeekDefinition;
        }
    }
}