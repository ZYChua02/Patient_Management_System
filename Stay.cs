//============================================================
// Student Number   : S10196904C, S10196605J
// Student Name	    : NG CHIN TIONG RYAN, CHUA ZHE YU
// Module  Group	: T08 
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace PRG2_T08_Team2
{
    class Stay
    {
        public DateTime AdmittedDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string AdmittedBy { get; set; }
        public bool IsPaid { get; set; }
        public List<MedicalRecord> MedicalRecordList { get; set; } = new List<MedicalRecord>();
        public List<BedStay> BedStayList { get; set; } = new List<BedStay>();
        public Patient Patient { get; set; }

        public Stay(DateTime ad, Patient p)
        {
            AdmittedDate = ad;
            Patient = p;
        }
        public void AddMedicalRecords(MedicalRecord mr)
        {
            MedicalRecordList.Add(mr);
        }
        public void AddBedStay (BedStay bs)
        {
            BedStayList.Add(bs);
        }
        public override string ToString()
        {
            //Incomplete
            return "Admitted Date: " + AdmittedDate +
                "\tDischarge Date: " + DischargeDate +
                "\tAdmitted By: "+ AdmittedBy;
        }

    }
}
