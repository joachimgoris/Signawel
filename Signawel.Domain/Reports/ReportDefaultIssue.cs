using Signawel.Domain.Enums;

namespace Signawel.Domain.Reports
{
    public class ReportDefaultIssue : Entity
    {
        public string Name { get; set; }

        public TrafficSignType Type { get; set; }
    }
}
