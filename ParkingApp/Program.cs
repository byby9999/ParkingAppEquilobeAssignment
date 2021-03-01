using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car("BC-02-EML");
            Car car2 = new Car("BZ-20-ASL");
            Car car3 = new Car("B-012-ABC");
            Car car4 = new Car("AB-08-CRT");
            Car car5 = new Car("CT-10-MAR");
            Car car6 = new Car("SJ-12-AWE");
            Car car7 = new Car("VN-03-CTR");
            Car car8 = new Car("SB-11-ADB");
            Car car9 = new Car("BV-12-AWX");
            Car car10 = new Car("BC-21-BBY");
            Car car11 = new Car("PH-65-WRT");
            Parking parking = new Parking();

            Console.WriteLine($"There are: { parking.GetFreeSpots() } free spots");

            Parking.WaitXHours(1);

            parking.Park(car1);

            Parking.WaitXHours(2);

            parking.Park(car2);

            Parking.WaitXHours(3);

            parking.Park(car3);

            Parking.WaitXHours(1);

            parking.Park(car4);

            Console.WriteLine($"There are { parking.GetFreeSpots() } free spots");

            Parking.WaitXHours(1);

            var parkedCars = parking.GetParkedCars();

            string message = "Following cars are parked: \n" + String.Join(",", parkedCars);

            Console.WriteLine(message);

            var summary = parking.Leave(car2);

            Console.WriteLine(summary.ToString());

            Parking.WaitXHours(3);

            summary = parking.Leave(car3);

            Console.WriteLine(summary.ToString());

            Parking.WaitXHours(1);

            summary = parking.Leave(car1);

            Console.WriteLine(summary.ToString());

            Parking.WaitXHours(2);

            parking.Leave(car4);

            Console.WriteLine($"There are { parking.GetFreeSpots() } free spots");

            parkedCars = parking.GetParkedCars();

            message = "Following cars are parked: \n" + String.Join(",", parkedCars);

            Console.WriteLine(message);

            parking.Park(car1);

            Parking.WaitXHours(2);

            parking.Park(car2);

            Parking.WaitXHours(2);

            parking.Park(car3);

            Parking.WaitXHours(0.8);

            parking.Park(car4);

            Parking.WaitXHours(0.5);

            parking.Park(car5);

            Parking.WaitXHours(0.3);

            parking.Park(car6);

            Parking.WaitXHours(1);

            parking.Park(car7);

            Parking.WaitXHours(2);

            parking.Park(car8);

            Parking.WaitXHours(2.2);

            parking.Park(car9);

            Parking.WaitXHours(3);

            parking.Park(car10);

            Parking.WaitXHours(0.5);

            Console.WriteLine($"There are { parking.GetFreeSpots() } free spots");
            try
            {
                parking.Park(car11);
            }
            catch (NoAvailableSpotsException e) 
            {
                Console.WriteLine(e.Message);
            }

            summary = parking.Leave(car1);

            Console.WriteLine(summary.ToString());

            Parking.WaitXHours(1);

            summary = parking.Leave(car2);
            Console.WriteLine(summary.ToString());

            Parking.WaitXHours(1);

            summary = parking.Leave(car4);
            Console.WriteLine(summary.ToString());

            Parking.WaitXHours(1.1);

            summary = parking.Leave(car6);
            Console.WriteLine(summary.ToString());

            Parking.WaitXHours(1.5);

            summary = parking.Leave(car7);
            Console.WriteLine(summary.ToString());

            Parking.WaitXHours(2);

            summary = parking.Leave(car5);
            Console.WriteLine(summary.ToString());

            Parking.WaitXHours(3);

            summary = parking.Leave(car10);
            Console.WriteLine(summary.ToString());

            Parking.WaitXHours(1);

            summary = parking.Leave(car8);
            Console.WriteLine(summary.ToString());

            parkedCars = parking.GetParkedCars();

            message = "Following cars are parked: \n" + String.Join(",", parkedCars);

            Console.WriteLine(message);

            summary = parking.Leave(car9);

            Console.WriteLine(summary.ToString());

            parkedCars = parking.GetParkedCars();

            message = "Following cars are parked: \n" + String.Join(",", parkedCars);

            Console.WriteLine(message);

            Console.WriteLine($"There are { parking.GetFreeSpots() } free spots");

            Console.ReadKey();
        }


        
    }
}
