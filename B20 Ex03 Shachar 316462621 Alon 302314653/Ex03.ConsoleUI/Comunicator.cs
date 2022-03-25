using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal static class Comunicator
    {
        internal static string GreetUser()
        {
            string garageName;

            Console.WriteLine("Hello! and welcome to your garage!");
            Console.WriteLine("First thing's first, what is the name of this garage?");
            garageName = getInputFromUser();
            Console.WriteLine(String.Format("{0} it is!", garageName));
            Console.WriteLine("Now you can preform the following operatins on your wonderful new garage!!!" + System.Environment.NewLine);

            return garageName;
        }

        /*
         *Gets an isntruction from the user as a number between 1-7.
         * If the input doesn't match any instruction, throws a FormatException. 
         */
        internal static int GetInstructionFromUser()
        {
            int instruction = 0;

            Console.WriteLine("What whould you like to do?");
            Printer.PrintInstructionOptions();
            if (!int.TryParse(getInputFromUser(), out instruction) || instruction > 7 || instruction < 1)
            {
                throw new FormatException("Bad instruction input.");
            }

            Console.Clear();

            return instruction;
        }

        internal static void GetOwnerName(out string o_Name)
        {
            Console.WriteLine("Please enter the owner's name.");
            o_Name = getInputFromUser();
        }

        internal static void GetOwnerPhoneNumber(string i_Name, out string o_PhoneNumber)
        {
            Console.WriteLine(string.Format("Please enter {0}'s phone number. (XXX-XXXXXXX or a 10 digit number)", i_Name));
            o_PhoneNumber = getInputFromUser();
            if (!parsePhoneNumber(o_PhoneNumber, out string error))
            {
                throw new FormatException(error);
            }
        }

        private static bool parsePhoneNumber(string i_PhoneNumber, out string o_Error)
        {
            bool success = true;
            long parsedPhoneNumber = 0;
            int startOfNumber = 0;
            int endOfNumber = 0;

            o_Error = "";
            if (i_PhoneNumber.IndexOf("-") == -1)
            {
                if (!(long.TryParse(i_PhoneNumber, out parsedPhoneNumber)) || i_PhoneNumber.Length != 10)
                {
                    success = false;
                    o_Error = "Problem with phone number format. Please only use digits and \"-\".";
                }
            }
            else if (i_PhoneNumber.IndexOf("-") == 3)
            {
                if (!int.TryParse(i_PhoneNumber.Substring(0, 3), out startOfNumber) || !int.TryParse(i_PhoneNumber.Substring(4), out endOfNumber))
                {
                    success = false;
                    o_Error = "Problem with phone number format. Please only use digits and \"-\".";
                }
                else if (i_PhoneNumber.Length > 11)
                {
                    success = false;
                    o_Error = "Seems like you have to many digits...";
                }
                else if (i_PhoneNumber.Length < 10)
                {
                    o_Error = "Seems like you are missing digits...";
                }
            }
            else
            {
                success = false;
                o_Error = "Problem with phone number format.";
            }

            return success;
        }

        /*
         * Get the basic details that every Vehicle object has from the user.
         */
        internal static void GetVehicleGeneralDetails(out int o_VehicleType, out string o_ModelName, out bool o_IsElectric, out string o_LicensePlateNumber)
        {
            char isElectric = 'N';

            Console.Clear();
            Console.WriteLine("Now let's get some details about the vehicle.");
            Console.WriteLine("=====================GENERAL INFO=====================");
            o_VehicleType = getVehicleType();
            Console.WriteLine(string.Format("What is the {0}'s model?", (eVehicleType)o_VehicleType));
            o_ModelName = getInputFromUser();
            if (o_VehicleType != 2)
            {
                Console.WriteLine(string.Format("Is the {0} electric? (Y/N)", (eVehicleType)o_VehicleType));
                isElectric = getInputFromUser()[0];
                while (isElectric != 'Y' && isElectric != 'N' && isElectric != 'y' && isElectric != 'n')
                {
                    Console.Clear();
                    Console.WriteLine("Please choose Y for electric and N for fuled");
                    isElectric = getInputFromUser()[0];
                }
            }

            o_IsElectric = (isElectric == 'Y' || isElectric == 'y');
            o_LicensePlateNumber = GetLicensePlateNumber();
        }

        /*
        * Given a dictionary with values that corrspond to a specific vehicle's special features, collects values corresponding
        * to the keys in the dictionary from the user.
        */
        internal static void GetVehicleSpecificDetails(ref Dictionary<string, string> io_PropertyDictionary)
        {
            Dictionary<string, string> tempDictionay = new Dictionary<string, string>(io_PropertyDictionary);

            Console.Clear();
            foreach (string property in tempDictionay.Keys)
            {
                Console.WriteLine(string.Format("Please enter {0}:", property));
                io_PropertyDictionary[property] = getInputFromUser().ToLower();
            }
        }

        /*
        * Given a dictionary with values that corrspond to a specific vehicle's special features, collects a spicific value corresponding
        * to a specific given keys in the dictionary from the user.
        */
        internal static void GetVehicleSpecificDetail(ref Dictionary<string, string> io_PropertyDictionary, string i_Key)
        {
            Console.WriteLine(string.Format("Please enter {0}, again:", i_Key));
            io_PropertyDictionary[i_Key] = getInputFromUser().ToLower();
        }

        private static int getVehicleType()
        {
            string vehicleTypeCode;
            int vehicleTypeNumber = 0;
            eVehicleType vehicleType;

            Console.WriteLine("What type of vehicle are you registering?");
            Printer.PrintVehicleTypeOptions();
            vehicleTypeCode = getInputFromUser();
            while (!eVehicleType.TryParse(vehicleTypeCode, out vehicleType))
            {
                Console.WriteLine("Invalid input. Please select the number corresponding to the chosen option:");
                Printer.PrintVehicleTypeOptions();
                vehicleTypeCode = getInputFromUser();
            }

            vehicleTypeNumber = int.Parse(vehicleTypeCode);

            return vehicleTypeNumber;
        }

        internal static float GetBatteryCurrentStatus(string i_LicensePlateNumber)
        {
            float currentBatteryChargeInHours = 0;

            Console.WriteLine(string.Format("What is the current battery charge of vehicle number {0}? (in hours)", i_LicensePlateNumber));
            while (!float.TryParse(getInputFromUser(), out currentBatteryChargeInHours))
            {
                Console.WriteLine("Invalid input. Please enter the battery charge (in hours).");
            }

            return currentBatteryChargeInHours;
        }

        internal static float GetFuelTankCurrentStatus(string i_LicensePlateNumber)
        {
            float currentFuleTankStatusInLiters = 0;

            Console.WriteLine(string.Format("What is the current tank status of vehicle {0}? (in liters)", i_LicensePlateNumber));
            while (!float.TryParse(getInputFromUser(), out currentFuleTankStatusInLiters))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter the tank status (in liters) again.");
            }

            return currentFuleTankStatusInLiters;
        }

        internal static float GetChargingDetails(string i_LicensePlateNumber)
        {
            float chargeAmmount = 0;

            Console.WriteLine(string.Format("How much time do you want to charge vehicle {0}? (in hours)", i_LicensePlateNumber));
            while (!float.TryParse(getInputFromUser(), out chargeAmmount))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter the wanted charge time (in hours) again.");
            }

            return chargeAmmount;
        }

        internal static void GetFuelingDetails(string i_LicensePlateNumber, out float o_FuelAmount, out string o_FuelType)
        {
            float fuelAmount = 0;

            o_FuelType = GetFuelType();
            Console.WriteLine(string.Format("How many liters do you want to fuel into vehicle {0} with?", i_LicensePlateNumber));
            while (!float.TryParse(getInputFromUser(), out fuelAmount))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter the wanted fuel amount (in liters) again.");
            }

            o_FuelAmount = fuelAmount;
        }

        internal static string GetFuelType()
        {
            string fuelType;
            int fuelTypeCode = 0;

            Console.WriteLine("What is the fuel type that you whould like to use?");
            Printer.PrintFuelTypeOptions();
            fuelType = getInputFromUser();
            while (!int.TryParse(fuelType, out fuelTypeCode) || fuelTypeCode < 1 || fuelTypeCode > 4)
            {
                throw new FormatException("Bad fuel type input.");
            }

            return fuelType;
        }

        internal static void GetWheelsManufacturer(string i_LicensePlateNumber, out string o_ManufacturerName)
        {
            Console.Clear();
            Console.WriteLine(string.Format("What is the manufacturer's name for the wheels of vehicle {0}?", i_LicensePlateNumber));
            o_ManufacturerName = getInputFromUser();
        }

        internal static float GetCurrentAirPressure(string i_LicensePlateNumber)
        {
            float currenAirPressure = 0;

            Console.WriteLine(string.Format("What is the air pressure of the wheels in vehicle number {0}?", i_LicensePlateNumber));
            while (!float.TryParse(getInputFromUser(), out currenAirPressure))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter the current air pressure again.");
            }

            return currenAirPressure;
        }

        internal static string GetLicensePlateNumber()
        {
            Console.WriteLine("Enter license plate number:");

            return getInputFromUser();
        }

        internal static void ChooseFilters(ref bool[] io_FilterChart)
        {
            char userAnswer;
            eState[] states = { eState.Fixing, eState.Fixed, eState.Payed };

            Console.Clear();
            for (int i = 0; i < states.Length; i++)
            {
                Console.WriteLine(string.Format("Would you like to see a list of the {0} cars? Y\\N", states[i]));
                userAnswer = getInputFromUser()[0];
                while (userAnswer != 'Y' && userAnswer != 'N' && userAnswer != 'n' && userAnswer != 'y')
                {
                    Console.WriteLine(string.Format("Invalid input. Please enter 'Y' if you would like to see {0}, and 'N' if you would't", states[i])); 
                    Console.WriteLine(string.Format("Would you like to see a list of the {0} cars? Y\\N", states[i]));
                    userAnswer = getInputFromUser()[0];
                }

                io_FilterChart[i] = (userAnswer == 'Y' || userAnswer == 'y');
            }
        }

        internal static string GetUpdatedState(string i_LicensePlateNumber)
        {
            string newState;
            int newStateCode = 0;

            Console.Clear();
            Console.WriteLine(string.Format("What is the new state of vehicle {0}", i_LicensePlateNumber));
            Printer.PrintStateOptions();
            newState = getInputFromUser();
            while (!int.TryParse(newState, out newStateCode) || newStateCode < 1 || newStateCode > 3)
            {
                Console.WriteLine("Invalid input. Please select the number corresponding to the chosen option:");
                Printer.PrintStateOptions();
                newState = getInputFromUser();
                int.TryParse(newState, out newStateCode);
            }

            return newState;
        }

        /* 
         * Reads an input from the user and makes sure it isn't a null input. If it is - asks the user for a new input.
         */
        private static string getInputFromUser()
        {
            string input = Console.ReadLine();

            while (input.Length == 0)
            {
                Console.WriteLine("You didn't enter anything... Try again.");
                input = Console.ReadLine();
            }

            Console.Clear();

            return input;
        }
    }
}