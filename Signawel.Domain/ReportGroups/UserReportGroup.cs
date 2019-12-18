namespace Signawel.Domain.ReportGroups
{
    public class UserReportGroup
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string ReportGroupId { get; set; }
        public ReportGroup ReportGroup { get; set; }
    }
}
