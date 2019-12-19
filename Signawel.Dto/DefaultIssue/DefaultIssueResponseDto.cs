using Signawel.Domain.Enums;

namespace Signawel.Dto.DefaultIssue
{
    public class DefaultIssueResponseDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public TrafficSignType Type { get; set; }
    }
}
