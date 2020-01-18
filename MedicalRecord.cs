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
    class MedicalRecord
    {
        public string Diagnosis { get; set; }

        public double Temperature { get; set; }

        public DateTime DatetimeEntered { get; set; }

        public MedicalRecord(string d, double t, DateTime dte)
        {
            Diagnosis = d;
            Temperature = t;
            DatetimeEntered = dte;
        }

        public override string ToString()
        {
            return "Diagnosis: " + Diagnosis + "\tTemperature " + Temperature + "\tDate entered: " + DatetimeEntered;
        }
    }
}
