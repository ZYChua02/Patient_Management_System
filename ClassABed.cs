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
    class ClassABed : Bed
    {
        public bool AccompanyingPerson { get; set; }

        public ClassABed(int w, int b, double dr, bool a, bool ap) : base(w, b, dr, a)
        {
            AccompanyingPerson = ap;
        }

        public override double CalculateCharges(string citizenStatus, int noOfDays)
        {
            //Not done?
        }

        public override string ToString()
        {
            return base.ToString() + "\tAccompanying Person: " + AccompanyingPerson;
        }







    }
}
