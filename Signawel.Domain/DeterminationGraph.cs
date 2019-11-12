using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Domain
{
    public class DeterminationGraph : Entity
    {

        public string StartId { get; set; }

        public DeterminationNode Start { get; set; }

    }
}
