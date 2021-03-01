using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ParkingApp
{
    public class Parking
    {
        public int PriceFirstHour { get; set; }

        public int PriceNextHours { get; set; }
        
        public Spot[] Spots { get; set; }

        public Parking(int totalSpots = 10, int priceFirstHour = 10, int priceNextHours = 5)
        {
            Spots = new Spot[totalSpots];
            for (int i = 0; i < totalSpots; i++)
            {
                Spots[i] = new Spot
                {
                    Id = $"S{i + 1}",
                    Free = true
                };
            }
            PriceFirstHour = priceFirstHour;
            PriceNextHours = priceNextHours;
        }

        public int GetFreeSpots() 
        {
            return this.Spots.Count(s => s.Free);
        }

        public List<string> GetParkedCars() 
        {
            var occupiedSpots = this.Spots.Where(s => s.Free == false);

            return occupiedSpots.Select(c => c.OccupiedBy).ToList();
        }

        public void Park(Car car) 
        {
            if(GetFreeSpots() > 0) 
            {
                var firstFreeSpot = this.Spots.First(s => s.Free);
                firstFreeSpot.Free = false;
                firstFreeSpot.OccupiedBy = car.LicensePlate;
                firstFreeSpot.TimeEntered = DateTime.Now;

                string message = $"[{firstFreeSpot.TimeEntered}] spot [{firstFreeSpot.Id}] > parked Car with License Plate [{car.LicensePlate}]";

                Console.WriteLine(message);
            }
            else 
            {
                string message = $"Sorry, {car.LicensePlate}. There are no more free spots.\n";
                throw new NoAvailableSpotsException(message);
            }
        }

        public ParkingSummary Leave(Car car) 
        {
            var spot = this.Spots.First(s => !string.IsNullOrEmpty(s.OccupiedBy) && s.OccupiedBy.Equals(car.LicensePlate));
            
            spot.TimeLeft = DateTime.Now;

            var summary = new ParkingSummary(spot, PriceFirstHour, PriceNextHours);

            spot.Free = true;
            spot.OccupiedBy = null;

            return summary;
        }

        public static void WaitXHours(double h)
        {
            int hours = (int)h * 1000; //same, simulate seconds as hours, to get faster visible results
            Thread.Sleep(hours);
        }

    } 
}
