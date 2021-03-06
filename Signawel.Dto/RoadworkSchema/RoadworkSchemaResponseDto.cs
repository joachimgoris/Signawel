﻿using System.Collections.Generic;
using Signawel.Domain.Enums;
using Signawel.Dto.BBox;

namespace Signawel.Dto.RoadworkSchema
{
    public class RoadworkSchemaResponseDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageId { get; set; }

        public ICollection<BBoxResponseDto> BoundingBoxes { get; set; }

        public RoadworkCategory RoadworkCategory { get; set; }

    }
}
