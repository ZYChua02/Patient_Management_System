﻿//============================================================
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

            //BedList
            List<Bed> bedList = new List<Bed>();

            while (option != "0")
            {
                DisplayMenu();
                //Read and store user input
                Console.Write("Enter your option: ");
                option = Console.ReadLine();

                if (option == "1")
                {
                    
                    Console.WriteLine("Option 1. View All Patients");
                    Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10} {4, -12} {5, -15}",
                        "Name", "ID No.", "Age", "Gender", "Citizenship", "Status");
                    AddPatients(patientList);
                    DisplayPatients(patientList);
                }
                else if (option == "2")
                {
                    
                    /*Console.WriteLine("Option 2. View All Beds");
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
                        //if (bedsdata[0] == "A")
                        //{
                        //    bedList.Add(new ClassABed(Convert.ToInt32(bedsdata[0]), Convert.ToInt32(bedsdata[1]), Convert.ToDouble(bedsdata[2]), Convert.ToBoolean(bedsdata[3])));
                        //}

                        else if (bedsdata[0] == "B")
                        {
                            bedList.Add(new ClassBBed(Convert.ToInt32(bedsdata[0]), Convert.ToInt32(bedsdata[1]), Convert.ToDouble(bedsdata[2]), Convert.ToBoolean(bedsdata[3])));
                        }
                        
                        else if (bedsdata[0] == "C")
                        {
                            bedList.Add(new ClassCBed(Convert.ToInt32(bedsdata[0]), Convert.ToInt32(bedsdata[1]), Convert.ToDouble(bedsdata[2]), Convert.ToBoolean(bedsdata[3])));
                        }
                    }
                    foreach (Bed b in bedList)
                    {
                        if (b is ClassABed)
                        {
                            Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", "A", b.WardNo, b.BedNo, b.DailyRate, b.Available);
                        }

                        else if (b is ClassBBed)
                        {
                            Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", "B", b.WardNo, b.BedNo, b.DailyRate, b.Available);
                        }

                        else if (b is ClassCBed)
                        {
                            Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", "C", b.WardNo, b.BedNo, b.DailyRate, b.Available);
                        }

                    
                       
                    }*/
                }
                else if (option == "3")
                {
                    RegisterPatient(patientList);
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
        
        //Menu 
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

        // Ryan's Methods //
        static void AddPatients(List <Patient> patientList)
        {
            string[] patientRaw = File.ReadAllLines(@"patients.csv");
            for (int i = 1; i < patientRaw.Length; i++)
            {
                string[] pData = patientRaw[i].Split(",");

                /* pData[0] : Name
                 * pData[1] : Nric Number
                 * pData[2] : Age
                 * pData[3] : Gender
                 * pData[4] : Citizenship
                 * pData[5] : CDA/Medisave Balance (if any)
                 */
                int age = Convert.ToInt32(pData[2]);
                string cs = pData[4];
                string stat = "Registered";
                if (age >= 0 && age <= 12)
                {
                    if (cs == "SC" || cs == "sc")
                    {
                        //                    Name      Nric      Age  Gender                   Cts  status   CDA/Medisave
                        Patient p = new Child(pData[0], pData[1], age, Convert.ToChar(pData[3]), cs, stat, Convert.ToDouble(pData[5]));
                        patientList.Add(p);
                    }
                    else if (cs == "Foreigner" || cs == "PR" || cs == "pr")
                    {
                        Patient p = new Child(pData[0], pData[1], age, Convert.ToChar(pData[3]), cs, stat, Convert.ToDouble(null));
                        patientList.Add(p);
                    }
                }
                else if (age <= 64)
                {
                    if (cs == "SC" || cs == "sc" || cs == "PR" || cs == "pr") //Validaton for casing
                    {
                        Patient p = new Adult(pData[0], pData[1], age, Convert.ToChar(pData[3]), cs, stat, Convert.ToDouble(pData[5]));
                        patientList.Add(p);
                    }
                    else if (cs == "Foreigner")
                    {
                        Patient p = new Adult(pData[0], pData[1], age, Convert.ToChar(pData[3]), cs, stat, Convert.ToDouble(null));
                        patientList.Add(p);
                    }
                }
                else if (age >= 65)
                {
                    Patient p = new Senior(pData[0], pData[1], age, Convert.ToChar(pData[3]), cs, stat);
                    patientList.Add(p);
                }
            }
        }
        static void DisplayPatients(List<Patient> patientList)
        {
            foreach (Patient pa in patientList)
            {
                Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10} {4, -12} {5, -15}",
                    pa.Name, pa.Id, pa.Age, pa.Gender, pa.CitizenStatus, "Registered");
            }
        }
        static void RegisterPatient(List <Patient> patientList)
        {
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
            string stat = "Registered";
            double subsidy = 0.0;

            if (age >= 0 && age <= 12)
            {
                // only if condition is met, subsidy will be updated accordingly
                // if not met, will remain as 0.
                if (cs == "SC" || cs == "sc")
                {
                    Console.Write("Enter CDA Balance: ");
                    subsidy = Convert.ToDouble(Console.ReadLine());
                }
                Patient p = new Child(n, id, age, g, cs, stat, subsidy);
                patientList.Add(p);
                using (StreamWriter file = new StreamWriter(@"Patients.csv", true))
                {
                    string line = "\n" + n + ',' + id + ',' + age + ',' + g + ',' + cs + ',' + subsidy;
                    file.Write(line);
                }
                Console.WriteLine($"\n{n} was successfully registered!\n");
            }
            else if (age <= 64)
            {
                if (cs == "SC" || cs == "sc" || cs == "PR" || cs == "pr") 
                {
                    Console.Write("Enter Medisave Balance: ");
                    subsidy = Convert.ToDouble(Console.ReadLine());
                }
                Patient p = new Adult(n, id, age, g, cs, stat, subsidy);
                patientList.Add(p);
                using (StreamWriter file = new StreamWriter(@"Patients.csv", true))
                {
                    string line = "\n" + n + ',' + id + ',' + age + ',' + g + ',' + cs + ',' + subsidy;
                    file.Write(line);
                }
                Console.WriteLine($"\n{n} was successfully registered!\n");
            }
            else if (age >= 65)
            {
                Patient p = new Senior(n, id, age, g, cs, stat);
                patientList.Add(p);
                using (StreamWriter file = new StreamWriter(@"Patients.csv", true))
                {
                    string line = "\n" + n + ',' + id + ',' + age + ',' + g + ',' + cs + ',' + subsidy;
                    file.Write(line);
                }
                Console.WriteLine($"\n{n} was successfully registered!\n");
            }

        }
        // Zhe Yu's Methods //
    }
}
