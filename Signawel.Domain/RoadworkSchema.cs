using Signawel.Domain.Enums;
using System.Collections.Generic;

namespace Signawel.Domain
{
    public class RoadworkSchema : Entity
    {

        public string Name { get; set; }

        public string ImageId { get; set; }

        public Image Image { get; set; }

        public ICollection<BBox.BBox> BoundingBoxes { get; set; }

        public RoadworkCategory RoadworkCategory { get; set; }

    }
}
