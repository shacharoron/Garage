using Ex03.GarageLogic;
using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    internal class GarageManager
    {
        private Garage m_MyGarage;
        private readonly string r_Name;
        private readonly float r_FillAirToMaxCode = -1.0F;

        internal GarageManager(string i_Name)
        {
            this.m_MyGarage = new Garage();
            this.r_Name = i_Name;
        }
 
        private string getLicensePlateNumber()
        {
            string licensePlateNumber = Comunicator.GetLicensePlateNumber();

            while (!this.m_MyGarage.ExistsInGarage(licensePlateNumber))
            {
                Printer.PrintLicensePlateNotFoundMessage(licensePlateNumber);
                licensePlateNumber = Comunicator.GetLicensePlateNumber();
            }

            return licensePlateNumber;
        }

        internal void FillEnergyInVehicle()
        {
            bool chargedSuccessfully = false;
            bool isElectricVehicle;
            float amountToFill = 0;
            eFuelType fuelType;
            string fuelTypeCode;
            string licensePlateNumber = getLicensePlateNumber();

            while (!chargedSuccessfully)
            {
                try
                {
                    isElectricVehicle = this.m_MyGarage.CheckIfElectricByLicensePlate(licensePlateNumber);
                    if (isElectricVehicle)
                    {
                        amountToFill = Comunicator.GetChargingDetails(licensePlateNumber);
                        this.m_MyGarage.FillEnergySource(licensePlateNumber, amountToFill);
                    }
                    else
                    {
                        Comunicator.GetFuelingDetails(licensePlateNumber, out amountToFill, out fuelTypeCode);
                        eFuelType.TryParse(fuelTypeCode, out fuelType);
                        this.m_MyGarage.FillEnergySource(licensePlateNumber, amountToFill, fuelType);
                    }

                    chargedSuccessfully = true;
                }
                catch (VehicleNotInGarageException e)
                {
                    Printer.PrintLicensePlateNotFoundMessage(licensePlateNumber);
                }
                catch (ValueOutOfRangeException e)
                {
                    Printer.PrintValueOutOfRangeMessage(e.MaxValue, e.MinValue);
                }
                catch (System.ArgumentException e)
                {
                    eFuelType.TryParse(e.Message,  out fuelType);
                    Printer.PrintFuelTypeErrorMessage(fuelType);
                }
                catch (System.FormatException e)
                {
                    Printer.PrintMessage(e.Message);
                }
            }
        }
        
        internal void CheckVehicleState()
        {
            string licensePlateNumber = getLicensePlateNumber();

            try
            {
                eState vehicleState = this.m_MyGarage.GetStateByLicensePlate(licensePlateNumber);
                Printer.PrintState(vehicleState, licensePlateNumber);
            }
            catch (VehicleNotInGarageException e)
            {
                Printer.PrintLicensePlateNotFoundMessage(licensePlateNumber);
            }
        }

        internal void AddNewVehicle()
        {
            int vehicleType = 0;
            string modelName;
            string licensePlateNumber;
            string ownerName;
            string ownerPhoneNumber;
            bool isElectric;
            Dictionary<string, string> vehicleSpecialFeatures;

            Comunicator.GetVehicleGeneralDetails(out vehicleType, out modelName, out isElectric, out licensePlateNumber);
            getAndSetOwnerNameAndPhoneNumber(out ownerName, out ownerPhoneNumber);
            vehicleSpecialFeatures = this.m_MyGarage.AddVehicle(vehicleType, modelName, isElectric, licensePlateNumber, ownerName, ownerPhoneNumber);
            getAndSetEnergySource(licensePlateNumber);
            getAndSetWheels(licensePlateNumber);
            if (vehicleSpecialFeatures == null)
            {
                Printer.PrintVehicleAlreadyInGarage(licensePlateNumber);
            }
            else
            {
                getAndSetSpecialFeatures(licensePlateNumber, ref vehicleSpecialFeatures);
            }
        }

        private void getAndSetOwnerNameAndPhoneNumber(out string o_OwnerName, out string o_OwnerPhoneNumber)
        {
            bool phoneNumberSetSuccessfully = false;

            o_OwnerPhoneNumber = "";
            Comunicator.GetOwnerName(out o_OwnerName);
            while (!phoneNumberSetSuccessfully)
            {
                try
                {
                    Comunicator.GetOwnerPhoneNumber(o_OwnerName, out o_OwnerPhoneNumber);
                    phoneNumberSetSuccessfully = true;
                }
                catch (FormatException e)
                {
                    Printer.PrintMessage(e.Message);
                }
            }
        }

        private void getAndSetSpecialFeatures(string i_LicensePlateNumber, ref Dictionary<string, string> io_VehicleSpecialFeatures)
        {
            bool specialFeaturesSetSuccessfully = false;

            Comunicator.GetVehicleSpecificDetails(ref io_VehicleSpecialFeatures);
            while (!specialFeaturesSetSuccessfully)
            {
                try
                {
                    this.m_MyGarage.SetVehicleSpecialFeatures(i_LicensePlateNumber, ref io_VehicleSpecialFeatures);
                    specialFeaturesSetSuccessfully = true;
                }
                catch (ArgumentException e)
                {
                    string[] errorMessages = e.Message.Split(':');
                    Printer.PrintArgumentException(errorMessages[1]);
                    Comunicator.GetVehicleSpecificDetail(ref io_VehicleSpecialFeatures, errorMessages[0]);
                }
            }
        }

        private void getAndSetWheels(string i_LicensePlateNumber)
        {
            string manufacturerName;
            float currentAirPressure = 0;
            bool setSuccessfuly = false;

            Comunicator.GetWheelsManufacturer(i_LicensePlateNumber, out manufacturerName);
            while (!setSuccessfuly)
            {
                try
                { 
                    currentAirPressure = Comunicator.GetCurrentAirPressure(i_LicensePlateNumber);
                    m_MyGarage.SetWheels(i_LicensePlateNumber, manufacturerName, currentAirPressure);
                    setSuccessfuly = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Printer.PrintValueOutOfRangeMessage(e.MaxValue, e.MinValue);
                }
            }
        }

        private void getAndSetEnergySource(string i_LicensePlateNumber)
        {
            bool setSuccessfuly = false;
            float energyAmountInEnergySource = 0;

            while (!setSuccessfuly)
            {
                try
                {
                    if (this.m_MyGarage.CheckIfElectricByLicensePlate(i_LicensePlateNumber))
                    {
                        energyAmountInEnergySource = Comunicator.GetBatteryCurrentStatus(i_LicensePlateNumber);
                    }
                    else
                    {
                        energyAmountInEnergySource = Comunicator.GetFuelTankCurrentStatus(i_LicensePlateNumber);
                    }

                    this.m_MyGarage.SetEnergySource(i_LicensePlateNumber, energyAmountInEnergySource);
                    setSuccessfuly = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Printer.PrintValueOutOfRangeMessage(e.MaxValue, e.MinValue);
                }
            }
        }

        internal void ChangeVehicleState()
        {
            string licensePlateNumber = getLicensePlateNumber();
            string newStateCode;
            eState newState;

            try
            {
                newStateCode = Comunicator.GetUpdatedState(licensePlateNumber);
                eState.TryParse(newStateCode, out newState);
                m_MyGarage.ChangeState(licensePlateNumber, newState);
            }
            catch (VehicleNotInGarageException e)
            {
                Printer.PrintLicensePlateNotFoundMessage(licensePlateNumber);
            }
        }

        internal void InflateWheels()
        {
            string licensePlateNumber = getLicensePlateNumber();
            int numberOfWheels = 0;
            float[] wheelsAirPressure;

            try
            {
                numberOfWheels = this.m_MyGarage.GetNumberOfWheelsInSpecificVehicle(licensePlateNumber);
                wheelsAirPressure = new float[numberOfWheels];
                for(int i = 0; i < wheelsAirPressure.Length; i++)
                {
                    wheelsAirPressure[i] = this.r_FillAirToMaxCode;
                }

                this.m_MyGarage.PumpWheels(licensePlateNumber, wheelsAirPressure);
            }
            catch (VehicleNotInGarageException e)
            {
                Printer.PrintLicensePlateNotFoundMessage(licensePlateNumber);
            }
            catch (ValueOutOfRangeException e)
            {
                Printer.PrintValueOutOfRangeMessage(e.MaxValue, e.MinValue);
            }
        }

        internal void GetVehicleDetails()
        {
            string licensePlateNumber = getLicensePlateNumber();
            string vehicleInfo;

            try
            {
                vehicleInfo = this.m_MyGarage.GetVehicleDetails(licensePlateNumber);
                Printer.PrintMessage(vehicleInfo);
            }
            catch (VehicleNotInGarageException e)
            {
                Printer.PrintLicensePlateNotFoundMessage(licensePlateNumber);
            }
        }

        internal void ShowLicencePlatesInGarageByFilter()
        {
            bool[] filters = new bool[3];
            string filteredLicensePlates;

            Comunicator.ChooseFilters(ref filters);
            filteredLicensePlates = m_MyGarage.LicensePlatesByState(ref filters);
            Printer.PrintMessage(filteredLicensePlates);
        }
    }
}