namespace Signawel.Domain.Determination
{
    public class DeterminationAnswer : Entity
    {

        public string Answer { get; set; }

        public string NodeId { get; set; }

        public DeterminationNode Node { get; set; }

        public string ParentNodeId { get; set; }

        public DeterminationNode ParentNode { get; set; }

    }
}