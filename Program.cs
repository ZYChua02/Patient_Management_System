//============================================================
// Student Number   : S10196904C, S10196605J
// Student Name	    : NG CHIN TIONG RYAN, CHUA ZHE YU
// Module  Group	: T08 
//============================================================

/*====================== T O D O ============================
Ryan   : ToString() of Stay.cs, CalculateCharges() for Adult, 
         Senior & Child.cs (Wait for Zhe Yu to complete his classes)
         Validations of Options 1 & 3

Zhe Yu : Refer to help notes below.

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

            //PatientList should be accessible to all options
            List<Patient> patientList = new List<Patient>();

            while (option != "0")
            {
                DisplayMenu();
                //Read and store user input
                Console.Write("Enter your option: ");
                option = Console.ReadLine();

                if (option == "1")
                {
                    string[] patientRaw = File.ReadAllLines(@"patients.csv");
                    Console.WriteLine("Option 1. View All Patients");
                    Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10} {4, -12} {5, -15}",
                        "Name", "ID No.", "Age", "Gender", "Citizenship", "Status");
                    for (int i = 1; i < patientRaw.Length; i++)
                    {
                        string[] pData = patientRaw[i].Split(",");
                        Patient p = new Patient(pData[0], pData[1], Convert.ToInt32(pData[2]), Convert.ToChar(pData[3]), pData[4], "Registered");
                        patientList.Add(p);
                    }

                    foreach (Patient pa in patientList)
                    {
                        Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10} {4, -12} {5, -15}",
                            pa.Name, pa.Id, pa.Age, pa.Gender, pa.CitizenStatus, "Registered");
                    }
                }
                else if (option == "2")
                {
                    List<Bed> bedList = new List<Bed>();
                    Console.WriteLine("Option 2. View All Beds");
                    Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", "Type", "Ward No", "Bed No", "Daily Rate", "Available");
                    string[] csvlines = File.ReadAllLines(@"beds.csv");

                    //Zhe Yu to Note: i starts from 1 (remove header)
                    for (int i = 1; i < csvlines.Length; i++)
                    {
                        string[] bedsdata = csvlines[i].Split(',');

                        // types in class: int WardNo, int BedNo, Double DailyRate, Bool Avaliable
                        // types in data file: string WardType, string WardNo, string Type, string DailyRate (You needa convert "Yes" or "No"
                        // to true/false
                        //also may need to downcast Bed to match Bed Classes as Bed is an abstract class (no implementation)
                        //bedList.Add(new Bed(Convert.ToInt32(bedsdata[0]), Convert.ToInt32(bedsdata[1]), Convert.ToDouble(bedsdata[2]), Convert.ToBoolean(bedsdata[3])));
                    }
                    foreach (Bed b in bedList)
                    {
                        //Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", b.Type, b.WardNo, b.BedNo, b.DailyRate, b.Available);
                    }
                }
                else if (option == "3")
                {
                    //need to check if this is the best way to do the option
                    Console.WriteLine("Option 3. Register Patient");
                    Console.Write("Enter Name: ");
                    string n = Console.ReadLine();
                    Console.Write("Enter Identification Number: ");
                    string id = Console.ReadLine();
                    Console.Write("Enter Age: ");
                    int age = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Gender [M/F]: ");
                    char g = Convert.ToChar(Console.ReadLine());
                    Console.Write("Enter Citizenship Status [SC/PR/Foreigner]: ");
                    string cs = Console.ReadLine();
                    Console.Write("Enter Status: ");
                    string stat = Console.ReadLine();

                    //child SC
                    if (age >= 0 && age <= 12 && cs == "SC")
                    {
                        Console.Write("Enter CDA Balance: ");
                        double cda = Convert.ToDouble(Console.ReadLine());
                        patientList.Add(new Child(id, n, age, g, cs, stat, cda));
                    }

                    //child PR or child foreigner
                    else if (age >= 0 && age <= 12 && cs != "SC")
                    {
                        patientList.Add(new Child(id, n, age, g, cs, stat, 0.0));
                    }

                    //adult SC or adult PR
                    else if (age > 12 && age <= 64 && (cs == "SC" || cs == "PR"))
                    {
                        Console.Write("Enter Medisave Balance: ");
                        double mdb = Convert.ToDouble(Console.ReadLine());
                        patientList.Add(new Adult(id, n, age, g, cs, stat, mdb));
                    }

                    //adult foreigner
                    else if (age > 12 && age <= 64 && cs == "Foreigner")
                    {
                        patientList.Add(new Adult(id, n, age, g, cs, stat, 0.0));
                    }

                    //seniors
                    else if (age >= 65)
                    {
                        patientList.Add(new Senior(id, n, age, g, cs, stat));
                    }
                    //validation: any incorrect spellings or attempts
                    else
                    {
                        Console.WriteLine("Invalid Inputs. Please re-enter.");
                    }

                    //Need to Write to CSV file and display status of adding to list
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
                    Console.WriteLine("Invalid Option! Please try again!");
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
