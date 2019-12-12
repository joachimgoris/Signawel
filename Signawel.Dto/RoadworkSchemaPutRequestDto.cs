using Signawel.Domain.Enums;
using System.Collections.Generic;

namespace Signawel.Dto
{
    public class RoadworkSchemaPutRequestDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageId { get; set; }

        public ICollection<BBoxPutRequestDto> BoundingBoxes { get; set; }

        public RoadworkCategory RoadworkCategory { get; set; }

    }
}
