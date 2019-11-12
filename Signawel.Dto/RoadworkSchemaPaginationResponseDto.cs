using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Dto
{
    public class RoadworkSchemaPaginationResponseDto
    {

        public int Total { get; set; }

        public IList<RoadworkSchemaResponseDto> Schemas { get; set; }

    }
}
