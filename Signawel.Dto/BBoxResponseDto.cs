using System.Collections.Generic;

namespace Signawel.Dto
{
    public class BBoxResponseDto
    {

        public string Name { get; set; }

        public ICollection<BBoxPointResponseDto> Points { get; set; }

    }
}