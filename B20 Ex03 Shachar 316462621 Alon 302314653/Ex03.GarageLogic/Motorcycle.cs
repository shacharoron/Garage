using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private float m_EngineVolumeInCC;
        private const int k_NumberOfWheels = 2;
        private const int k_MaxPressure = 30;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const string k_SpecialFeature1 = "license type";
        private const string k_SpecialFeature2 = "engine volume";
        private const float k_MaxCapacityOfBatteryInHours = 1.2f;
        private const float k_MaxCapacityOfFuelInLiters = 7.0f;

        internal Motorcycle(string i_ModelName, bool i_IsElectric, string i_LicensePlateNumber, Owner i_Owner) : base(i_ModelName, i_IsElectric,
            i_LicensePlateNumber, i_Owner, k_NumberOfWheels,k_MaxPressure, k_FuelType)
        {
            this.r_ListOfSpecialFeatures.Add(k_SpecialFeature1);
            this.r_ListOfSpecialFeatures.Add(k_SpecialFeature2);
        }

        /*
         * Sets all the special features a motorcycle has. 
         */
        internal override void SetVehicleSpecialFeatures(Dictionary<string, string> i_SpecialFeaturesDictionary)
        {
            string licenseType;
            string engineVolume;

            i_SpecialFeaturesDictionary.TryGetValue(k_SpecialFeature1, out licenseType);
            i_SpecialFeaturesDictionary.TryGetValue(k_SpecialFeature2, out engineVolume);
            setLicenseType(licenseType);
            setEngineVolumeInCC(engineVolume);
        }

        private void setLicenseType(string i_LicenseType)
        {
            if (!eLicenseType.TryParse(i_LicenseType, out this.m_LicenseType) || !Enum.IsDefined(typeof(eLicenseType), i_LicenseType))
            {
                throw new System.ArgumentException(k_SpecialFeature1 + ":your options are AA, B, A1, and A");
            }

        }

        private void setEngineVolumeInCC(string i_EngineVolumeInCC)
        { 
            if (!float.TryParse(i_EngineVolumeInCC, out m_EngineVolumeInCC) || m_EngineVolumeInCC < 0)
            {
                throw new System.ArgumentException(k_SpecialFeature2 + ": We only accept non-negative numbers as an engine volume...");
            }
        }

        /*
         * Sets a battery for an electric motorcycle and fuel tank for a non electric motorcycle
         */
        internal override void SetEnergy(float i_CurrentEnergyInEnergySource)
        {
            if (this.IsElectric)
            {
                this.m_EnergySource = new Battery(k_MaxCapacityOfBatteryInHours, i_CurrentEnergyInEnergySource);
            }
            else
            {
                this.m_EnergySource = new FuelTank(k_MaxCapacityOfFuelInLiters, i_CurrentEnergyInEnergySource, k_FuelType);
            }
        }

        internal override string ToString()
        {
            StringBuilder motorcycleDetails = new StringBuilder();

            motorcycleDetails.Append(base.ToString());
            motorcycleDetails.Append(string.Format("License type: {0}", this.m_LicenseType) + System.Environment.NewLine);
            motorcycleDetails.Append(string.Format("Engine volume in CC: {0}", this.m_EngineVolumeInCC) + System.Environment.NewLine);

            return motorcycleDetails.ToString();
        }
    }
}