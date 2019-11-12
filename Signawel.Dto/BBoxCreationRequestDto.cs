using System.Collections.Generic;

namespace Signawel.Dto
{
    public class BBoxCreationRequestDto
    {

        public string Name { get; set; }

        public ICollection<BBoxPointCreationRequestDto> Points { get; set; }

    }
}