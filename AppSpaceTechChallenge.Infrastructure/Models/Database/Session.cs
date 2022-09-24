﻿using System;

namespace AppspaceTechChallenge.Infrastructure.Models.Database
{
    public class Session
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int MovieId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? SeatsSold { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Room Room { get; set; }
    }
}
