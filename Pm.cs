using System;
using System.Collections.Generic;
using System.Text;

namespace PRG2_T08_Team2
{
    public class RootObject
    {
        public List<RegionMetadata> region_metadata { get; set; }
        public List<Item> items { get; set; }
        public ApiInfo api_info { get; set; }
    }
    public class LabelLocation
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class RegionMetadata
    {
        public string name { get; set; }
        public LabelLocation label_location { get; set; }
    }

    public class Pm25OneHourly
    {
        public int west { get; set; }
        public int east { get; set; }
        public int central { get; set; }
        public int south { get; set; }
        public int north { get; set; }
    }

    public class Readings
    {
        public Pm25OneHourly pm25_one_hourly { get; set; }
    }

    public class Item
    {
        public DateTime timestamp { get; set; }
        public DateTime update_timestamp { get; set; }
        public Readings readings { get; set; }
    }

    public class ApiInfo
    {
        public string status { get; set; }
    }

    
}
