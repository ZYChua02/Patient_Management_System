using System;
using System.Collections.Generic;
using System.Text;

namespace PRG2_T08_Team2
{
    class Result
    {
        public List<string> Resource_id { get; set; } = new List<string>();
        public int Limit { get; set; } 
        public string Total { get; set; }
        public List<Record> Records { get; set; } = new List<Record>();
        public Result(int l, string t)
        {
            Limit = l;
            Total = t;
        }
        public override string ToString()
        {
            return "Limit: " + Limit + "Total: " + Total;
        }
    }
}
