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
    //Done by Ryan
    abstract class Patient
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
        public string CitizenStatus { get; set; }
        public string Status { get; set; }
        public Stay Stay { get; set; }

        public Patient(string i, string n, int a, char g, string cs, string stat)
        {
            Id = i;
            Name = n;
            Age = a;
            Gender = g;
            CitizenStatus = cs;
            Status = stat;
        }
        public abstract double CalculateCharges();
        
        public override string ToString()
        {
            return "Id: " + Id + "\tName: " + Name +
                "\tAge: " + Age + "\tGender: "+ Gender +
                "\tCitizen Status: " + CitizenStatus +
                "\tStatus: " + Status + "\tStay: " + Stay;
        }
    }
}
