namespace Signawel.Domain.Determination
{
    public class DeterminationGraph : Entity
    {

        public string StartId { get; set; }

        public DeterminationNode Start { get; set; }

    }
}
