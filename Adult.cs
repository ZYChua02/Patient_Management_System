﻿//============================================================
// Student Number   : S10196904C, S10196605J
// Student Name	    : NG CHIN TIONG RYAN, CHUA ZHE YU
// Module  Group	: T08 
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace PRG2_T08_Team2
{
    class Adult:Patient
    {
        public double MedisaveBalance { get; set; }
        public Adult(string i, string n, int a, char g, string cs, string stat, double mb) 
            : base(i, n, a, g, cs, stat)
        {
            MedisaveBalance = mb;
        }
        public new double CalculateCharges()
        {
            return base.CalculateCharges() - MedisaveBalance;
        }

        public override string ToString()
        {
            return base.ToString() + "\tMedisave Balance:" + MedisaveBalance;
        }
    }
}
