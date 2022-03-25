using System;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class FuelTank : EnergySource
    {
        private readonly eFuelType r_FuelType;

        internal FuelTank(float i_MaxCapacity, float i_RemainingEnergyInLiters, eFuelType i_FuelType) : base(i_MaxCapacity, i_RemainingEnergyInLiters)
        {
            this.r_FuelType = i_FuelType;
        }

        internal eFuelType FuelType
        {
            get
            {
                return this.r_FuelType;
            }
        }
        
        internal void Charge(float i_Liters, eFuelType i_FuelType)
        {
            if (r_FuelType.Equals(i_FuelType))
            {
                base.Charge(i_Liters);
            }
            else
            {
                throw new ArgumentException(i_FuelType.ToString());
            }
        }

        internal override string ToString()
        {
            StringBuilder energyDetails = new StringBuilder();

            energyDetails.Append("Type: " + this.FuelType + System.Environment.NewLine);
            energyDetails.Append(base.ToString());

            return energyDetails.ToString();
        }
    }
}