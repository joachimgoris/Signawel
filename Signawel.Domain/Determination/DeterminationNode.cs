using System.Collections.Generic;
using Signawel.Domain.Enums;

namespace Signawel.Domain.Determination
{
    public class DeterminationNode : Entity
    {

        public DeterminationNodeType Type { get; set; }

        public string Question { get; set; }

        public string QuestionDescription { get; set; }

        public string SchemaId { get; set; }

        public IList<DeterminationAnswer> Answers { get; set; }

    }
}