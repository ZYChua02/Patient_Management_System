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
    class Senior : Patient
    {
        public Senior(string i, string n, int a, char g, string cs, string stat)
            :base(i, n, a, g, cs, stat)
        {

        }
        public override double CalculateCharges()
        {
            //NOT DONE: Need to associate with bedStayList
            return CalculateCharges() * .5;


        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
