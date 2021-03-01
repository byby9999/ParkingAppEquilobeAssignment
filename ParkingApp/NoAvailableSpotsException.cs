using System;

namespace ParkingApp
{
    public class NoAvailableSpotsException : Exception
    {
        public NoAvailableSpotsException()
        {
        }

        public NoAvailableSpotsException(string message) : base(message)
        {
        }

    }
}