namespace Signawel.Mobile.Models
{
    public class RoadWork
    {
        public int GipodId { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public bool ImportantHindrance { get; set; }
        public Coordinates Coordinate { get; set; }
        public string Detail { get; set; }
        public string[] Cities { get; set; }
        public string LatestUpdate { get; set; }
        public double DistanceToDevice { get; set; }

    }

        
}
