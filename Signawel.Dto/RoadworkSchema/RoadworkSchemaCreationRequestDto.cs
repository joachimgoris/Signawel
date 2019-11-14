using System.Collections.Generic;
using Signawel.Dto.BBox;

namespace Signawel.Dto.RoadworkSchema
{
    public class RoadworkSchemaCreationRequestDto
    {

        public string Name { get; set; }

        public string ImageId { get; set; }

        public ICollection<BBoxCreationRequestDto> BoundingBoxes { get; set; }

    }
}
