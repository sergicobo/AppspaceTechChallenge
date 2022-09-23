namespace AppspaceTechChallenge.API.Models.Billboards
{
    public class IntelligentBillboardRequest
    {
        public TimePeriod TimePeriod { get; set; }
        public int BigRooms { get; set; }
        public int SmallRooms { get; set; }
        public string City { get; set; }
    }
}
