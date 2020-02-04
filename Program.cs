//============================================================
// Student Number   : S10196904C, S10196605J
// Student Name	    : NG CHIN TIONG RYAN, CHUA ZHE YU
// Module  Group	: T08 
//============================================================

/*====================== T O D O ============================
Ryan   : Option 5 & 6 Problems: Admission Date has Time,  Bed Number Wrong
        2.6 Time EndBedStay has issues.
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

            //Medical record list
            List<MedicalRecord> MedicalRecordList = new List<MedicalRecord>();

            //Stay list
            List<Stay> StayList = new List<Stay>();

            InitPatients(patientList);
            InitBedList(bedList);
            while (option != "0")
            {
                DisplayMenu();
                //Read and store user input
                Console.Write("Enter your option: ");
                option = Console.ReadLine();

                if (option == "1")
                {
                    Console.WriteLine("Option 1. View All Patients");
                    DisplayPatients(patientList);
                }
                else if (option == "2")
                {
                    
                    Console.WriteLine("Option 2. View All Beds");
                    DisplayAllBeds(bedList);
                }
                else if (option == "3")
                {
                    RegisterPatient(patientList);
                }
                else if (option == "4")
                {
                    AddBed(bedList);
                }
                else if (option == "5")
                {
                    DisplayPatients(patientList);
                    RegisterHospitalStay(patientList, bedList);

                }
                else if (option == "6")
                {
                    RetrievePatientDetails(patientList, bedList);
                }
                else if (option == "7")
                {
                    Console.WriteLine("Option 7 Add Medical record entry");
                    DisplayPatients(patientList);
                    AddMedicalRecord(patientList, StayList);
                    
                }
                else if (option == "8")
                {
                    Console.WriteLine("Option 8 View Patient Medical Record");
                    DisplayPatients(patientList);
                    ViewMedicalRecords(patientList, StayList);
                }
                else if (option == "9")
                {
                    TransferPatientToAnotherBed(patientList, bedList);
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

                
            }
            Console.ReadKey();
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
        static void InitPatients(List <Patient> patientList)
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
                        //                    Nric       Name     Age  Gender                   Cts  status   CDA/Medisave
                        Patient p = new Child(pData[1], pData[0], age, Convert.ToChar(pData[3]), cs, stat, Convert.ToDouble(pData[5]));
                        patientList.Add(p);
                    }
                    else if (cs == "Foreigner" || cs == "PR" || cs == "pr")
                    {
                        Patient p = new Child(pData[1], pData[0], age, Convert.ToChar(pData[3]), cs, stat, 0.0);
                        patientList.Add(p);
                    }
                }
                else if (age <= 64)
                {
                    if (cs == "SC" || cs == "sc" || cs == "PR" || cs == "pr") //Validaton for casing
                    {
                        Patient p = new Adult(pData[1], pData[0], age, Convert.ToChar(pData[3]), cs, stat, Convert.ToDouble(pData[5]));
                        patientList.Add(p);
                    }
                    else if (cs == "Foreigner")
                    {
                        Patient p = new Adult(pData[1], pData[0], age, Convert.ToChar(pData[3]), cs, stat, 0.0);
                        patientList.Add(p);
                    }
                }
                else if (age >= 65)
                {
                    Patient p = new Senior(pData[1], pData[0], age, Convert.ToChar(pData[3]), cs, stat);
                    patientList.Add(p);
                }
            }
        }
        static void DisplayPatients(List<Patient> patientList)
        {
            Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10} {4, -12} {5, -15}",
                        "Name", "IC No. ", "Age", "Gender", "Citizenship", "Status");
            foreach (Patient pa in patientList)
            {
                Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10} {4, -12} {5, -15}",
                    pa.Name, pa.Id, pa.Age, pa.Gender, pa.CitizenStatus, pa.Status);
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
            string cs = Console.ReadLine().ToUpper();
            string stat = "Registered";
            double subsidy = 0.0;
            
            if (age >= 0 && age <= 12)
            {
                // only if condition is met, subsidy will be updated accordingly
                // if not met, will remain as 0.
                if (cs == "SC")
                {
                    Console.Write("Enter CDA Balance: ");
                    subsidy = Convert.ToDouble(Console.ReadLine());
                }
                Patient p = new Child(id, n, age, g, cs, stat, subsidy);
                patientList.Add(p);
                using (StreamWriter file = new StreamWriter(@"Patients.csv", true))
                {
                    string line = "\n" + id + ',' + n + ',' + age + ',' + g + ',' + cs + ',' + subsidy;
                    file.Write(line);
                }
                Console.WriteLine($"\n{n} was successfully registered!\n");
            }
            else if (age <= 64)
            {
                if (cs == "SC" || cs == "PR")
                {
                    Console.Write("Enter Medisave Balance: ");
                    subsidy = Convert.ToDouble(Console.ReadLine());
                }
                Patient p = new Adult(id, n, age, g, cs, stat, subsidy);
                patientList.Add(p);
                using (StreamWriter file = new StreamWriter(@"Patients.csv", true))
                {
                    string line = "\n" + id + ',' + n + ',' + age + ',' + g + ',' + cs + ',' + subsidy;
                    file.Write(line);
                }
                Console.WriteLine($"\n{n} was successfully registered!\n");
            }
            else if (age >= 65)
            {
                Patient p = new Senior(id, n, age, g, cs, stat);
                patientList.Add(p);
                using (StreamWriter file = new StreamWriter(@"Patients.csv", true))
                {
                    string line = "\n" + id + ',' + n + ',' + age + ',' + g + ',' + cs + ',' + subsidy;
                    file.Write(line);
                }
                Console.WriteLine($"\n{n} was successfully registered!\n");
            }
        }

        static void RegisterHospitalStay(List<Patient> patientList, List<Bed> bedList)
        {
            //Prompt for and read patient NRIC number
            Console.Write("Enter Patient ID Number: ");
            string pNo = Console.ReadLine();
                       
            Patient p = SearchPatient(patientList, pNo);
           
            if (p != null)
            {
                DisplayAllBeds(bedList);
                //Prompt for and read preferred bed
                Console.Write("Select bed to stay: ");
                int bedNo = Convert.ToInt32(Console.ReadLine());
                Bed b = SearchBed(bedList, bedNo);
                Console.WriteLine("Search Successful {0}", b.WardNo);
                if (b != null) { 
                    Console.Write("Enter date of admission [DD/MM/YYYY]: ");
                    //Issue with Time at the end
                    DateTime admDate= Convert.ToDateTime(Console.ReadLine()).Date;

                    Stay s = new Stay(admDate, p);

                    //initialisation of BedStay to prevent errors
                    BedStay bs = new BedStay(admDate, b);
                    if (b is ClassABed)
                    {
                        Console.Write("Any accompanying guest? (Additional $100 per day) [Y/N]: ");
                        string accGuest = Console.ReadLine().ToUpper();
                        //ClassABed cab = (ClassABed)b;
                        ClassABed clab = new ClassABed(b.WardNo, b.BedNo, b.DailyRate, b.Available);
                        clab.AccompanyingPerson = CheckOption(accGuest);
                        bs = new BedStay(admDate, clab);
                    }
                    else if (b is ClassBBed)
                    {
                        Console.Write("Do you want to upgrade to an Air-Conditioned variant? (Additional $50 per week) [Y/N]: ");
                        string ac = Console.ReadLine().ToUpper();
                        //ClassBBed cbb = (ClassBBed)b;
                        ClassBBed clbb = new ClassBBed(b.WardNo, b.BedNo, b.DailyRate, b.Available);
                        clbb.AirCon = CheckOption(ac);
                        bs = new BedStay(admDate, clbb);
                    }
                    else if (b is ClassCBed)
                    {
                        Console.Write("Do you want to rent a portable TV? (One-Time Cost of $30) [Y/N]: ");
                        string pTV = Console.ReadLine();
                        //ClassCBed ccb = (ClassCBed)b;
                        ClassCBed clcb = new ClassCBed(b.WardNo, b.BedNo, b.DailyRate, b.Available);
                        clcb.PortableTv = CheckOption(pTV);
                        bs = new BedStay(admDate, clcb);
                    }
                    s.AddBedStay(bs);
                    p.Stay = s;
                    p.Status = "Admitted";
                    b.Available = false;
                    Console.WriteLine("Stay registration successful!");
                }
                else
                {
                    Console.WriteLine("Stay registration unsuccessful!");
                }
            }
            else
            {
                //Console.WriteLine("Patient not found!");
                Console.WriteLine(p);
            }
        }
        static bool CheckOption(string opt)
        {
            bool agreeToOption = false;
            while (!agreeToOption)
            {
                if (string.Equals(opt, "Y"))
                {
                    return true;
                }

                if (string.Equals(opt, "N"))
                {
                    return false;
                }
                Console.WriteLine("Invalid Input. Please try again!");
            }
            return false;
        }
        static Patient SearchPatient(List<Patient> patientList, string j)
        {
            foreach (Patient p in patientList)
            {
                if(p.Id == j)
                {
                    return p;
                }
            }
            return null;
        }
        static Bed SearchBed(List<Bed> bedList, int j)
        {
            for (int i = 0; i < bedList.Count; i++)
            {
                if (bedList[i+1].BedNo == j)
                {
                    return bedList[i+1];
                }
            }
            return null;
        }
        static void RetrievePatientDetails(List<Patient> patientList, List<Bed> bedList)
        {
            DisplayPatients(patientList);
            //Prompt for and read patient NRIC number
            Console.Write("Enter Patient ID Number: ");
            string pNo = Console.ReadLine();
            Patient p = SearchPatient(patientList, pNo);
            if (p != null)
            {
                Console.WriteLine("Name of Patient: " + p.Name + "\n" + 
                    "ID Number: " + p.Id + "\n" + "Citizenship Status: " + p.CitizenStatus + "\n" +
                    "Gender: " + p.Gender + "\n" + "Status: " + p.Status + "\n\n");
                if(p.Status == "Admitted")
                {
                    Console.WriteLine("Admission Date: " + p.Stay.AdmittedDate.ToString("dd/MM/yyyy") + "\n" +
                    "Discharge Date: " + p.Stay.DischargeDate + "\n");
                    //ToString() Discharge Date?

                    if (p.Stay.IsPaid == true)
                    {
                        Console.WriteLine("Payment Status: Paid");
                    }
                    else
                    {
                        Console.WriteLine("Payment Status: Unpaid");
                    }
                    Console.WriteLine("======================\n");
                }
                for (int i = 0; i < p.Stay.BedStayList.Count; i++)
                {
                    if (p.Stay.AdmittedDate == p.Stay.BedStayList[i].StartBedStay)
                    {
                        Console.WriteLine("Ward No: " + p.Stay.BedStayList[i].Bed.WardNo);
                        //ISSUE with this
                        Console.WriteLine("Bed No: " + p.Stay.BedStayList[i].Bed.BedNo);
                        if(p.Stay.BedStayList[i].Bed is ClassABed)
                        {
                            Console.WriteLine("Ward Class: A");
                        }
                        else if (p.Stay.BedStayList[i].Bed is ClassBBed)
                        {
                            Console.WriteLine("Ward Class: B");
                        }
                        else if (p.Stay.BedStayList[i].Bed is ClassCBed)
                        {
                            Console.WriteLine("Ward Class: C");
                        }
                        Console.WriteLine("Start of Bed Stay: " + p.Stay.BedStayList[i].StartBedStay.ToString("dd/MM/yyyy"));
                        Console.WriteLine("End of Bed Stay: " + p.Stay.BedStayList[i].EndBedStay);
                    }
                }
            }
        }
        static void TransferPatientToAnotherBed(List<Patient> patientList, List<Bed> bedList)
        {
            foreach (Patient pa in patientList)
            {
                if (pa.Status == "Admitted")
                {
                    Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10} {4, -12} {5, -15}",
                        "Name", "IC No. ", "Age", "Gender", "Citizenship", "Status");
                    Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10} {4, -12} {5, -15}",
                    pa.Name, pa.Id, pa.Age, pa.Gender, pa.CitizenStatus, pa.Status);
                    Console.Write("Enter Patient ID Number: ");

                    //Need to make use of this?
                    string pNo = Console.ReadLine();
                    Stay s = pa.Stay;
                    DisplayAllBeds(bedList);
                    Console.Write("Select Bed to transfer to: ");
                    int newBNo = Convert.ToInt32(Console.ReadLine());
                    Bed b = SearchBed(bedList, newBNo);
                    Console.Write("Date of transfer [DD/MM/YYYY]: ");
                    DateTime transferDate = Convert.ToDateTime(Console.ReadLine());
                    b.Available = false;
                    for (int i = 0; i < pa.Stay.BedStayList.Count; i++)
                    {
                        pa.Stay.BedStayList[i].EndBedStay = transferDate;
                    }
                    BedStay transferBed = new BedStay(transferDate, b);
                    s.AddBedStay(transferBed);
                    Console.WriteLine(pa.Name + " will be transferred to Ward " + b.WardNo + 
                        " Bed " + b.BedNo + " on " + transferDate.ToString("dd/MM/yyyy") + ".\n");
                }
            }
        }

        // Zhe Yu's Methods //

        //For option 2

        static void InitBedList(List<Bed> bList)
        {
            string[] csvlines = File.ReadAllLines(@"beds.csv");
            for (int i = 1; i < csvlines.Length; i++)
            {
                string[] bedsdata = csvlines[i].Split(',');
                //bedsdata[0] : class of bed
                //bedsdata[1] : wardno
                //bedsdata[2] : BedNo
                //bedsdata[3] : Availabiity (Yes/No)
                //bedsdata[4] : Daily Rate

                if (bedsdata[0] == "A") //To check which class is the bed
                {
                    if (bedsdata[3] == "Yes") //To check the availabity of the bed
                    {
                        bList.Add(new ClassABed(Convert.ToInt32(bedsdata[1]), Convert.ToInt32(bedsdata[2]), Convert.ToDouble(bedsdata[4]), true));
                    }

                    else if (bedsdata[3] == "No") //To check the availabity of the bed
                    {
                        bList.Add(new ClassABed(Convert.ToInt32(bedsdata[1]), Convert.ToInt32(bedsdata[2]), Convert.ToDouble(bedsdata[4]), false));
                    }
                }



                else if (bedsdata[0] == "B")
                {
                    if (bedsdata[3] == "Yes") //To check the availability of the bed
                    {
                        bList.Add(new ClassBBed(Convert.ToInt32(bedsdata[1]), Convert.ToInt32(bedsdata[2]), Convert.ToDouble(bedsdata[4]), true));
                    }

                    else if (bedsdata[3] == "No") //To check the availability of the bed
                    {
                        bList.Add(new ClassBBed(Convert.ToInt32(bedsdata[1]), Convert.ToInt32(bedsdata[2]), Convert.ToDouble(bedsdata[4]), false));
                    }

                }

                else if (bedsdata[0] == "C")
                {
                    if (bedsdata[3] == "Yes") //To check the availabity of the bed
                    {
                        bList.Add(new ClassCBed(Convert.ToInt32(bedsdata[1]), Convert.ToInt32(bedsdata[2]), Convert.ToDouble(bedsdata[4]), true));
                    }

                    else if (bedsdata[3] == "No") //To check the availabity of the bed
                    {
                        bList.Add(new ClassCBed(Convert.ToInt32(bedsdata[1]), Convert.ToInt32(bedsdata[2]), Convert.ToDouble(bedsdata[4]), false));
                    }
                }


            }
        }
        static void DisplayAllBeds(List <Bed> bList)
        {
            Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10} ", "No", "Type", "Ward No", "Bed No", "Daily Rate", "Available");
            int counter = 1;//for the no of beds
            foreach (Bed b in bList)
            {
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
                counter++;

            }
        }
        
        //For Option 4
        static void AddBed(List<Bed> bList)
        {
            //inputs
            Console.Write("Enter Ward Type[A/B/C]: ");
            string wardtype = Console.ReadLine();
            Console.Write("Enter Ward No: ");
            int wardno = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Bed No: ");
            int bedno = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Daily Rate:$ ");
            double drate = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter Available[Y/N]: ");
            string available = Console.ReadLine();
            string bedtrue = "Yes";
            string bedfalse = "No";
            Console.WriteLine();
            Console.WriteLine("Bed added successfully");
            
            if (wardtype == "A" && available == "Y") //Find out the class and availability
            {
                ClassABed newabedtrue = new ClassABed(wardno, bedno, drate, true);
                bList.Add(newabedtrue);
                using (StreamWriter file = new StreamWriter(@"beds.csv", true)) //append csv file
                {
                    string bed = "\n" + wardtype + ',' + wardno + ',' + bedno + ',' + bedtrue + ',' + drate;
                    file.Write(bed);
                }
                
            }

            else if (wardtype == "A" && available == "N") //if availability is no
            {
                ClassABed newabedfalse = new ClassABed(wardno, bedno, drate, false);
                bList.Add(newabedfalse);
                using (StreamWriter file = new StreamWriter(@"beds.csv", true)) //append csv file
                {
                    string bed = "\n" + wardtype + ',' + wardno + ',' + bedno + ',' + bedfalse + ',' + drate;
                    file.Write(bed);
                }

            }

            if (wardtype == "B" && available == "Y")
            {
                ClassBBed newbbedtrue = new ClassBBed(wardno, bedno, drate, true);
                bList.Add(newbbedtrue);
                using (StreamWriter file = new StreamWriter(@"beds.csv", true)) //append csv file
                {
                    string bed = "\n" + wardtype + ',' + wardno + ',' + bedno + ',' + bedtrue + ',' + drate;
                    file.Write(bed);
                }
            }

            else if (wardtype == "B" && available == "N")
            {
                ClassBBed newbbedfalse = new ClassBBed(wardno, bedno, drate, false);
                bList.Add(newbbedfalse);
                using (StreamWriter file = new StreamWriter(@"beds.csv", true)) //append csv file
                {
                    string bed = "\n" + wardtype + ',' + wardno + ',' + bedno + ',' + bedfalse + ',' + drate;
                    file.Write(bed);
                }
            }

            if (wardtype == "C" && available == "Y")
            {
                ClassCBed newcbedtrue = new ClassCBed(wardno, bedno, drate, true);
                bList.Add(newcbedtrue);
                using (StreamWriter file = new StreamWriter(@"beds.csv", true)) //append csv file
                {
                    string bed = "\n" + wardtype + ',' + wardno + ',' + bedno + ',' + bedtrue + ',' + drate;
                    file.Write(bed);
                }
            }

            else if (wardtype == "C" && available == "N")
            {
                ClassCBed newcbedfalse = new ClassCBed(wardno, bedno, drate, false);
                bList.Add(newcbedfalse);
                using (StreamWriter file = new StreamWriter(@"beds.csv", true)) //append csv file
                {
                    string bed = "\n" + wardtype + ',' + wardno + ',' + bedno + ',' + bedfalse + ',' + drate;
                    file.Write(bed);
                }
            }
        }









        //For option 7
        static void AddMedicalRecord(List<Patient> patientlist, List<Stay> StayList)
        {
            //Initalise Patient
            Patient MedRecord = null;
            //prompt user to enter patient ID number
            Console.Write("Enter patient ID number: ");
            string patientid = Console.ReadLine();
            //Retrieve the patient
            foreach(Patient p in patientlist)
            {
                if (p.Id == patientid)
                {
                    MedRecord = p;
                    Console.Write("Patient temperature: ");
                    double temperature = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Please enter patient observation: ");
                    string diagonsis = Console.ReadLine();
                    DateTime today = DateTime.Now;
                    MedicalRecord newrecord = new MedicalRecord(diagonsis, temperature, today);
                    Stay s = new Stay(today, MedRecord);
                    StayList.Add(s);
                    s.AddMedicalRecords(newrecord);
                    


                }
                break;




            }
           


        }

        //For option 8 
        static void ViewMedicalRecords(List<Patient> patientlist, List<Stay> StayList)
        {
            Console.Write("Enter patient ID number: ");
            string patientid = Console.ReadLine();
            Patient viewmedrecord = null;
            int counter = 1;
            foreach (Patient p in patientlist)
            {
                if (p.Id == patientid)
                {
                    viewmedrecord = p;
                    Console.WriteLine("Name of patient: {0}", viewmedrecord.Name);
                    Console.WriteLine("ID number: {0}", viewmedrecord.Id);
                    Console.WriteLine("Citizenship status: {0}", viewmedrecord.CitizenStatus);
                    Console.WriteLine("Gender: {0}", viewmedrecord.Gender);
                    Console.WriteLine("Status: {0}", viewmedrecord.Status);
                    foreach (Stay s in StayList)
                    {
                        Console.WriteLine("=====Stay=====");
                        Console.WriteLine("Admission date: {0}", s.AdmittedDate);
                        Console.WriteLine("Discharge date: ", s.DischargeDate);
                        foreach (MedicalRecord m in s.MedicalRecordList)
                        {
                            Console.WriteLine("======Record #{0} =======", counter);
                            Console.WriteLine("Date/Time: {0}", m.DatetimeEntered);
                            Console.WriteLine("Temperature: {0} deg. cel.", m.Temperature);
                            Console.WriteLine("Diganosis: {0}", m.Diagnosis);
                            Console.WriteLine();
                            counter = counter + 1;
                            
                        }
                    }

                break;
                }
               
            }
           
            
          

        }

    }
}
