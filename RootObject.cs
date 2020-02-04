﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PRG2_T08_Team2
{
    class RootObject
    {
        public bool Success { get; set; }
        public Result Result { get; set; }

        public RootObject(bool s, Result r)
        {
            Success = s;
            Result = r;
        }
        public override string ToString()
        {
            return "Sucess: " + Success + "\tResult: " + Result;
        }
    }
}