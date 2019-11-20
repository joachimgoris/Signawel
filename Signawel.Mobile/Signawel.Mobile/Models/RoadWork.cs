using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Mobile.Models
{
    public class RoadWork
    {
        public int gipodId { get; set; }
        public string owner { get; set; }
        public string description { get; set; }
        public string startDateTime { get; set; }
        public string endDateTime { get; set; }
        public bool importantHindrance { get; set; }
        public Coordinates coordinate { get; set; }
        public string detail { get; set; }
        public string[] cities { get; set; }
        public string latestUpdate { get; set; }
        public double distanceToDevice { get; set; }

    }

        
}
