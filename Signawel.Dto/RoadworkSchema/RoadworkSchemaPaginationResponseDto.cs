using System.Collections.Generic;

namespace Signawel.Dto.RoadworkSchema
{
    public class RoadworkSchemaPaginationResponseDto
    {

        public int Total { get; set; }

        public IList<RoadworkSchemaResponseDto> Schemas { get; set; }

    }
}
