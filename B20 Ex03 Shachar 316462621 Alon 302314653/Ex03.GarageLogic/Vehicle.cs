using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    abstract class Vehicle
    {
        internal class Wheel
        {
            private readonly string r_ManufacturerName;
            private readonly float r_MaxAirPressure;
            private float m_CurrentAirPressure;

            internal Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
            {
                if (i_CurrentAirPressure > i_MaxAirPressure || i_CurrentAirPressure < 0)
                {
                    throw new ValueOutOfRangeException(i_MaxAirPressure, 0);
                }

                this.r_ManufacturerName = i_ManufacturerName;
                this.r_MaxAirPressure = i_MaxAirPressure;
                this.m_CurrentAirPressure = i_CurrentAirPressure;
            }

            /*
             * -1 is to fill all the way
             * Throws ValueOutOfRangeException if value is out of range
            */
            internal void FillAir(float i_AmountOfAirToFill)
            {
                if (i_AmountOfAirToFill == -1)
                {
                    this.m_CurrentAirPressure = this.r_MaxAirPressure;
                }
                else if (this.r_MaxAirPressure < this.m_CurrentAirPressure + i_AmountOfAirToFill || i_AmountOfAirToFill < 0)
                {
                    throw new ValueOutOfRangeException(0, r_MaxAirPressure);
                }
                else
                {
                    this.m_CurrentAirPressure = this.m_CurrentAirPressure + i_AmountOfAirToFill;
                }
            }

            internal string ToString()
            {
                string wheelDetails = "        Max air pressure: " + this.r_MaxAirPressure + System.Environment.NewLine + "        Cuurent air pressure: " + this.m_CurrentAirPressure + System.Environment.NewLine + "        Manifacturer: " + this.r_ManufacturerName + System.Environment.NewLine;

                return wheelDetails;
            }
        }

        protected EnergySource m_EnergySource;
        protected eState m_State;
        protected Owner m_Owner;
        protected Wheel[] m_Wheels;
        protected readonly bool r_IsElectric;
        protected readonly List<string> r_ListOfSpecialFeatures;
        protected readonly string r_ModelName;
        protected readonly string r_LicensePlateNumber;
        protected readonly eFuelType r_FuelType;
        protected readonly float r_MaxPressure;

        internal Vehicle(string i_ModelName, bool i_IsElectric, string i_LicensePlateNumber, Owner i_Owner, int i_NumberOfWheels, float i_MaxPressure, eFuelType i_FuelType)
        {
            this.r_ModelName = i_ModelName;
            this.r_IsElectric = i_IsElectric;
            this.r_LicensePlateNumber = i_LicensePlateNumber;
            this.m_Owner = i_Owner;
            this.m_State = eState.Fixing;
            this.m_Wheels = new Wheel[i_NumberOfWheels];
            this.r_ListOfSpecialFeatures = new List<string>();
            this.r_MaxPressure = i_MaxPressure;
            if (!r_IsElectric)
            {
                this.r_FuelType = i_FuelType; 
            }
        }

        internal int NumberOfWheels
        {
            get
            {
                return this.m_Wheels.Length;
            }
        }

        internal bool IsElectric
        {
            get
            {
                return this.r_IsElectric;
            }
        }

        internal eState State
        {
            get
            {
                return this.m_State;
            }
            set
            {
                this.m_State = value;
            }
        }

        internal string LicensePlateNumber
        {
            get
            {
                return this.r_LicensePlateNumber;
            }
        }

        internal Wheel[] Wheels
        {
            get
            {
                return this.m_Wheels;
            }
            set
            {
                this.m_Wheels = value;
            }
        }

        internal abstract void SetVehicleSpecialFeatures(Dictionary<string, string> i_SpecialFeaturesDictionary);

        internal abstract void SetEnergy(float i_CurrentEnergyInEnergySource);

        /*
         *Builds a dictionary with all the necessary values(as keys) for the specific requested kind of vehicle
         * Initializes the values to be empty
         */
        internal Dictionary<string, string> GetSpecialFeatursDictionary()
        {
            Dictionary<string, string> specialFeaturesDictionary = new Dictionary<string, string>();
            
            foreach (string specialFeature in this.r_ListOfSpecialFeatures)
            {
                specialFeaturesDictionary.Add(specialFeature, "");
            }

            return specialFeaturesDictionary;
        }

        /*
         * Sets all the wheels of said vehicle
         */
        internal void SetWheels(string i_Manufacturer, float i_AirPressure)
        {
            for(int i = 0; i < this.m_Wheels.Length; i++)
            {
                this.m_Wheels[i] = new Wheel(i_Manufacturer, i_AirPressure, this.r_MaxPressure);
            }
        }

        /*
         * Charges electric vehicles
         */
        internal void FillEnergySource(float i_AmountToFill)
        {
            (this.m_EnergySource as Battery).Charge(i_AmountToFill);
        }

        /*
         * Fuels Non-electric vehicles
         */
        internal void FillEnergySource(float i_AmountToFill, eFuelType i_FuelType)
        {
            (this.m_EnergySource as FuelTank).Charge(i_AmountToFill, i_FuelType);
        }

        /*
         * Fills air in all of the vehicles wheels.
         * Gets an array of the amount of air to fill in each wheel coresponding to the wheels 
         * order in the vehicles wheels array
         */
        internal void PumpWheels(float[] i_AmountOfAirToFillInWheels)
        {
            for (int i = 0; i < this.m_Wheels.Length; i++)
            {
                this.m_Wheels[i].FillAir(i_AmountOfAirToFillInWheels[i]);
            }
        }

        /*
         * Builds a string with all of the vehicle's details
         */
        internal virtual string ToString()
        {
            StringBuilder vehicleStringValue = new StringBuilder();

            vehicleStringValue.Append("License plate number: " + this.LicensePlateNumber + System.Environment.NewLine);
            vehicleStringValue.Append("Vehicle model: " + this.r_ModelName + System.Environment.NewLine);
            vehicleStringValue.Append("Owner's name: " + this.m_Owner.Name + System.Environment.NewLine);
            vehicleStringValue.Append("Owner's phone number: " + this.m_Owner.PhoneNumber + System.Environment.NewLine);
            vehicleStringValue.Append("State: " + this.State + System.Environment.NewLine);
            vehicleStringValue.Append("Wheels: " + System.Environment.NewLine);
            vehicleStringValue.Append(wheelsDetailsToString());
            vehicleStringValue.Append(this.m_EnergySource.ToString());

            return vehicleStringValue.ToString();
        }

        /*
         * Builds a string with all the details of all the wheels the vehicle has
         */
        private string wheelsDetailsToString()
        {
            StringBuilder wheelsDetails = new StringBuilder();
            int i = 1;

            foreach (Wheel wheel in this.m_Wheels)
            {
                wheelsDetails.Append("    Wheel " + i + ": " + System.Environment.NewLine);
                wheelsDetails.Append(wheel.ToString());
                i++;
            }

            return wheelsDetails.ToString();
        }
    }
}