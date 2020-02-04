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
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


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
                    DisplayAdmittedPatients(patientList);
                    AddMedicalRecord(patientList);

                }
                else if (option == "8")
                {
                    Console.WriteLine("Option 8 View Patient Medical Record");
                    DisplayPatients(patientList);
                    //Get patient ID
                    Console.Write("Enter patient ID number: ");
                    string patientid = Console.ReadLine();
                    foreach (Patient p in patientList)
                    {
                        if (p.Id == patientid)
                        {
                            ViewMedicalRecords(p);
                        }
                    }
                }
                else if (option == "9")
                {
                    TransferPatientToAnotherBed(patientList, bedList);
                }
                else if (option == "10")
                {
                    DisplayAdmittedPatients(patientList);
                    DischargePayment(patientList, bedList);
                }
                else if (option == "11")
                {
                    //DisplayCurrencyExchange();
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
        static void InitPatients(List<Patient> patientList)
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
        static void DisplayPatients(List<Patient> patientList, bool filterAdmitted = false)
        {
            Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10} {4, -12} {5, -15}",
                        "Name", "IC No. ", "Age", "Gender", "Citizenship", "Status");
            foreach (Patient pa in patientList)
            {
                if (filterAdmitted == true)
                {
                    if (pa.Status != "Admitted")
                    {
                        continue;
                    }
                }
                Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10} {4, -12} {5, -15}",
                    pa.Name, pa.Id, pa.Age, pa.Gender, pa.CitizenStatus, pa.Status);
            }
        }
        static void RegisterPatient(List<Patient> patientList)
        {
            Console.WriteLine("Option 3. Register Patient");
            Console.Write("Enter Name: ");
            string n = Console.ReadLine();
            Console.Write("Enter Identification Number: ");
            string id = Console.ReadLine();
            if (!readID(id))
            {
                Console.WriteLine("Error!");
                return;
            }
            Console.Write("Enter Age: ");

            string userinput = Console.ReadLine();
            int age;
            if (!int.TryParse(userinput, out age))
            {
                // ERROR
                return;
            }
            Console.Write("Enter Gender [M/F]: ");
            string g = Console.ReadLine().Trim().ToUpper();
            
            if (g != "M" && g != "F") {
                return;
            }

            Console.Write("Enter Citizenship Status [SC/PR/Foreigner]: ");
            string cs = Console.ReadLine().ToUpper().Trim();
            if (cs != "SC" && cs != "PR" && cs != "FOREIGNER")
            {
                return;
            }
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
                Patient p = new Child(id, n, age, Convert.ToChar(g), cs, stat, subsidy);
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
                Patient p = new Adult(id, n, age, Convert.ToChar(g), cs, stat, subsidy);
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
                Patient p = new Senior(id, n, age, Convert.ToChar(g), cs, stat);
                patientList.Add(p);
                using (StreamWriter file = new StreamWriter(@"Patients.csv", true))
                {
                    string line = "\n" + id + ',' + n + ',' + age + ',' + g + ',' + cs + ',' + subsidy;
                    file.Write(line);
                }
                Console.WriteLine($"\n{n} was successfully registered!\n");
            }
        }
        //validation
        public static bool readID(string id)
        {
            
            if (id.Length == 9 && Char.IsLetter(id[0]) && Char.IsLetter(id[8]))
            {
                for (int i = 1; i < id.Length -1; i++)
                {
                    if (!Char.IsDigit(id[i]))
                    {
            
                        return false;
                    }
                }
                return true;
            }
            return false;
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
                int index = Convert.ToInt32(Console.ReadLine());
                //Bed b = SearchBed(bedList, bedNo);
                if (index <=bedList.Count && index > 0)
                {                                                                                                                                                                                                                                                   
                    Bed b = bedList[index - 1];
                    Console.Write("Enter date of admission [DD/MM/YYYY]: ");
                    //Issue with Time at the end
                    DateTime admDate = Convert.ToDateTime(Console.ReadLine()).Date;

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
                        string pTV = Console.ReadLine().ToUpper();
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
                Console.WriteLine("Patient not found!");
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
                if (p.Id == j)
                {
                    return p;
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
                if (p.Status == "Admitted")
                {
                    Console.WriteLine("Admission Date: " + p.Stay.AdmittedDate.ToString("dd/MM/yyyy") + "\n" +
                    "Discharge Date: " + p.Stay.DischargeDate + "\n");


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
                    
                        Console.WriteLine("Ward No: " + p.Stay.BedStayList[i].Bed.WardNo);
                        //ISSUE with this
                        Console.WriteLine("Bed No: " + p.Stay.BedStayList[i].Bed.BedNo);
                        if (p.Stay.BedStayList[i].Bed is ClassABed)
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
                        string startDate = DateToString(p.Stay.BedStayList[i].StartBedStay);
                        Console.WriteLine("Start of Bed Stay: " + startDate);
                        string endDate = DateToString(p.Stay.BedStayList[i].EndBedStay);
                        Console.WriteLine("End of Bed Stay: " + endDate);
                        //ToString() EndBedStay?
                    
                }
            }
        }
        static string DateToString(DateTime? date)
        {
            if (date != null)
            {
                return date.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                return "";
            }
        }

        static void TransferPatientToAnotherBed(List<Patient> patientList, List<Bed> bedList)
        {

            DisplayPatients(patientList, true);
            Console.Write("Enter patient ID number: ");
            string pNo = Console.ReadLine();
            Patient p = SearchPatient(patientList, pNo);
            Stay s = p.Stay;
            DisplayAllBeds(bedList);
            Console.Write("Select Bed to transfer to: ");
            int newBNo = Convert.ToInt32(Console.ReadLine());
            if (newBNo <= bedList.Count && newBNo > 0)
            {
                Bed b = bedList[newBNo - 1];
            
                Console.Write("Date of transfer [DD/MM/YYYY]: ");
                DateTime transferDate = Convert.ToDateTime(Console.ReadLine());

                b.Available = false;
                for (int i = 0; i < p.Stay.BedStayList.Count; i++)
                {
                    p.Stay.BedStayList[i].EndBedStay = transferDate;
                }
                BedStay transferBed = new BedStay(transferDate, b);
                s.AddBedStay(transferBed);
                Console.WriteLine(p.Name + " will be transferred to Ward " + b.WardNo +
                    " Bed " + b.BedNo + " on " + transferDate.ToString("dd/MM/yyyy") + ".\n");
            }
        }
        static void DisplayCurrencyExchange()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://ictonejourney.com");

                //HTTP GET: Goes to specified URI
                Task<HttpResponseMessage> responseTask = client.GetAsync("/api/books");

                //wait for task to complete execution
                responseTask.Wait();

                //get JSON data and store in HTTPResponseMessage Type. 
                HttpResponseMessage result = responseTask.Result;

                //Boolean to check that response was successful (true) OR
                //unsuccessful (false)
                if (result.IsSuccessStatusCode)
                {
                    //Serialise to string
                    Task<string> readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
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
        static void DisplayAllBeds(List<Bed> bList)
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
                    Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", counter, "C", cbed.WardNo, cbed.BedNo, cbed.DailyRate, cbed.Available);
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

            else
            {
                Console.WriteLine("Bed added unsccesffuly. Please try again");

            }
        }


        //For option 7
        static void AddMedicalRecord(List<Patient> patientlist)
        {
            //prompt user to enter patient ID number
            Console.Write("Enter patient ID number: ");
            string patientid = Console.ReadLine();
            //Retrieve the patient
            foreach (Patient p in patientlist)
            {
                if (p.Id == patientid)
                {

                    Console.Write("Patient temperature: ");
                    double temperature = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Please enter patient observation: ");
                    string diagonsis = Console.ReadLine();
                    DateTime today = DateTime.Now;
                    MedicalRecord newrecord = new MedicalRecord(diagonsis, temperature, today);
                    p.Stay.AddMedicalRecords(newrecord);
                    Console.WriteLine("Medical Record added successfully.");
                    break;
                }
                else if (p.Id != patientid)
                {
                    Console.WriteLine("Invalid Patient. Try again!");
                    break;
                }

                else
                {
                    Console.WriteLine("Medical Record added unsuccessfully.");
                    break;
                }





            }
        }

        //For option 8 
        static void ViewMedicalRecords(Patient p)
        {
            int counter = 1;
            Console.WriteLine("Name of patient: {0}", p.Name);
            Console.WriteLine("ID number: {0}", p.Id);
            Console.WriteLine("Citizenship status: {0}", p.CitizenStatus);
            Console.WriteLine("Gender: {0}", p.Gender);
            Console.WriteLine("Status: {0}", p.Status);
            if (p.Stay == null)
            {
                Console.WriteLine("This patient is not admitted. Please try again");
            }
            else
            {
                Console.WriteLine("=====Stay=====");
                Console.WriteLine("Admission date: {0}", p.Stay.AdmittedDate);
                foreach (MedicalRecord m in p.Stay.MedicalRecordList)
                {
                    Console.WriteLine("======Record #{0} =======", counter);
                    Console.WriteLine("Date/Time: {0}", m.DatetimeEntered);
                    Console.WriteLine("Temperature: {0} deg. cel.", m.Temperature);
                    Console.WriteLine("Diganosis: {0}", m.Diagnosis);
                    Console.WriteLine();
                    counter = counter + 1;

                }
            }



        }
        //Disply Admitted Patients
        static void DisplayAdmittedPatients(List<Patient> patientList)
        {
            Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10} {4, -12} {5, -15}",
                        "Name", "IC No. ", "Age", "Gender", "Citizenship", "Status");
            foreach (Patient pa in patientList)
            {
                if (pa.Status == "Admitted")
                {
                    Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10} {4, -12} {5, -15}",
                   pa.Name, pa.Id, pa.Age, pa.Gender, pa.CitizenStatus, pa.Status);
                }

            }
        }
        static void DischargePayment(List<Patient> patientlist, List<Bed> bList)
        {
            double totalcharges = 0;
            int counter = 1;
            Console.Write("Enter patient ID number to discharge: ");
            string patientid = Console.ReadLine();
            Console.Write("Date of discharge (DD/MM/YYYY): ");
            DateTime disdate = Convert.ToDateTime(Console.ReadLine()).Date;
            
            foreach(Patient p in patientlist)
            {
                if(p.Id == patientid)
                {
                    Console.WriteLine("Name of patient: {0}", p.Name);
                    Console.WriteLine("ID number: {0}", p.Id);
                    Console.WriteLine("Citizenship status: {0}", p.CitizenStatus);
                    Console.WriteLine("Gender: {0}", p.Gender);
                    Console.WriteLine("Status: {0}", p.Status);
                    Console.WriteLine();
                    Console.WriteLine("=====Stay=====");
                    Console.WriteLine("Admission date: {0}", p.Stay.AdmittedDate);
                    p.Stay.DischargeDate = disdate;
                    Console.WriteLine("Discharge date: {0}", p.Stay.DischargeDate);
                    if (p.Stay.IsPaid == true)
                    {
                        Console.WriteLine("Payment status: Unpaid");
                    }

                    else if (p.Stay.IsPaid == false)
                    {
                        Console.WriteLine("Payment status: Unpaid");
                    }
                    
                    
                    foreach(BedStay bes in p.Stay.BedStayList)
                    {
                      
                        Console.WriteLine("======Bed #{0}=======", counter);
                        Console.WriteLine("Ward Number: {0}", bes.Bed.WardNo);
                        Console.WriteLine("Start of bed stay: ", p.Stay.AdmittedDate);
                        Console.WriteLine("End of bed stay: {0}", bes.EndBedStay);
                       
                        
                        if (bes.Bed is ClassABed)
                        {
                            ClassABed abed = (ClassABed)bes.Bed;
                            Console.WriteLine("Ward Class: A");
                            Console.WriteLine("Accompanying Person: {0} ", abed.AccompanyingPerson);
                        }

                        else if (bes.Bed is ClassBBed)
                        {
                            ClassBBed bbed = (ClassBBed)bes.Bed;
                            Console.WriteLine("Ward Class: B");
                            Console.WriteLine("Air con: {0}", bbed.AirCon);
                        }

                        else if (bes.Bed is ClassCBed)
                        {
                            ClassCBed cbed = (ClassCBed)bes.Bed;
                            Console.WriteLine("Ward Class: C");
                            Console.WriteLine("Portable TV {0}", cbed.PortableTv);
                        }
                        Console.WriteLine();
                        int staydays = (Convert.ToDateTime(bes.EndBedStay) - Convert.ToDateTime(bes.StartBedStay)).Days;
                        Console.WriteLine("Number of days stayed: {0}", staydays);
                        bes.Bed.Available = true;

                        counter++;

                    }
                    Console.WriteLine("============");
                    Console.WriteLine("Total Charges pending: {0}", )

                    
                    
                   
                }
                

            }
           


        }
        static void charges()
        {

        }

    } 
}



