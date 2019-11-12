using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Dto
{
    public class RoadworkSchemaResponseDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageId { get; set; }

        public ICollection<BBoxResponseDto> BoundingBoxes { get; set; }

    }
}
