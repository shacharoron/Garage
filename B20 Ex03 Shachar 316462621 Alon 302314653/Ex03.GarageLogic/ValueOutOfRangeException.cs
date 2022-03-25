using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        internal ValueOutOfRangeException(float i_Max , float i_Min) : base()
        {
            m_MaxValue = i_Max;
            m_MinValue = i_Min;
        }

        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
        }
      
        public float MinValue
        {
            get
            {
                return m_MinValue;
            }
        }
    }
        
}
