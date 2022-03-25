using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private float m_CargoVolume;
        private const int k_NumberOfWheels = 16;
        private const int k_TankSize = 120;
        private const int k_MaxPressure = 28;
        private const eFuelType k_FuelType = eFuelType.Solar;
        private const string k_SpecialFeature1 = "cargo volume";
        private const float k_MaxCapacityOfFuelInLiters = 120;

        internal Truck(string i_ModelName, bool i_IsElectric, string i_LicensePlateNumber, Owner i_Owner) : base(i_ModelName, i_IsElectric,
            i_LicensePlateNumber, i_Owner, k_NumberOfWheels, k_MaxPressure, k_FuelType)
        {
            this.r_ListOfSpecialFeatures.Add(k_SpecialFeature1);
        }

        /*
         * Sets all the special features a truck has. 
         */
        internal override void SetVehicleSpecialFeatures(Dictionary<string, string> i_SpecialFeaturesDictionary)
        {
            string cargoVolumeOfTruck;

            i_SpecialFeaturesDictionary.TryGetValue(k_SpecialFeature1, out cargoVolumeOfTruck);
            setCurrentCargoVolume(cargoVolumeOfTruck);
        }

        /*
         * Sets the current volume of cargo the truck has.
         */
        private void setCurrentCargoVolume(string i_CargoVolume)
        {
            float cargoVolumeOfTruck;

            if (float.TryParse(i_CargoVolume, out cargoVolumeOfTruck) && cargoVolumeOfTruck >= 0)
            {
                this.m_CargoVolume = cargoVolumeOfTruck;
            }
            else
            {
                throw new ArgumentException(k_SpecialFeature1 + ":We only accept non-negative numbers as cargo volume");
            }
        }

        /*
         *  Sets the truck's fuel tank
         */
        internal override void SetEnergy(float i_CurrentEnergyInEnergySource)
        {
            this.m_EnergySource = new FuelTank(k_MaxCapacityOfFuelInLiters, i_CurrentEnergyInEnergySource,k_FuelType);

        }

        internal override string ToString()
        {
            StringBuilder truckDetails = new StringBuilder();

            truckDetails.Append(base.ToString());
            truckDetails.Append(string.Format("Cargo Volume: {0}", this.m_CargoVolume) + System.Environment.NewLine);

            return truckDetails.ToString();
        }
    }
}