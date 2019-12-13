using System;

namespace Signawel.Mobile.Models
{
    public class Report
    {
        public int RoadworkId { get; set; }
        public string UserEmail { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public string IssueType { get; set; }
    }
}
