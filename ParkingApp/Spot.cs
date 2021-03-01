using System;

namespace ParkingApp
{
    public class Spot
    {
        public string Id { get; set; }

        public bool Free { get; set; }

        public string OccupiedBy { get; set; }

        public DateTime TimeEntered { get; set; }

        public DateTime TimeLeft { get; set; }
    }
}
