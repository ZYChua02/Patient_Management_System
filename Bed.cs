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
    abstract class Bed
    {
        public int WardNo { get; set; }

        public int BedNo { get; set; }

        public double DailyRate { get; set; }

        public bool Available { get; set; }

        public Bed(int w, int b, double dr, bool a)
        {
            WardNo = w;
            BedNo = b;
            DailyRate = dr;
            Available = a;
        }

        public abstract double CalculateCharges(string citizenStatus, int noOfDays);
        

        public override string ToString()
        {
            return "Ward No: " + WardNo + "\tBedNo: " + BedNo + "\tDaily rate: " + DailyRate + "\tAvailbility: " + Available;
        }

    }
}
