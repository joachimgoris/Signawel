using Signawel.Domain.Enums;

namespace Signawel.Dto.DefaultIssue
{
    public class DefaultIssueRequestDto
    {

        public string Name { get; set; }

        public TrafficSignType Type { get; set; } = TrafficSignType.All;

    }
}
