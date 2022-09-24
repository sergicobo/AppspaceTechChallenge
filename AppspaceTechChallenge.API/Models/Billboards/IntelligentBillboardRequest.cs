namespace AppspaceTechChallenge.API.Models.Billboards
{
    public class IntelligentBillboardRequest
    {
        public TimePeriod TimePeriod { get; set; }
        public int BigRooms { get; set; }
        public int SmallRooms { get; set; }
        public string City { get; set; }
        
        public IntelligentBillboardRequest()
        {
        }

        public IntelligentBillboardRequest(TimePeriod timePeriod, int bigRooms, int smallRooms, string city)
        {
            TimePeriod = timePeriod; 
            BigRooms = bigRooms; 
            SmallRooms = smallRooms;
            City = city;
        }
    }
}
