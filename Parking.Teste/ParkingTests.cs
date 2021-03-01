using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingApp;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ParkingTests
{
    [TestClass]
    public class ParkingTests
    {
        [TestMethod]
        public void EmptyParking_WhenAskedHowManySpots_ShouldReturnAllSpots()
        {
            int totalspots = 10;
            Parking parking = new Parking(totalspots);

            Assert.AreEqual(parking.GetFreeSpots(), totalspots);
        }

        [TestMethod]
        public void EmptyParking_WhenAskedForParkedCars_ShouldReturnEmptyList()
        {
            int totalspots = 10;
            Parking parking = new Parking(totalspots);

            Assert.AreEqual(parking.GetParkedCars().Count, 0);
        }

        [TestMethod]
        public void OneSpotOccupied_WhenAskedForFreeSpots_ShouldReturnTotalSpotsMinusOne()
        {
            int totalspots = 10;
            Parking parking = new Parking(totalspots);
            Car car1 = new Car("BC-12-QWR");
            parking.Park(car1);

            Assert.AreEqual(parking.GetFreeSpots(), totalspots - 1);
        }

        [TestMethod]
        public void OneSpotOccupied_WhenAskedForParkedCars_ShouldReturnOneResult() 
        {
            int totalspots = 10;
            Parking parking = new Parking(totalspots);
            Car car1 = new Car("BC-12-AWR");
            parking.Park(car1);

            Assert.AreEqual(parking.GetParkedCars().Count, 1);
        }

        [TestMethod]
        public void OneSpotOccupied_WhenAskedForParkedCars_ShouldReturnOneCarsLicencePlate()
        {
            int totalspots = 10;
            string licensePlate = "BC-12-CCR";
            Parking parking = new Parking(totalspots);
            Car car1 = new Car(licensePlate);
            parking.Park(car1);

            Assert.AreEqual(parking.GetParkedCars()[0], licensePlate);
        }

        [TestMethod]
        public void XSpotsOccupied_WhenAskedForParkedCars_ShouldReturnXResults()
        {
            int totalspots = 10;
            Parking parking = new Parking(totalspots);
            List<Car> cars = new List<Car>
            {
                new Car("BC-12-CCR"),
                new Car("B-012-AAR"),
                new Car("BZ-08-XXE")
            };
            foreach (var car in cars)
            {
                parking.Park(car);
            }
            Assert.AreEqual(parking.GetParkedCars().Count, cars.Count);
        }

        [TestMethod]
        public void XSpotsOccupied_WhenAskedForFreeSpots_ShouldReturnTotalMinusXResults()
        {
            int totalspots = 10;
            Parking parking = new Parking(totalspots);
            List<Car> cars = new List<Car>
            {
                new Car("BC-12-CCR"),
                new Car("B-012-AAR"),
                new Car("BZ-08-XXE")
            };
            foreach (var car in cars)
            {
                parking.Park(car);
            }

            Assert.AreEqual(parking.GetFreeSpots(), totalspots - cars.Count);
        }

        [TestMethod]
        public void FullParking_WhenAskedForFreeSpots_ShouldReturnZero()
        {
            int totalspots = 4;
            Parking parking = new Parking(totalspots);
            List<Car> cars = new List<Car>
            {
                new Car("BC-12-CCR"),
                new Car("B-012-AAR"),
                new Car("BZ-08-XXE"),
                new Car("AB-12-AAS")
            };
            foreach (var car in cars)
            {
                parking.Park(car);
            }

            Assert.AreEqual(parking.GetFreeSpots(), 0);
        }

        [TestMethod]
        public void FullParking_WhenAskedForParkedCars_ShouldReturnTotalResults()
        {
            int totalspots = 3;
            Parking parking = new Parking(totalspots);
            List<Car> cars = new List<Car>
            {
                new Car("BC-12-CCR"),
                new Car("B-012-AAR"),
                new Car("BZ-08-XXE")
            };
            foreach (var car in cars)
            {
                parking.Park(car);
            }

            Assert.AreEqual(parking.GetParkedCars().Count, totalspots);
        }

        [TestMethod]
        public void FullParking_WhenAskedForParkedCars_ShouldReturnAllPlates()
        {
            int totalspots = 3;
            Parking parking = new Parking(totalspots);
            List<Car> cars = new List<Car>
            {
                new Car("BC-12-CCR"),
                new Car("B-012-AAR"),
                new Car("BZ-08-XXE")
            };
            foreach (var car in cars)
            {
                parking.Park(car);
            }

            var parkedCars = parking.GetParkedCars();

            Assert.IsTrue(parkedCars.Contains(cars[0].LicensePlate) && parkedCars.Contains(cars[1].LicensePlate) &&
                parkedCars.Contains(cars[2].LicensePlate));
        }

        [TestMethod]
        public void FullParking_WhenNewCarTriesToPark_ShouldThrowException()
        {
            int totalspots = 3;
            Parking parking = new Parking(totalspots);
            Car car1 = new Car("BC-02-EML");
            Car car2 = new Car("BZ-20-ASL");
            Car car3 = new Car("B-012-ABC");
            Car car4 = new Car("AB-08-CRT");
            
            parking.Park(car1);
            parking.Park(car2);
            parking.Park(car3);

            Assert.ThrowsException<NoAvailableSpotsException>(() => parking.Park(car4));

           
        }
    }

    [TestClass]
    public class ParkingTestsCosts 
    {
        [TestMethod]
        public void Park1CarFor1Hour_ShouldCostHourlyPrice()
        {
            int totalspots = 10;
            int firstHour = 10;
            int secondHourAndMore = 5;
            Parking parking = new Parking(totalspots, firstHour, secondHourAndMore);
            Car car1 = new Car("BC-24-XYZ");
            parking.Park(car1);
            Parking.WaitXHours(0.5);
            
            var summary = parking.Leave(car1);


            Assert.AreEqual(summary.Cost, firstHour);
        }

        [TestMethod]
        public void Park1CarFor1HourAndAFraction_ShouldCost2HoursPrice()
        {
            int totalspots = 10;
            int firstHour = 10;
            int secondHourAndMore = 5;
            Parking parking = new Parking(totalspots, firstHour, secondHourAndMore);
            Car car1 = new Car("BC-24-XYZ");
            parking.Park(car1);
            Parking.WaitXHours(1.1);

            var summary = parking.Leave(car1);


            Assert.AreEqual(summary.Cost, firstHour + secondHourAndMore);
        }

        [TestMethod]
        public void Park1Car2HoursAndAFraction_ShouldCost3HoursPrice()
        {
            int totalspots = 10;
            int firstHour = 10;
            int secondHourAndMore = 5;
            Parking parking = new Parking(totalspots, firstHour, secondHourAndMore);
            Car car1 = new Car("BC-24-XYZ");
            parking.Park(car1);
            Parking.WaitXHours(2.1);

            var summary = parking.Leave(car1);


            Assert.AreEqual(summary.Cost, firstHour + 2 * secondHourAndMore);
        }

        [DataRow(10, 5, 3.1, 25)]
        [DataRow(10, 5, 1.8, 15)]
        [DataTestMethod]
        public void Park1CarNHours_ShouldCostNHoursPrice(int firstHour, int secondHourAndMore, double nrHoursParked, double expectedPrice)
        {
            int totalspots = 10;
            Parking parking = new Parking(totalspots, firstHour, secondHourAndMore);
            Car car1 = new Car("BC-24-XYZ");
            parking.Park(car1);
            Parking.WaitXHours(nrHoursParked);

            var summary = parking.Leave(car1);


            Assert.AreEqual(summary.Cost, expectedPrice);
        }

        [DataRow(10, 5, 2, 10)]
        [DataTestMethod]
        public void ParkXCarsForFractionOfHour_ShouldCostSameHourlyRatePerCar(int firstHour, int secondHourAndMore, int nrCarsToPark, int expectedCost)
        {
            int totalspots = 10;
            Parking parking = new Parking(totalspots, firstHour, secondHourAndMore);
            List<Car> cars = new List<Car>();
            for (int i = 0; i < nrCarsToPark; i++)
            {
                cars.Add(new Car("XX-XX-XXX"));
            }
            foreach (var car in cars) 
            {
                parking.Park(car);
            }
            
            Parking.WaitXHours(0.8);

            int[] costs = new int[nrCarsToPark];
            for (int i = 0; i < nrCarsToPark; i++)
            {
                var summary = parking.Leave(cars[i]);
                costs[i] = summary.Cost;
            }

            bool samePrice = true;
            foreach (var cost in costs)
            {
                if (cost != expectedCost)
                {
                    samePrice = false;
                }
            }
            Assert.IsTrue(samePrice && expectedCost == costs[0]);
        }

        [DataRow(10, 5, 2, 2.9, 20)]
        [DataTestMethod]
        public void ParkXCarsForYHours_ShouldCostSameCalculatedRatePerCar(int firstHour, int secondHourAndMore, int nrCarsToPark, double nrHoursToPark, int expectedCost)
        {
            int totalspots = 10;
            Parking parking = new Parking(totalspots, firstHour, secondHourAndMore);
            List<Car> cars = new List<Car>();
            for (int i = 0; i < nrCarsToPark; i++)
            {
                cars.Add(new Car("XX-XX-XXX"));
            }
            foreach (var car in cars)
            {
                parking.Park(car);
            }

            Parking.WaitXHours(nrHoursToPark);

            int[] costs = new int[nrCarsToPark];
            for (int i = 0; i < nrCarsToPark; i++)
            {
                var summary = parking.Leave(cars[i]);
                costs[i] = summary.Cost;
            }

            bool samePrice = true;
            foreach (var cost in costs)
            {
                if (cost != expectedCost)
                {
                    samePrice = false;
                }
            }
            Assert.IsTrue(samePrice && expectedCost == costs[0]);
        }
    }
}
