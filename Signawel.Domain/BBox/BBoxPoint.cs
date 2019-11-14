namespace Signawel.Domain.BBox
{
    public class BBoxPoint : Entity
    {

        public double X { get; set; }

        public double Y { get; set; }

        public string BBoxId { get; set; }

        public BBox BBox { get; set; }

        public int Order { get; set; }

    }
}