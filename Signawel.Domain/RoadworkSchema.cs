using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Domain
{
    public class RoadworkSchema : Entity
    {

        public string Name { get; set; }

        public string ImageId { get; set; }

        public Image Image { get; set; }

        public ICollection<BBox> BoundingBoxes { get; set; }

    }
}
