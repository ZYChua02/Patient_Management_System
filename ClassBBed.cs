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
    class ClassBBed:Bed
    {
        public bool AirCon { get; set; }

        public ClassBBed(int w, int b, double dr, bool a) : base(w, b, dr, a)
        {
          
        }

        public override double CalculateCharges(string citizenStatus, int noOfDays)
        {
            return DailyRate * noOfDays;
        }

        public override string ToString()
        {
            return base.ToString() + "\tAir Con: " + AirCon;
        }
    }
}
