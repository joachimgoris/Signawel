using System.Collections.Generic;

namespace Signawel.Domain.ReportGroups
{
    public class ReportGroup : Entity
    {
        public virtual List<CityReportGroup> CityReportGroups { get; set; } 
        public virtual List<EmailReportGroup> EmailReportGroups { get; set; }

        public ReportGroup()
        {
            CityReportGroups = new List<CityReportGroup>();
            EmailReportGroups = new List<EmailReportGroup>();
        }
    }
}
