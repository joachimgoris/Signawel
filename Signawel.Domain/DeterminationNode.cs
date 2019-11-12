using Signawel.Domain.Enums;
using System.Collections;
using System.Collections.Generic;

namespace Signawel.Domain
{
    public class DeterminationNode : Entity
    {

        public DeterminationNodeType Type { get; set; }

        public string Question { get; set; }

        public string SchemaId { get; set; }

        public IList<DeterminationAnswer> Answers { get; set; }

    }
}