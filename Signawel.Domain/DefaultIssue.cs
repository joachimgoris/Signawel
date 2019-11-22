using System.Collections.Generic;

namespace Signawel.Domain
{
    public class DefaultIssue : Entity
    {
        public string Name { get; set; }

        public ItemType Type { get; set; }

        public ICollection<ReportIssue> ReportLink { get; set; }
    }
}
