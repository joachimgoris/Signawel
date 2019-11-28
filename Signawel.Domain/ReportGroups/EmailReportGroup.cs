namespace Signawel.Domain.ReportGroups
{
    public class EmailReportGroup
    {
        public string EmailId { get; set; }
        public Email Email { get; set; }

        public string ReportGroupId { get; set; }
        public ReportGroup ReportGroup { get; set; }
    }
}
