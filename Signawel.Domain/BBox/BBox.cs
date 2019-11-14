using System.Collections.Generic;

namespace Signawel.Domain.BBox
{
    public class BBox : Entity
    {

        public string Name { get; set; }

        public ICollection<BBoxPoint> Points { get; set; }

        public string SchemaId { get; set; }

        public RoadworkSchema Schema { get; set; }

    }
}
