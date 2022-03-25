using System.Text;

namespace Ex03.GarageLogic
{
    abstract class EnergySource
    {
        private protected readonly float r_MaxCapacity;
        private protected float  m_CurrentCapacity;
        private protected float m_PrecentageFull;

        internal EnergySource(float i_MaxCapacity, float i_RemainingEnergy)
        {
            if (i_RemainingEnergy > i_MaxCapacity || i_RemainingEnergy < 0)
            {
                throw new ValueOutOfRangeException(i_MaxCapacity, 0);
            }

            this.r_MaxCapacity = i_MaxCapacity;
            this.m_CurrentCapacity = i_RemainingEnergy;
            this.m_PrecentageFull = m_CurrentCapacity / r_MaxCapacity * 100;
        }
        
        internal virtual void Charge(float i_EnergyAmuont)
        {
            if (i_EnergyAmuont + m_CurrentCapacity <= r_MaxCapacity && i_EnergyAmuont >= 0)
            {
                this.m_CurrentCapacity += i_EnergyAmuont;
                this.m_PrecentageFull = m_CurrentCapacity / r_MaxCapacity * 100;
            }
            else
            {
                throw new ValueOutOfRangeException(r_MaxCapacity - m_CurrentCapacity,0);
            }
        }

        internal virtual string ToString()
        {
            StringBuilder energyDetails = new StringBuilder();

            energyDetails.Append(string.Format("Max capacity: {0}.", this.r_MaxCapacity) + System.Environment.NewLine);
            energyDetails.Append(string.Format("Current capacity: {0}.", this.m_CurrentCapacity) + System.Environment.NewLine);
            energyDetails.Append(string.Format("Percentage: {0}.", this.m_PrecentageFull) + System.Environment.NewLine);

            return energyDetails.ToString();
        }
    }
}