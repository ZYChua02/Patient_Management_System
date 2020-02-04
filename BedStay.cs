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
    class BedStay
    {
        public DateTime StartBedStay { get; set; }

        public DateTime? EndBedStay { get; set; }

        public Bed Bed { get; set; }
        
        public BedStay(DateTime start, Bed b)
        {
            StartBedStay = start;
            Bed = b;
        }

        public override string ToString()
        {
            return "Start of Stay: " + StartBedStay + "\tEnd of stay: " + EndBedStay+ "\tBed" + Bed;
        }
        
    }

}

