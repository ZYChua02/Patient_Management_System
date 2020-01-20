//============================================================
// Student Number   : S10196904C, S10196605J
// Student Name	    : NG CHIN TIONG RYAN, CHUA ZHE YU
// Module  Group	: T08 
//============================================================

/*====================== T O D O ============================
Ryan   : ToString() of Stay.cs, CalculateCharges() for Adult, 
         Senior & Child.cs (Wait for Zhe Yu to complete his classes)

Zhe Yu : Implement BedStay, MedicalRecord, Bed, ClassABed, 
         ClassBBed, ClassCBed classes

Both   :

============================================================*/

using System;
using System.Collections.Generic;
using System.IO;

namespace PRG2_T08_Team2
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Initialise variable option to enable storing 
              of user input thereafter*/
            string option = "1";
            while (option != "0")
            {
                DisplayMenu();
                //Read and store user input
                Console.Write("Enter your option: ");
                option = Console.ReadLine();

                if (option == "1")
                {
                   List<Bed> bedList = new List<Bed>();
                   Console.WriteLine("Option 2. View All Beds");
                   Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", "Type", "Ward No", "Bed No", "Daily Rate", "Available");
                   string[] csvlines = File.ReadAllLines(@"beds.csv");
                   for (int i = 0; i < csvlines.Length; i++)
                   {
                       string[] bedsdata = csvlines[i].Split(',');
                       bedList.Add(new Bed(Convert.ToInt32(bedsdata[0]), Convert.ToInt32(bedsdata[1]), Convert.ToDouble(bedsdata[2]), Convert.ToBoolean(bedsdata[3])));
                   }
                   foreach(Bed b in bedList)
                   {
                        Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", b.Type , b.WardNo , b.BedNo, b.DailyRate, b.Available);
                   }
                }
                else if (option == "2")
                {

                }
                else if (option == "3")
                {

                }
                else if (option == "4")
                {

                }
                else if (option == "5")
                {

                }
                else if (option == "6")
                {

                }
                else if (option == "7")
                {

                }
                else if (option == "8")
                {

                }
                else if (option == "9")
                {

                }
                else if (option == "10")
                {

                }
                else if (option == "11")
                {

                }
                else if (option == "12")
                {

                }
                else if (option == "0")
                {
                    //Exit the program
                    break;
                }
                else
                {
                    //To catch errors and perform validation of input

                }

                Console.ReadKey();
            }
        }
        
        //Menu Done by Ryan
        static void DisplayMenu()
        {
            //Header of Menu
            Console.WriteLine("\n================ M E N U =================");
            List<string> menu = new List<string> 
            {"Exit", "View all patients","View All Beds","Register Patient",
            "Add new bed","Register a Hospital Stay","Retrieve Patient Details","Add Medical Record Entry",
            "View Medical Records", "Transfer patient to another bed","Discharge and Payment", 
            "Display Currencies Exchange Rate","Display PM 2.5 Information"};
            
            /*Loop through the list and print the respective string 
              corresponding to the option, starting from 1 (Display Exit at the bottom)*/
            for (int i = 1; i < menu.Count; i++)
            {
                Console.WriteLine("[{0}] {1} ", i, menu[i]);
            }

            //Line of code to display "Exit" string
            Console.WriteLine("[0] " + menu[0] + "\n");
        }
    }
}
