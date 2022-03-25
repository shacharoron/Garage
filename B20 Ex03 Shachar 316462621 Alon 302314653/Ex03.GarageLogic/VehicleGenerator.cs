namespace Ex03.GarageLogic
{
    static class VehicleGenerator
    {
        /*
         * Creates a new vehicle based on the vehicle type
         */
        internal static Vehicle AddVehicle(int i_VehicleType, string i_ModelName, bool i_IsElectric, string i_LicensePlateNumber, Owner i_Owner)
        {
            Vehicle newVehicle = null;

            switch((eVehicleType)(i_VehicleType))
            {
                case eVehicleType.Car:
                    newVehicle = new Car(i_ModelName, i_IsElectric, i_LicensePlateNumber,i_Owner);
                    break;
                case eVehicleType.Motorcycle:
                    newVehicle = new Motorcycle(i_ModelName, i_IsElectric, i_LicensePlateNumber,i_Owner);
                    break;
                case eVehicleType.Truck:
                    newVehicle = new Truck(i_ModelName, i_IsElectric, i_LicensePlateNumber,i_Owner);
                    break;
            }

            return newVehicle;
        }
    }
}