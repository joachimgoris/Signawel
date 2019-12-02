using Signawel.Domain.Reports;

namespace Signawel.Domain
{
    public class ReportIssue
    {
       public string ReportId { get; set; }

        public string IssueId { get; set; }

        public Report Report { get; set; }
    }
}
