//============================================================
// Student Number   : S10196904C, S10196605J
// Student Name	    : NG CHIN TIONG RYAN, CHUA ZHE YU
// Module  Group	: T08 
//============================================================

/*====================== T O D O ============================
Ryan   : ToString() of Stay.cs, CalculateCharges() for Adult, 
         Senior & Child.cs 
         Validations of Options 1 & 3

Zhe Yu : Option 2 and 4

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
                    
                    Console.WriteLine("Option 2. View All Beds");
                    Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10} ", "No", "Type", "Ward No", "Bed No", "Daily Rate", "Available");
                    DisplayAllBeds(bedList);
                }
                else if (option == "3")
                {
                    RegisterPatient(patientList);
                }
                else if (option == "4")
                {
                    Bed a = AddBed();
                    //bedList.Add(b)
                }
                else if (option == "5")
                {
                    DisplayPatients(patientList);
                    RegisterHospitalStay();

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

        static void RegisterHospitalStay()
        {
            Console.Write("Enter Patient Number: ");
            int pNo = Convert.ToInt32(Console.ReadLine());
        }


        // Zhe Yu's Methods //
        static void DisplayAllBeds(List <Bed> bList)
        {
            string[] csvlines = File.ReadAllLines(@"beds.csv");
            for (int i = 1; i<csvlines.Length; i++)
            {
                string[] bedsdata = csvlines[i].Split(',');
                //bedsdata[0] : class of bed
                //bedsdata[1] : wardno
                //bedsdata[2] : BedNo
                //bedsdata[3] : Availabiity (Yes/No)
                //bedsdata[4] : Daily Rate
                if (bedsdata[0] == "A") //To check which class is the bed
                {
                    bList.Add(new ClassABed(Convert.ToInt32(bedsdata[1]), Convert.ToInt32(bedsdata[2]), Convert.ToDouble(bedsdata[4]), true));
                }

                else if (bedsdata[0] == "B")
                {
                    bList.Add(new ClassBBed(Convert.ToInt32(bedsdata[1]), Convert.ToInt32(bedsdata[2]), Convert.ToDouble(bedsdata[4]), true));
                }

                else if (bedsdata[0] == "C")
                {
                    bList.Add(new ClassCBed(Convert.ToInt32(bedsdata[1]), Convert.ToInt32(bedsdata[2]), Convert.ToDouble(bedsdata[4]), true));
                }

                
            }
            foreach (Bed b in bList)
            {
                int counter = 1;
                if (b is ClassABed) //To display according to type
                {
                    
                    ClassABed abed = (ClassABed)b; //Downcasting
                    Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", counter, "A", abed.WardNo, abed.BedNo, abed.DailyRate, abed.Available);
                }

                else if (b is ClassBBed)
                {
                    ClassBBed bbed = (ClassBBed)b; //Downcasting
                    Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", counter, "B", bbed.WardNo, bbed.BedNo, bbed.DailyRate, bbed.Available);
                   
                }

                else if (b is ClassCBed)
                {
                    ClassCBed cbed = (ClassCBed)b; //Downcasting
                    Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", counter, "C" , cbed.WardNo, cbed.BedNo, cbed.DailyRate, cbed.Available);
                }
                //counter = counter + 1;

            }

            

        }
        static Bed AddBed()
        {
            //inputs
            Console.Write("Enter Ward Type[A/B/C]");
            string wardtype = Console.ReadLine();
            Console.Write("Enter Ward No: ");
            int wardno = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Bed No: ");
            int bedno = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Daily Rate: ");
            double drate = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter Available[Y/N]: ");
            string available = Console.ReadLine();
            if (wardtype == "A" && available == "Y") //Find out the class and availability
            {
                ClassABed newabed = new ClassABed(wardno, bedno, drate, true);
                return newabed;
            }

            else if (wardtype == "A" && available == "N") //if availability is no
            {
                ClassABed newabed = new ClassABed(wardno, bedno, drate, false);
                return newabed;

            }

            if (wardtype == "B" && available == "Y")
            {
                ClassBBed newbbed = new ClassBBed(wardno, bedno, drate, true);
                return newbbed;
            }

            else if (wardtype == "B" && available == "N")
            {
                ClassBBed newbbed = new ClassBBed(wardno, bedno, drate, false);
                return newbbed;
            }

            if (wardtype == "C" && available == "Y")
            {
                ClassCBed newcbed = new ClassCBed(wardno, bedno, drate, true);
                return newcbed;
            }

            else 
            {
                ClassCBed newcbed = new ClassCBed(wardno, bedno, drate, false);
                return newcbed;
            }
            



        }





    }
}
