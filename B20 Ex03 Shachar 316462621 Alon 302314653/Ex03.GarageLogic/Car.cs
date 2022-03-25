using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private eColor m_Color;
        private eNumberOfDoors m_NumberOfDoors;
        private const int k_MaxPressure = 32;
        private const int k_NumberOfWheels = 4;
        private const eFuelType k_FuelType = eFuelType.Octan96;
        private const float k_MaxCapacityOfFuelInLiters = 60f;
        private const float k_MaxCapacityOfBatteryInHours = 1.2f;
        private const float k_Zero = 0;
        private const string k_SpecialFeature1 = "Color";
        private const string k_SpecialFeature2 = "Number of doors";

        internal Car(string i_ModelName, bool i_IsElectric, string i_LicensePlateNumber, Owner i_Owner) : base(i_ModelName, i_IsElectric,
            i_LicensePlateNumber, i_Owner, k_NumberOfWheels, k_MaxPressure, k_FuelType)
        {
          this.r_ListOfSpecialFeatures.Add(k_SpecialFeature1);
          this.r_ListOfSpecialFeatures.Add(k_SpecialFeature2);
        }

        /*
        * Sets all the special features a car has. 
        */
        internal override void SetVehicleSpecialFeatures(Dictionary<string, string> i_SpecialFeaturesDictionary)
        {
            string color;
            string numberOfDoors;

            i_SpecialFeaturesDictionary.TryGetValue(k_SpecialFeature1, out color);
            i_SpecialFeaturesDictionary.TryGetValue(k_SpecialFeature2, out numberOfDoors);
            setColor(color);
            setNumberOfDoors(numberOfDoors);
        }

        private void setColor(string i_Color)
        {
            if (!eColor.TryParse(i_Color, out m_Color) ||!Enum.IsDefined(typeof(eColor), i_Color))
            {
                throw new ArgumentException(k_SpecialFeature1 + ":your options are Red, Silver, Black or White");
            }
        }

        private void setNumberOfDoors(string i_NumberOfDoors)
        {
            int numberOfDoors;

            if (!int.TryParse(i_NumberOfDoors, out numberOfDoors) || !Enum.IsDefined(typeof(eNumberOfDoors), numberOfDoors))
            {
                throw new ArgumentException(k_SpecialFeature2 + ":your options are 2, 3, 4, 5");
            }
            else
            {
                eNumberOfDoors.TryParse(i_NumberOfDoors, out this.m_NumberOfDoors);
            }
        }

        /*
         * Sets a battery for an electric car and fuel tank for a non electric car
         */
        internal override void SetEnergy(float i_CurrentEnergyInEnergySource)
        {
            if (this.IsElectric)
            {
                this.m_EnergySource = new Battery(k_MaxCapacityOfBatteryInHours, i_CurrentEnergyInEnergySource);
            }
            else
            {
                this.m_EnergySource = new FuelTank(k_MaxCapacityOfFuelInLiters, i_CurrentEnergyInEnergySource,k_FuelType);
            }
        }
        internal override string ToString()
        {
            StringBuilder carDetails = new StringBuilder();

            carDetails.Append(base.ToString());
            carDetails.Append(string.Format("Color: {0}",this.m_Color) + System.Environment.NewLine);
            carDetails.Append(string.Format("Number of doors: {0}", this.m_NumberOfDoors) + System.Environment.NewLine);

            return carDetails.ToString();
        }
    }
}