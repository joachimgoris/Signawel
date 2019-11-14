﻿using System.Collections.Generic;

namespace Signawel.Dto
{
    public class BBoxResponseDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<BBoxPointResponseDto> Points { get; set; }

    }
}