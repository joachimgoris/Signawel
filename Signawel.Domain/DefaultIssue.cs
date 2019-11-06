using System;
using System.Collections.Generic;

namespace Signawel.Domain
{
    public class DefaultIssue
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public ItemType Type { get; set; }

        public ICollection<ReportIssue> ReportLink { get; set; }
    }
}
