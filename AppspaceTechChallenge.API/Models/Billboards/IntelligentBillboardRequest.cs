namespace AppspaceTechChallenge.API.Models.Billboards
{
    /// <summary>
    /// Request for a new billboard for specific time period, number of big screens and number of small screens. Can be specified a city for a filter.
    /// </summary>
    public class IntelligentBillboardRequest
    {
        /// <summary>
        /// Time period to build billboards week by week. If no start date are provided, start date will be today.
        /// </summary>
        public TimePeriod TimePeriod { get; set; }

        /// <summary>
        /// Number of big rooms needed.
        /// </summary>
        public int BigRooms { get; set; }

        /// <summary>
        /// Number of small rooms needed.
        /// </summary>
        public int SmallRooms { get; set; }

        /// <summary>
        /// City name to build intelligent billboard based on movies successfully on it.
        /// </summary>
        public string City { get; set; }

        /// <inheritdoc/>
        public IntelligentBillboardRequest() { }

        /// <inheritdoc/>
        public IntelligentBillboardRequest(TimePeriod timePeriod, int bigRooms, int smallRooms, string city)
        {
            TimePeriod = timePeriod; 
            BigRooms = bigRooms; 
            SmallRooms = smallRooms;
            City = city;
        }
    }
}
