using Ex03.GarageLogic;
using System;

namespace Ex03.ConsoleUI
{
    internal class GarageRunner
    {
        private GarageManager m_Manager;

        internal void Run()
        {
            eInstructionOption currentInstreuction;
            int nextInstruction = 0;
            string garageName;

            garageName = Comunicator.GreetUser();
            this.m_Manager = new GarageManager(garageName);
            while (true)
            {
                try
                {
                    nextInstruction = Comunicator.GetInstructionFromUser();
                    switch (nextInstruction)
                    {
                        case 1:
                            this.m_Manager.AddNewVehicle();
                            Printer.PrintUpdateSuccessfully(eInstructionOption.AddNewVehicle);
                            break;
                        case 2:
                            this.m_Manager.FillEnergyInVehicle();
                            Printer.PrintUpdateSuccessfully(eInstructionOption.FuelOrChargeVehicle);
                            break;
                        case 3:
                            this.m_Manager.CheckVehicleState();
                            break;
                        case 4:
                            this.m_Manager.ChangeVehicleState();
                            Printer.PrintUpdateSuccessfully(eInstructionOption.ChangeVehicleState);
                            break;
                        case 5:
                            this.m_Manager.InflateWheels();
                            Printer.PrintUpdateSuccessfully(eInstructionOption.InflateWheels);
                            break;
                        case 6:
                            this.m_Manager.GetVehicleDetails();
                            break;
                        case 7:
                            this.m_Manager.ShowLicencePlatesInGarageByFilter();
                            break;
                        default:
                            Printer.PrintBadChosenOptionMessage();
                            break;
                    }
                }
                catch (FormatException e)
                {
                    Printer.PrintMessage(e.Message);
                }
            }
        }
    }
}