using System.Collections.Generic;
using Signawel.Domain.Enums;
using Signawel.Dto.BBox;

namespace Signawel.Dto.RoadworkSchema
{
    public class RoadworkSchemaCreationRequestDto
    {

        public string Name { get; set; }

        public ICollection<BBoxCreationRequestDto> BoundingBoxes { get; set; }

        public RoadworkCategory RoadworkCategory { get; set; }

    }
}
