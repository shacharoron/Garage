using System;

namespace Ex03.GarageLogic
{
    /*
    The excption thrown when a vehicle that's not in the garage is trying to be reached
    */
    public class VehicleNotInGarageException : Exception
    {
        public VehicleNotInGarageException(){}
    }
}

