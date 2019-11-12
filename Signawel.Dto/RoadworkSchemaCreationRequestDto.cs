using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Dto
{
    public class RoadworkSchemaCreationRequestDto
    {

        public string Name { get; set; }

        public ICollection<BBoxCreationRequestDto> BoundingBoxes { get; set; }

    }
}
