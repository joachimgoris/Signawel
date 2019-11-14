using System.Collections.Generic;

namespace Signawel.Dto
{
    public class BBoxPutRequestDto
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<BBoxPointPutRequestDto> Points { get; set; }

    }
}