using System;

namespace ParkingApp
{
    public class ParkingSummary
    {
        public DateTime TimeEntered { get; set; }

        public DateTime TimeLeft { get; set; }

        public string OccupantLicensePlace { get; set; }

        public int HoursParkedRounded { get; set; }

        public double HoursParkedRaw { get; set; }

        public string SpotId { get; set; }

        public int Cost { get; set; }

        public ParkingSummary(Spot spot, int priceFirstHour, int priceSecondHour)
        {
            SpotId = spot.Id;
            OccupantLicensePlace = spot.OccupiedBy;
            TimeEntered = spot.TimeEntered;
            TimeLeft = spot.TimeLeft;

            var hours = (TimeLeft - TimeEntered).TotalSeconds; //to show data faster, simulated seconds as hours
            HoursParkedRaw = hours;
            HoursParkedRounded = (int)Math.Ceiling(hours);

            Cost = priceFirstHour;
            if (HoursParkedRounded > 1)
            {
                Cost += (HoursParkedRounded - 1) * priceSecondHour;
            }
        }

        public override string ToString()
        {
            return $"[{TimeLeft}] Car [{OccupantLicensePlace}] left spot [{SpotId}].\n" +
                $"This visit was for [{HoursParkedRaw}] hours, rounded to [{HoursParkedRounded}]. Cost for this stay is {Cost}. Enjoy your ride.";
        }
    }
}
