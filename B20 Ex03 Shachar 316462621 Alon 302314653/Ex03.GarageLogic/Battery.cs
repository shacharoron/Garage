using System.Text;

namespace Ex03.GarageLogic
{
    internal class Battery : EnergySource
    {
        internal Battery(float i_MaxCapacity, float i_RemainingEnergyPrecentageInHours) : base(i_MaxCapacity, i_RemainingEnergyPrecentageInHours) { }
        
        internal override void Charge(float i_HoursToCharge)
        {
            base.Charge(i_HoursToCharge);
        }

        internal override string ToString()
        {
            StringBuilder energyDetails = new StringBuilder();

            energyDetails.Append("Type: Electric." + System.Environment.NewLine);
            energyDetails.Append(base.ToString());

            return energyDetails.ToString();
        }
    }
}