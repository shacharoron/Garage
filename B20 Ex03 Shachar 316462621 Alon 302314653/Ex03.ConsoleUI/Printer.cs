using Ex03.GarageLogic;
using System;

namespace Ex03.ConsoleUI
{
    static internal class Printer
    {
        internal static void PrintStateOptions()
        {
            Console.WriteLine("[1] - Fixing" + System.Environment.NewLine + "[2] - Fixed" + System.Environment.NewLine + "[3] - Payed" + System.Environment.NewLine);
        }

        internal static void PrintFuelTypeOptions()
        {
            Console.WriteLine("[1] - Octan95" + System.Environment.NewLine + "[2] - Octan96" + System.Environment.NewLine + "[3] - Octan98" + System.Environment.NewLine + "[4] - Solar" + System.Environment.NewLine);
        }

        internal static void PrintState(eState i_State, string i_LicensePlateNumber)
        {
            Console.WriteLine(string.Format("The state of vehicle {0} is: {1}", i_LicensePlateNumber, i_State.ToString()) + System.Environment.NewLine);
        }

        internal static void PrintInstructionOptions()
        {
            Console.WriteLine("[1] - Add a new vehicle to your garage" + System.Environment.NewLine + "[2] - Charge/Fuel up a vehicle"  + System.Environment.NewLine +
                "[3] - Check a vehicle for it's status" + System.Environment.NewLine + "[4] - Change a vehicle's status" + System.Environment.NewLine + "[5] - Inflate the wheels of a vehicle" + System.Environment.NewLine + "[6]" +
                " - View details of a vehicle" + System.Environment.NewLine + "[7] - Show license plates by filter" + System.Environment.NewLine);
        }

        internal static void PrintVehicleTypeOptions()
        {
            int i = 1;

            foreach (eVehicleType vehicleType in Enum.GetValues(typeof(eVehicleType)))
            {
                Console.WriteLine(string.Format("[{0}] - {1}", i, vehicleType));
                i++;
            }
        }
        
        internal static void PrintLicensePlateNotFoundMessage(string i_LicensePlateNumber)
        {
            Console.WriteLine(string.Format("A vehicle with a license plate number matching your input - {0} does not exist in this garage.", i_LicensePlateNumber) + System.Environment.NewLine);
        }

        internal static void PrintValueOutOfRangeMessage(float i_MaxValue, float i_MinValue)
        {
            Console.WriteLine(string.Format("The value you entered is not in the range, please enter a number between {0} and {1}", i_MinValue, i_MaxValue));
        }

        internal static void PrintArgumentException(string i_ExceptionDetails)
        {
            Console.WriteLine(string.Format("Invalid input. {0}", i_ExceptionDetails));
        }

        internal static void PrintFuelTypeErrorMessage(eFuelType i_FuelType)
        {
            Console.WriteLine(string.Format("The fuel type {0} does not match this vehicle type. Try again", i_FuelType));
        }
       
        internal static void PrintBadChosenOptionMessage()
        {
            Console.WriteLine(string.Format("I didn't get your choise, please type the number corresponding to the option you want to choose. Let's try again."));
        }

        internal static void PrintMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        internal static void PrintUpdateSuccessfully(eInstructionOption i_Instruction)
        {
            Console.WriteLine(string.Format("Task: {0} handeld successfully.", i_Instruction) + System.Environment.NewLine);
        }

        internal static void PrintVehicleAlreadyInGarage(string i_VehicleNumber)
        {
            Console.WriteLine(string.Format("Vehicle {0} is already listed in this garage. Moved to fixing state.", i_VehicleNumber) + System.Environment.NewLine);
        }
    }
}