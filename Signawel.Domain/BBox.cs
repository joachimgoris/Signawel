using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Domain
{
    public class BBox : Entity
    {

        public string Name { get; set; }

        public ICollection<BBoxPoint> Points { get; set; }

        public string SchemaId { get; set; }

        public RoadworkSchema Schema { get; set; }

    }
}
