using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApp
{
    public class Car
    {
        public Car(string licensePlate)
        {
            LicensePlate = licensePlate;
        }
        public string LicensePlate { get; set; }
    }
}
