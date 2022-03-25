using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, Vehicle> m_FixingVehiclesInGarage;
        private Dictionary<string, Vehicle> m_FixedVehiclesInGarage;
        private Dictionary<string, Vehicle> m_PayedVehiclesInGarage;
        private Dictionary<string, Vehicle>[] m_ArrayOfStateDictionaries;

        public Garage()
        {
            this.m_FixingVehiclesInGarage = new Dictionary<string, Vehicle>();
            this.m_FixedVehiclesInGarage = new Dictionary<string, Vehicle>();
            this.m_PayedVehiclesInGarage = new Dictionary<string, Vehicle>();
            this.m_ArrayOfStateDictionaries = new Dictionary<string, Vehicle>[] { this.m_FixingVehiclesInGarage, this.m_FixedVehiclesInGarage, this.m_PayedVehiclesInGarage};
        }

        public Dictionary<string, string> AddVehicle(int i_VehicleType, string i_ModelName, bool i_IsElectric, string i_LicensePlateNumber, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            Vehicle newVehicle = null;
            Dictionary<string, string> specialFeatursDictionary = null;
            Owner vehicleOwner;

            if (ExistsInGarage(i_LicensePlateNumber))
            {
                ChangeState(i_LicensePlateNumber, eState.Fixing);
            }
            else
            {
                vehicleOwner = new Owner(i_OwnerName,i_OwnerPhoneNumber);
                newVehicle = VehicleGenerator.AddVehicle(i_VehicleType, i_ModelName, i_IsElectric, i_LicensePlateNumber, vehicleOwner);
                this.m_FixingVehiclesInGarage.Add(newVehicle.LicensePlateNumber, newVehicle);
                specialFeatursDictionary = newVehicle.GetSpecialFeatursDictionary();
            }
            
            return specialFeatursDictionary;
        }

        /*
         * Sets all features for specific vehicle that are not global for all vehicles
         */
        public void SetVehicleSpecialFeatures(string i_LicensePlateNumber, ref Dictionary<string, string> io_VehicleSpecialFeatures)
        {
            Vehicle currentVehicle = search(i_LicensePlateNumber);
            currentVehicle.SetVehicleSpecialFeatures(io_VehicleSpecialFeatures);
        }

        public bool ExistsInGarage(string i_LicensePlate)
        {
            return !(search(i_LicensePlate) == null);
        }

        /*
         * Searches for a vehicle in the garage based on a given license plate.
         * Returns null if the vehicle doesn't exist.
         */
        private Vehicle search(string i_LicensePlate)
        {
            Vehicle wantedVehicle = null;

            if (this.m_FixingVehiclesInGarage.TryGetValue(i_LicensePlate, out wantedVehicle)) ;
            else if (this.m_FixedVehiclesInGarage.TryGetValue(i_LicensePlate, out wantedVehicle)) ;
            else if (this.m_PayedVehiclesInGarage.TryGetValue(i_LicensePlate, out wantedVehicle)) ;

            return wantedVehicle;
        }


        /*
         * Gets a string containing all of the vehicles details
         * Throws VehicleNotInGarageException exception if car isn't in garage
         */
        public string GetVehicleDetails(string i_LicensePlateNumber)
        {
            Vehicle vehicleToGetDetails = search(i_LicensePlateNumber);
            string vehicleDetails;

            if (vehicleToGetDetails == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                vehicleDetails = vehicleToGetDetails.ToString();
            }

            return vehicleDetails;
        }

        /*
         * Changes vehicle's state to given state
         * Throws VehicleNotInGarageException exception if car isn't in garage
         */
        public void ChangeState(string i_LicensePlateNumber, eState i_NewState)
        {
            Vehicle vehicleToChangeState = search(i_LicensePlateNumber);

            if (vehicleToChangeState == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                removeVehicleFromListOfState(vehicleToChangeState);
                addVehicleToListOfState(vehicleToChangeState, i_NewState);
                vehicleToChangeState.State = i_NewState;
            }
        }

        private void removeVehicleFromListOfState(Vehicle i_VehicleToRemove)
        {
            switch (i_VehicleToRemove.State)
            {
                case eState.Fixing:
                    m_FixingVehiclesInGarage.Remove(i_VehicleToRemove.LicensePlateNumber);
                    break;
                case eState.Fixed:
                    m_FixedVehiclesInGarage.Remove(i_VehicleToRemove.LicensePlateNumber);
                    break;
                case eState.Payed:
                    m_PayedVehiclesInGarage.Remove(i_VehicleToRemove.LicensePlateNumber);
                    break;
            }
        }

        private void addVehicleToListOfState(Vehicle i_VehicleToAdd, eState i_StateOfVehicle)
        {
            switch (i_StateOfVehicle)
            {
                case eState.Fixing:
                    m_FixingVehiclesInGarage.Add(i_VehicleToAdd.LicensePlateNumber, i_VehicleToAdd);
                    break;
                case eState.Fixed:
                    m_FixedVehiclesInGarage.Add(i_VehicleToAdd.LicensePlateNumber, i_VehicleToAdd);
                    break;
                case eState.Payed:
                    m_PayedVehiclesInGarage.Add(i_VehicleToAdd.LicensePlateNumber, i_VehicleToAdd);
                    break;
            }
        }

        /*
         * Gets a string containing all the license plates in given state.
         * Receives a boolean array of size 3 representing the states(Fixing, Fixed, Payed).  
         */
        public string LicensePlatesByState(ref bool[] io_WantedStates)
        {
            StringBuilder stringOfVehiclesByState = new StringBuilder();

            for (int i = 0; i < io_WantedStates.Length; i++)
            {
                if (io_WantedStates[i] == true)
                {
                    stringOfVehiclesByState.Append(((eState)(i + 1)).ToString() + ": ");
                    stringOfVehiclesByState.Append(listOfVehiclesWithSameStateToString(ref this.m_ArrayOfStateDictionaries[i]));
                    stringOfVehiclesByState.Append(System.Environment.NewLine);
                }
            }

            return stringOfVehiclesByState.ToString();
        }

        public int GetNumberOfWheelsInSpecificVehicle(string i_LicensePlateNumber)
        {
            int numberOfWheels = 0;

            if (ExistsInGarage(i_LicensePlateNumber))
            {
                numberOfWheels = search(i_LicensePlateNumber).NumberOfWheels;
            }
            else
            {
                numberOfWheels = 0;
            }

            return numberOfWheels;
        }

        /*
         * Creates a string with all license plates of vehicles in a given state
         */
        private string listOfVehiclesWithSameStateToString(ref Dictionary<string, Vehicle> io_DictionaryOfCarsInState)
        {
            StringBuilder stringOfVehiclesInState = new StringBuilder();
            Vehicle currentVehicle = null;

            foreach (string licensePlateNumber in io_DictionaryOfCarsInState.Keys)
            {
                stringOfVehiclesInState.Append(licensePlateNumber + ", ");
            }

            if (stringOfVehiclesInState.Length > 0)
            {
                stringOfVehiclesInState.Remove(stringOfVehiclesInState.Length - 2, 2);
            }

            stringOfVehiclesInState.Append(System.Environment.NewLine);

            return stringOfVehiclesInState.ToString();
        }


        public void SetEnergySource(string i_LicensePlateNumber, float i_CurrentEnergyInEnergySource)
        {
            Vehicle currentVehicle = search(i_LicensePlateNumber);

            currentVehicle.SetEnergy(i_CurrentEnergyInEnergySource);
        }

        public void SetWheels(string i_LicensePlateNumber, string i_ManufacturerName, float i_CurrentAirPressure)
        {
            Vehicle currentVehicle = search(i_LicensePlateNumber);
            currentVehicle.SetWheels(i_ManufacturerName,i_CurrentAirPressure);
        }

        public void PumpWheels(string i_LicensePlateNumber, float[] i_AmountOfAirToFillInWheels)
        {
            Vehicle vehicleToPumpWheels = search(i_LicensePlateNumber);

            if (vehicleToPumpWheels == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                vehicleToPumpWheels.PumpWheels(i_AmountOfAirToFillInWheels);
            }
        }

        /*
         * Fills the fuel tank of vehicle with given license plate number.
         */
        public void FillEnergySource(string i_LicensePlateNumber, float i_AmountToFill, eFuelType i_FuelType)
        {
            Vehicle vehicleToFillEnergySource = search(i_LicensePlateNumber);

            if (vehicleToFillEnergySource == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                vehicleToFillEnergySource.FillEnergySource(i_AmountToFill, i_FuelType);
            }
        }

        /*
         * Fills the battery of vehicle with given license plate number.
         */
        public void FillEnergySource(string i_LicensePlateNumber, float i_AmountToFill)
        {
            Vehicle vehicleToFillEnergySource = search(i_LicensePlateNumber);

            if (vehicleToFillEnergySource == null)
            {
                throw new VehicleNotInGarageException();
            }
            else
            {
                vehicleToFillEnergySource.FillEnergySource(i_AmountToFill);
            }
        }

        public eState GetStateByLicensePlate(string i_LicensePlateNumber)
        {
            Vehicle vehicleToCheck = search(i_LicensePlateNumber);

            if (vehicleToCheck == null)
            {
                throw new VehicleNotInGarageException();
            }

            return vehicleToCheck.State;
        }

        public bool CheckIfElectricByLicensePlate(string i_LicensePlateNumber)
        {
            Vehicle vehicleToCheck = search(i_LicensePlateNumber);

            if (vehicleToCheck == null)
            {
                throw new VehicleNotInGarageException();
            }

            return vehicleToCheck.IsElectric;
        }
    }
}