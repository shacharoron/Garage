namespace Ex03.GarageLogic
{
    internal struct Owner
    {
        private readonly string r_Name;
        private string m_PhoneNumber;

        internal Owner(string i_Name, string i_PhoneNumber)
        {
            this.r_Name = i_Name;
            this.m_PhoneNumber = i_PhoneNumber;
        }

        internal string Name
        {
            get
            {
                return this.r_Name;
            }
        }

        internal string PhoneNumber
        {
            get
            {
                return m_PhoneNumber;
            }
            set
            {
                this.m_PhoneNumber = value;
            }
        }
    }
}